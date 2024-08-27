本项目部分代码是参考Furion、SqlSugar等开源项目

一: 项目配置运行步骤
    
    1、配置数据库:
        (1):选择数据库类型在 HuanTian.Store-appsettings.json 中 SqlSettings:SqlType 选择数据库类型 MySql、SqlServer
        (2):再修改数据库连接配置在 HuanTian.Store、appsettings.json 中 ConnectionString 对应的数据库类型连接地址
        (3):生成数据库表结构有两种方式
            (a):使用EF Core Code First生成数据库 详情可以参考第二点，前提是先删除 HuanTian.EntityFrameworkCore、Migrations文件夹下的所有文件包括文件夹，然后执行文件目录SqlData中的sql文件获得初始化数据
            (b):手动先创建数据库，需要跟配置文件的数据库名字相同，然后再使用文件目录SqlData中的sql文件初始化数据库
    2、配置中间件
        (1):如果有远程或本地Redis、RabbitMQ地址 直接修改 HuanTian.Store、appsettings.json 中 ConnectionStrings 中对应的地址
        (2):如果没有Redis、RabbitMQ地址可以 修改 HuanTian.Store、appsettings.json 中 AppSettings:MiddlewareEnable 设置为 false
    3、配置定时任务调度
        (1):HuanTian.ScheduledTasks 项目单独部署，需要修改 HuanTian.ScheduledTasks、appsettings.json 中 ConnectionStrings 中对应的数据库地址
        (2):如果需要修改调度端部署端口直接使用部署端口即可，还需要修改服务端 HuanTian.Store-appsettings.json 中 Hangfire 对应的地址配置

二: EF Core CodeFirst生成、修改数据库操作方式

    1、Domain文件夹 HuanTian.Domain项目新增实体表格 例如：NewTable
	   表文件夹内新建表的DO     例如：NewTableDO

    2、在 HuanTian.EntityFrameworkCore 项目里EfSqlContext文件中写入对应 DO 的 DbSet
       例如：
       public DbSet<NewTableDO> NewTable { get; set; }

    3、打开【程序包管理控制台】，默认项目选为 HuanTian.EntityFrameworkCore

    4、选择解决方案重新生成,如果没问题就可以执行迁移命令

    5、在【程序包管理控制台】中输入添加数据库迁移命令 "Add-Migration"
       可以简单写英文描述进行记录   例如：
       "Add-Migration add_NewTable"

    6、HuanTian.EntityFrameworkCore.MySql 的 Migrations 文件夹中
       确认是否能看到具体日期对应的数据库迁移记录文件      例如：
       [202107010000_add_NewTable]

    7、确认有迁移记录后，在【程序包管理控制台】输入更新数据库命令 "update-database" 后等待完成即可

三: 修改数据库、仓储连接方式

    1、修改 HuanTian.Store-appsettings.json 中 SqlSettings:SqlType 的数据库连接字符串 MySql、或者SqlServer
    2、修改 HuanTian.Store-Program.cs 中 builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlSugarRepository<>)); 
       如果使用的是 Autofac 容器 需要修改 HuanTian.WebCore、AutofacRegisterAutofac 中的依赖注入  把 SqlSugarRepository 切换为想要的仓储就行

四: 本项目已经全部统一 EF Core 生的表名、列名为下划线命名法  方便命名
    
    1、例如  TestTable 会转换为 test_table
    2、如果需要修改命名方式可以修改 HuanTian.Infrastructure.ToLowerHump 方法
    3、如果不需要统一列名表名可以修改 HuanTian.Store-appsettings.json 中 SqlSettings:(GlobalSettingsTableName、GlobalSettingsColumnName) 的值为 false 
       **注意**:如果切换为false的话,仓储就不能随意切换了,因为不用orm框架生成的表名、列名不一致,会报错。

五: 动态生成 Controle 功能

    1、可参考 HuanTian.Service 中的方法  只需要继承 IDynamicApiController 接口就会生成 Controle
    2、Swagger中的大部分常用配置,本项目都已经配置

六: 项目关于Swagger备注的实现

    1、Swagger备注默认是加载HuanTian.Infrastructure、HuanTian.Service两个项目的xml文件，如果需要添加的话，
        (1)需要在HuanTian.Store引用该项目 
        (2)需要修改在项目文件添加 <GenerateDocumentationFile>true</GenerateDocumentationFile> <DocumentationFile>项目名称.xml</DocumentationFile> 这两行代码，可以参考HuanTian.Infrastructure的实现方式
    2、为了更方便面向接口编程,如果在接口跟接口实现类中,只有接口中有备注那么接口中的备注就会替换到实现类中,因为Swagger默认只会读取实现类生成的controle的备注，
       **注意**：但是需要谨遵命名规范  IService 和 Service 两个类名的接口实现类才会被替换(你也可以进行修改,在HuanTian.WebCore-SwaggerExtensions.cs),如果不是这两个命名规范的话,那么就需要在接口实现类中添加备注了。
    3、可以对 Controle 进行分组、ApiDescriptionSettingsAttribute["",order=10]特性。 对分组的Swagger配置则在 HuanTian.Store-appsettings.json 中

七: 关于Excel、PDF文件按模板导出

    1、Excel、PDF文件按模板导出的功能已经实现,可以参考 HuanTian.Service 中的方法 TestData 类中 DownmldExcel、DownmldPdf
	2、Excel、PDF文件按模板导出的功能是使用 EPPlus、iTextSharp 实现的

