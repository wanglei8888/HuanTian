using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;

namespace HuanTian.Service
{
    /// <summary>
    /// RabbitMQ 消息队列
    /// </summary>
    public class RabbitMQMessageQueue : IMessageQueue
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _queueName;
        private readonly string _connectionString;
        private readonly static ConcurrentDictionary<string, IMessageQueue> _queues = new ConcurrentDictionary<string, IMessageQueue>();
        private readonly ILogger<RabbitMQMessageQueue> _logger;
        public RabbitMQMessageQueue(ILogger<RabbitMQMessageQueue> logger)
        {
            _logger = logger;
        }
        public RabbitMQMessageQueue(ILogger<RabbitMQMessageQueue> logger, string queueName)
            : this(logger)
        {
            _queueName = queueName;
            _connectionString = App.Configuration["ConnectionStrings:RabbitMQ"];
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_connectionString)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }
        public IMessageQueue SelectQueue(string queueName)
        {
            if (_queues.ContainsKey(queueName))
            {
                return _queues[queueName];
            }
            else
            {
                var queue = new RabbitMQMessageQueue(_logger, queueName);
                _queues[queueName] = queue;
                return queue;
            }
        }
        public void Enqueue(string message)
        {
            CheckQueue();
            var body = Encoding.UTF8.GetBytes(message.ToString());

            _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
        }
        public TValue Dequeue<TValue>()
        {
            CheckQueue();
            var result = _channel.BasicGet(_queueName, autoAck: true);
            if (result != null)
            {
                var body = result.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                return JsonSerializer.Deserialize<TValue>(message);
            }
            return default;
        }
        public void StartConsumingAsync(Func<string, Task<bool>> processMessage, bool finishClose = false)
        {
            CheckQueue();
            var maxRetryCount = 3;
            var messageCount = _channel.MessageCount(_queueName);
            var finishNum = 0;
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var success = false;
                var errorMsg = "";
                try
                {
                    success = await Task.Run(() => processMessage(message));
                }
                catch (Exception ex)
                {
                    errorMsg = $"消息队列-{_queueName}-处理异常:异常信息为:{ex.Message}";
                    _logger.LogError(errorMsg, ex);
                }
                // 如果成功则确认消息
                if (success)
                {
                    finishNum++;
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                else
                {
                    var retryCount = GetRetryCount(ea.BasicProperties);
                    if (retryCount < maxRetryCount)
                    {
                        retryCount++;
                        SetRetryCount(ea.BasicProperties, retryCount);
                        // 初始的消息确认
                        _channel.BasicAck(ea.DeliveryTag, false);
                        // 带着重试次数重新排队
                        _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: ea.BasicProperties, body: body);
                    }
                    else
                    {
                        finishNum++;
                        // 初始的消息拒绝 不再进入队列
                        _channel.BasicReject(ea.DeliveryTag, false);
                        // 统一存放到错误消息队列
                        var erroeQueue = SelectQueue(MsgQConst.FailMsg);
                        // 错误消息添加实体中
                        JObject jsonObject = JObject.Parse(message);
                        jsonObject["ErrorMsg"] = errorMsg;
                        erroeQueue.Enqueue(jsonObject.ToString());
                    }
                    // 如果finishClose 设置true 消息处理完成则关闭连接
                    if (messageCount == finishNum && finishClose)
                    {
                        Dispose();
                    }
                }
            };
            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }

        public void StartConsuming(Func<string, bool> processMessage, bool finishClose = false)
        {
            CheckQueue();
            var maxRetryCount = 3;
            var messageCount = _channel.MessageCount(_queueName);
            var finishNum = 0;
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var success = false;
                var errorMsg = "";
                try
                {
                    success = processMessage(message);
                }
                catch (Exception ex)
                {
                    errorMsg = $"消息队列-{_queueName}-处理异常:异常信息为:{ex.Message}";
                    App.GetService<ILogger<RabbitMQMessageQueue>>().LogError(errorMsg);
                }
                // 如果成功则确认消息
                if (success)
                {
                    finishNum++;
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                else
                {
                    var retryCount = GetRetryCount(ea.BasicProperties);
                    if (retryCount < maxRetryCount)
                    {
                        retryCount++;
                        SetRetryCount(ea.BasicProperties, retryCount);
                        // 初始的消息确认
                        _channel.BasicAck(ea.DeliveryTag, false);
                        // 带着重试次数重新排队
                        _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: ea.BasicProperties, body: body);
                    }
                    else
                    {
                        finishNum++;
                        // 初始的消息拒绝 不再进入队列
                        _channel.BasicReject(ea.DeliveryTag, false);
                        // 统一存放到错误消息队列
                        var erroeQueue = SelectQueue(MsgQConst.FailMsg);
                        // 错误消息添加实体中
                        JObject jsonObject = JObject.Parse(message);
                        jsonObject["ErrorMsg"] = errorMsg;
                        erroeQueue.Enqueue(jsonObject.ToString());
                    }
                    // 如果finishClose 设置true 消息处理完成则关闭连接
                    if (messageCount == finishNum && finishClose)
                    {
                        Dispose();
                    }
                }
            };
            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }

        public void Purge()
        {
            CheckQueue();
            _channel.QueuePurge(_queueName);
        }

        public void Dispose()
        {
            // 默认队列名称为null  不需要关闭连接
            if (!string.IsNullOrEmpty(_queueName))
            {
                Console.WriteLine($"{_queueName}正在关闭连接");
                _queues.TryRemove(_queueName, out var _);
                _channel.Close();
                _connection.Close();
            }
        }
        /// <summary>
        /// 获取重试次数
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        private int GetRetryCount(IBasicProperties properties)
        {
            var headers = properties.Headers;
            if (headers != null && headers.ContainsKey("retry_count"))
            {
                return Convert.ToInt32(headers["retry_count"]);
            }
            return 0;
        }
        /// <summary>
        /// 设置重试次数
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="retryCount"></param>
        private void SetRetryCount(IBasicProperties properties, int retryCount)
        {
            var headers = properties.Headers ?? new Dictionary<string, object>();
            headers["retry_count"] = retryCount;
            properties.Headers = headers;
        }
        /// <summary>
        /// 检查队列
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        private void CheckQueue()
        {
            if (string.IsNullOrEmpty(_queueName))
                throw new ArgumentException("队列名称不能为空,清先调用 SelectQueue 方法,选择队列");
        }
    }
}
