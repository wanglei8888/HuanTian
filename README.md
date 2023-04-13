# .Net6 + Ant Design of Vue 前后端分离项目

#### 🎁项目介绍
* 前端基于ant design vue 的模板而搭建的服务器 （前端模板微小改动主要涉及后端）

* 后端涉及技术 .Net6框架、Mapster对象映射、Autofac自动依赖注入、Jwt权限认证、Logging缓存写入文件夹、继承接口动态生成API Controle、Swagger动态配置等等技术
  仓储层支持 EF Core 、SqlSugar,数据库（暂时）支持 SqlServer、MySql
#### :tw-1f37a: 前端页面展示
![首页展示](image.png)

#### 🍁项目说明
* 项目分层思想
```
Repository                        --仓储层
    HuanTian.EntityFrameworkCore  --存放EF Core相关的配置信息
    HuanTian.SqlSugar             --存放SqlSugar相关的配置信息
HuanTian.Infrastructure           --基础设施层 最上级项目主要存放公共类、dll包等  方便各层级调用
HuanTian.Service                  --主要存放业务所需接口、接口实现类  Autofac自动注册依赖注入   命名示例 MenuService、IMenuService
HuanTian.Entities                 --主要用于存放项目ORM的实体类、公共枚举、开发所需的Input和Output、Mapper等等
HuanTian.Store                    --程序的启动项、存放一些静态文件等等   
HuanTian.WebCore                  --程序中间件的实现、服务拓展配置
```
* 下载源码参考文档
```
如果本项目对您有帮助，您可以点右上角 “Star” 收藏一下 ，获取第一时间更新，谢谢！
如果您发现项目有任何问题或者不合理的地方，欢迎 “Issues” 大家一起学习进步。
```