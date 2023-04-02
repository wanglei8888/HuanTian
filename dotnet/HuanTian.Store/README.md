本项目部分代码是参考Furion开源项目   官网地址:https://furion.baiqian.ltd/docs/category/appendix

一：CodeFirst操作方式

    1、Domain文件夹 HuanTian.Domain项目新增实体表格 例如：NewTable
	   表文件夹内新建表的DO     例如：NewTableDO

    2、在 HuanTian.EntityFrameworkCore.MySql 项目里MySqlContext文件中写入对应 DO 的 DbSet
       例如：
       public DbSet<NewTableDO> NewTable { get; set; }

    3、打开【程序包管理控制台】，默认项目选为 Suncere.AirAnalysis.EntityFrameworkCore.Mysql / SqlServer

    4、在【程序包管理控制台】中输入添加数据库迁移命令 "Add-Migration"
       可以简单写英文描述进行记录   例如：
       "Add-Migration add_NewTable"

    5、HuanTian.EntityFrameworkCore.MySql 的 Migrations 文件夹中
       确认是否能看到具体日期对应的数据库迁移记录文件      例如：
       [202107010000_add_NewTable]

    6、确认有迁移记录后，在【程序包管理控制台】输入更新数据库命令 "update-database" 后等待完成即可