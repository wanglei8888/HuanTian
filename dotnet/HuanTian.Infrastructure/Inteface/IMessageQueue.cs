namespace HuanTian.Infrastructure
{
    /// <summary>
    /// 消息队列仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMessageQueue : IDisposable
    {
        /// <summary>
        /// 选择队列
        /// </summary>
        /// <param name="queueName"></param>
        IMessageQueue SelectQueue(string queueName);
        /// <summary>
        /// 用于将消息放入队列中
        /// </summary>
        /// <param name="message"></param>
        void Enqueue(string message);
        /// <summary>
        /// 用于从队列中取出消息
        /// </summary>
        /// <returns></returns>
        TValue Dequeue<TValue>();
        /// <summary>
        /// 异步开始消费端,消费完后是否关闭
        /// </summary>
        /// <param name="processMessage"></param>
        void StartConsumingAsync(Func<string, Task<bool>> processMessage, bool finishClose = false);
        /// <summary>
        /// 同步开始消费端,消费完后是否关闭
        /// </summary>
        /// <param name="processMessage"></param>
        void StartConsuming(Func<string, bool> processMessage, bool finishClose = false);
        /// <summary>
        /// 清除队列
        /// </summary>
        void Purge();
    }
    /// <summary>
    /// 消息队列实体约束
    /// </summary>
    public interface IMsgQBaseEntity
    {
        /// <summary>
        /// 消息队列报错信息
        /// </summary>
        public string ErrorMsg { get; set; }
    }
}
