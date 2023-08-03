using HuanTian.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HuanTian.EntityFrameworkCore
{
    public class EfSqlContext : DbContext
    {
        public DbSet<SysUserDO> UserInfoDO { get; set; }
        public DbSet<SysMenuRoleDO> SysMneuRoleDO { get; set; }
        public DbSet<SysRoleDO> SysRoleInfoDO { get; set; }
        public DbSet<SysUserRoleDO> SysUserRoleDO { get; set; }
        public DbSet<SysMenuDO> SysMenuDO { get; set; }
        public DbSet<SysDicDO> SysDictionaryDO { get; set; }
        public DbSet<SysDicDetailDO> SysDictionaryDetailDO { get; set; }
        public DbSet<SysCodeGenDO> SysCodeGenDO { get; set; }
        public DbSet<SysCodeGenDetailDO> SysCodeGenDetailDO { get; set; }
        public DbSet<SysDeptDO> SysDeptDO { get; set; }
        public DbSet<SysPermissionsDO> SysPermissionsDO { get; set; }
        public DbSet<SysRolePermissionsDO> SysRolePermissionsDO { get; set; }
        public DbSet<SysAppsDO> SysAppDO { get; set; }
        
        #region 配置文件
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
                if (App.Configuration["SqlSettings:GlobalSettingsTableName"] == "True") 
                {
                    // 全局设置表名生成规则
                    if (entity.ClrType.Name.EndsWith("DO"))
                    {
                        // 替换DO后缀 并将属性名转换成小写，并将驼峰命名方式转换为下划线命名方式
                        var tableName = entity.ClrType.Name.Replace("DO", "").ToLowerHump();
                        entity.SetTableName(tableName);
                    }
                }
                    
                if (App.Configuration["SqlSettings:GlobalSettingsColumnName"] == "True") 
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