八: 代码生成功能
    
    1、案例参考 HuanTian.Service、SysCodeGenService、RunLocal  实现流程为在 Razor 模板中写好代码的模板,再通过实体类数据进行替换,最后生成代码文件 
    2、模板地址是 HuanTian.Store、wwwroot、Template 中的文件

九: EntityFranmwork Code 过滤器

    1、为了实现多租户模式,翻阅了EntityFranmwork Code 官方文档和大量文档 发现Ef还是存在很多不足
    2、由于官方不支持过滤器重复添加,所以新增了累计添加过滤器方法,跟全局表格添加,可以参考 HuanTian.EntityFrameworkCore、EfSqlContext.cs 中的方法
    3、EntityFranmwork Code 动态监测值,只能通过依赖注入的方法获取,如果使用静态方法或方法将只会加载的时候拿一遍值。 详情可以参考TenantId赋值

十: 消息队列、Redis 仓储
    
    1、新增消息队列仓储，暂时实现为RabbitMQ 用字符串去判断使用哪个队列 通过 SelectQueue 方法
    2、消息队列打开消费端，消息如果成功接受就 ack确认 如果失败就 nack 重新入队列 失败次数大于3次就存进死信队列了
    3、Redis现在暂时支持 String、Set、Hash 三种类型 (因为只用到这三种类型)
    4、Redis仓储如果只是想看项目效果的话,没有安装Redis环境的话,建议把 Redis 相关代码都注释 

十一: 多租户模式

    1、利用ORM的过滤器实现了多租户模式
    2、清楚过滤器可以用仓储中的 IgnoreFilters 方法,查询数据
    3、可以参考 HuanTian.EntityFrameworkCore、EfSqlContext.cs 中的方法 和 HuanTian.WebCore、SqlSugarExtensions、SqlSugar
    4、多租户用户体验 (仅限提供的数据库)  账号密码: admin 123  、 huantian 123

十二: 邮件发送

    1、邮件发送是利用RabbitMQ消息队列实现的 实现代码可以参考HuanTian.Service、TestData、SendEmail
    2、通过读取当前用户的租户信息，获得邮箱配置，发送前需要先设置 SysTenant 表用户相关的租户信息邮箱配置 EmailConfig

十三: 日志功能
    
    1、项目已配置好 日志生成File还是Sql 在 Appsettings 中 [AppSettings:LogPath]配置
    2、日志功能已更改为通过RabbitMQ消息队列实现的 控制打开消费端代码在 HuanTian.WebCore、HostAppLifetimeExtensions(AppLeftTime)、ApplicationStarted
    3、日志记录等级也可以通过 Appsetting 中 [AppSettings:ApiLogLevel] 设置记录 Web Api 记录信息

十四: 定时任务

    1、项目中 HuanTian.ScheduledTasks 为调度端 推荐单独部署,因为是测试项目所以我放在一个解决方案中
    2、Hangfire 支持 
        (1):项目内调度 耦合度太高不适用大型项目和分布式项目,项目中没有使用
        (2):Hangfire.HttpJob 方式通过Http方式调用 让业务跟调度分离 实现解耦 但是考虑到安全性和鉴权 等等 建议使用 Hangfire.HttpJob.Agent
        (3):Hangfire.HttpJob.Agent 方式通过Http方式调用 通过中间件截取 更适用于项目开发 所以项目中使用的是这种方式
    3、Hangfire.HttpJob 就是定时调用接口 通过接口的参数就能直接调度 注入方式在调度端直接添加
    4、Hangfire.HttpJob.Agent 跟HttpJob 类似需要配置 AgentClass 详情可以参考官方文档 或者我提供的实例、 也支持自动注入 在appsettings.json 中配置 JobAgent:EnableAutoRegister 设置为 true
       再配置好调度端和服务端地址等等启动项目就可以自动注入了、还支持回调函数 类似于 ajax 的success、error回调,详情可以参考官方文档
    5、示例代码 HuanTian.Service、Job、DeleteDataJob 
    6、详情可以参考 HttpJob 官方文档 https://github.com/yuzd/Hangfire.HttpJob/wiki/03.HttpJob.Agent%E7%BB%84%E4%BB%B6%E4%BB%8B%E7%BB%8D%E4%BB%A5%E5%8F%8A%E5%A6%82%E4%BD%95%E4%BD%BF%E7%94%A8

十五: 项目中已经实现的功能

    1、防止Token已经失效依然被请求，使用Redis加JWT，缓存已经注销的用户Token，生成黑名单在鉴权过滤器中查询存在就显示未认证
    2、增加了友好的异常处理，使用友好异常处理，返回200状态码。
       示例:throw new FriendlyException("测试异常");  
       建议只在需要的情况下使用，不然出问题排查不到问题错在哪里
    3、前端已经全局拦截后端异常信息包括文件信息 详情可以参考前端文件 Src、utils、request.js 中
    4、项目SqlData有sql文件，直接加载就可以获得初始化项目SQL，或者通过Ef Core Code First 生成数据库(不过只有数据结构没有数据)
    5、EF 生成数据库 1、已添加遍历枚举类型获取枚举的备注值 2、增加默认值特性 3、EF 使用的时候打印日志在控制台会带上对应的参数
    6、更多功能正在实现中,敬请期待 ····   
       个人开发精力有限,还请谅解   ···· 
       本人也是一个小菜鸡,遇到的问题解决的方式都是自己找文档、论坛一步步解决的。 ····
       在学习中慢慢完善本项目,如果你也是志同道合之人欢迎你的加入。               ····
