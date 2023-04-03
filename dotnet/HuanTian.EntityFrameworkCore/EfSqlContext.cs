using HuanTian.Entities;
using HuanTian.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HuanTian.EntityFrameworkCore
{
    public class EfSqlContext : DbContext
    {
        public DbSet<SysUserInfoDO> UserInfoDO { get; set; }
        public DbSet<SysMenuRoleDO> SysMneuRoleDO { get; set; }
        public DbSet<SysRoleInfoDO> SysRoleInfoDO { get; set; }
        public DbSet<SysUserRoleDO> SysUserRoleDO { get; set; }
        public DbSet<SysMenuDO> SysMenuDO { get; set; }

        
        #region 
        public EfSqlContext()
        {

        }

        public EfSqlContext(DbContextOptions<EfSqlContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 获取所有实体类型
            var entityTypes = modelBuilder.Model.GetEntityTypes();

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (Appsettings.Configuration["SqlSettings:GlobalSettingsTableName"] == "true") 
                {
                    // 全局设置表名生成规则
                    if (entity.ClrType.Name.EndsWith("DO"))
                    {
                        // 替换DO后缀 并将属性名转换成小写，并将驼峰命名方式转换为下划线命名方式
                        var tableName = entity.ClrType.Name.Replace("DO", "").ToLowerHump();
                        entity.SetTableName(tableName);
                    }
                }
                    
                if (Appsettings.Configuration["SqlSettings:GlobalSettingsColumnName"] == "true") 
                {
                    // 全局设置列名生成规则
                    foreach (var property in entity.GetProperties())
                    {
                        // 将属性名转换成小写，并将驼峰命名方式转换为下划线命名方式
                        property.SetColumnName(property.Name.ToLowerHump());
                    }
                }
                   
            }
        }
        #endregion
    }
}
