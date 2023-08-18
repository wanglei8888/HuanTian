本项目部分代码是参考Furion开源项目   官网地址:https://furion.baiqian.ltd/docs/category/appendix

一：CodeFirst操作方式

    1、Domain文件夹 HuanTian.Domain项目新增实体表格 例如：NewTable
	   表文件夹内新建表的DO     例如：NewTableDO

    2、在 HuanTian.EntityFrameworkCore 项目里EfSqlContext文件中写入对应 DO 的 DbSet
       例如：
       public DbSet<NewTableDO> NewTable { get; set; }

    3、打开【程序包管理控制台】，默认项目选为 HuanTian.Store

    4、选择解决方案重新生成,如果没问题就可以执行迁移命令

    5、在【程序包管理控制台】中输入添加数据库迁移命令 "Add-Migration"
       可以简单写英文描述进行记录   例如：
       "Add-Migration add_NewTable"

    6、HuanTian.EntityFrameworkCore.MySql 的 Migrations 文件夹中
       确认是否能看到具体日期对应的数据库迁移记录文件      例如：
       [202107010000_add_NewTable]

    7、确认有迁移记录后，在【程序包管理控制台】输入更新数据库命令 "update-database" 后等待完成即可

二：修改数据库、仓储连接方式

    1、修改 HuanTian.Store-appsettings.json 中 SqlSettings:SqlType 的数据库连接字符串 MySql、或者SqlServer
    2、修改 HuanTian.Store-Program.cs 中 builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlSugarRepository<>)); 
       如果使用的是 Autofac 容器 需要修改 HuanTian.WebCore、AutofacRegisterAutofac 中的依赖注入  把 SqlSugarRepository 切换为想要的仓储就行

三：本项目已经全部统一 EF Core 生的表名、列名为下划线命名法  方便命名
    
    1、例如  TestTable 会转换为 test_table
    2、如果需要修改命名方式可以修改 HuanTian.Infrastructure.ToLowerHump 方法
    3、如果不需要统一列名表名可以修改 HuanTian.Store-appsettings.json 中 SqlSettings:(GlobalSettingsTableName、GlobalSettingsColumnName) 的值为 false 
       **注意**:如果切换为false的话,仓储就不能随意切换了,因为不用orm框架生成的表名、列名不一致,会报错。

四: 动态生成 Controle 功能

    1、可参考 HuanTian.Service 中的方法  只需要继承 IDynamicApiController 接口就会生成 Controle
    2、Swagger中的大部分常用配置,本项目都已经配置

五：项目关于Swagger备注的实现

    1、Swagger备注默认是加载HuanTian.Infrastructure、HuanTian.Service两个项目的xml文件，如果需要添加的话，
        (1)需要在HuanTian.Store引用该项目 
        (2)需要修改在项目文件添加 <GenerateDocumentationFile>true</GenerateDocumentationFile> <DocumentationFile>项目名称.xml</DocumentationFile> 这两行代码，可以参考HuanTian.Infrastructure的实现方式
    2、为了更方便面向接口编程,如果在接口跟接口实现类中,只有接口中有备注那么接口中的备注就会替换到实现类中,因为Swagger默认只会读取实现类生成的controle的备注，
       **注意**：但是需要谨遵命名规范  IService 和 Service 两个类名的接口实现类才会被替换(你也可以进行修改,在HuanTian.WebCore-SwaggerExtensions.cs),如果不是这两个命名规范的话,那么就需要在接口实现类中添加备注了。
    3、可以对 Controle 进行分组、ApiDescriptionSettingsAttribute["",order=10]特性。 对分组的Swagger配置则在 HuanTian.Store-appsettings.json 中

六: 关于Excel、PDF文件按模板导出

    1、Excel、PDF文件按模板导出的功能已经实现,可以参考 HuanTian.Service 中的方法 TestData 类中 DownmldExcel、DownmldPdf
	2、Excel、PDF文件按模板导出的功能是使用 EPPlus、iTextSharp 实现的

七: 代码生成功能
    
    1、案例参考 HuanTian.Service、SysCodeGenService、RunLocal  实现流程为在 Razor 模板中写好代码的模板,再通过实体类数据进行替换,最后生成代码文件 
    2、模板地址是 HuanTian.Store、wwwroot、Template 中的文件

八: EntityFranmwork Code 过滤器

    1、为了实现多租户模式,翻阅了EntityFranmwork Code 官方文档和大量文档 发现Ef还是存在很多不足
    2、由于官方不支持过滤器重复添加,所以新增了累计添加过滤器方法,跟全局表格添加,可以参考 HuanTian.EntityFrameworkCore、EfSqlContext.cs 中的方法
    3、EntityFranmwork Code 动态监测值,只能通过依赖注入的方法获取,如果使用静态方法或方法将只会加载的时候拿一遍值。 详情可以参考TenantId赋值

九: 消息队列、Redis 仓储
    
    1、新增消息队列仓储，暂时实现为RabbitMQ 用字符串去判断使用哪个队列 通过 SelectQueue 方法
    2、Redis仓储如果只是想看项目效果的话,没有安装Redis环境的话,建议把 Redis 相关代码都注释 

十: 多租户模式

    1、利用ORM的过滤器实现了多租户模式
    2、清楚过滤器可以用仓储中的 IgnoreFilters 方法,查询数据
    3、可以参考 HuanTian.EntityFrameworkCore、EfSqlContext.cs 中的方法 和 HuanTian.WebCore、SqlSugarExtensions、SqlSugar 

十一：项目中已经实现的功能

    1、防止Token已经失效依然被请求，使用Redis加JWT，缓存已经注销的用户Token，生成黑名单在鉴权过滤器中查询存在就显示未认证
    2、增加了友好的异常处理，使用友好异常处理，返回200状态码。
       示例:throw new FriendlyException("测试异常");  
       建议只在需要的情况下使用，不然出问题排查不到问题错在哪里
    3、前端已经全局拦截后端异常信息包括文件信息 详情可以参考前端文件 Src、utils、request.js 中
    4、项目SqlData有sql文件，直接加载就可以获得初始化项目SQL，或者通过Ef Core Code First 生成数据库(不过只有数据结构没有数据)
    5、更多功能正在实现中,敬请期待 ····   
       个人开发精力有限,还请谅解   ···· 
       本人也是一个小菜鸡,遇到的问题解决的方式都是自己找文档、论坛一步步解决的。 ····
       在学习中慢慢完善本项目,如果你也是志同道合之人欢迎你的加入。               ····
