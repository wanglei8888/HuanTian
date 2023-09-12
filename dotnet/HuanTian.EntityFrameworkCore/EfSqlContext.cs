using HuanTian.Infrastructure;
using HuanTian.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using NPOI.SS.Formula.Functions;
using SqlSugar.Extensions;
using System.IO.Pipelines;
using System.Linq.Expressions;
using Expression = System.Linq.Expressions.Expression;

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
        public DbSet<SysEmailTemplateDO> SysEmailTemplateDO { get; set; }
        public DbSet<SysTenantDO> SysTenantDO { get; set; }
        public DbSet<SysLogInfoDO> SysLogInfoDO { get; set; }
        public DbSet<SysLogErrorDO> SysLogErrorDO { get; set; }

        #region 配置文件
        private readonly IQueryFilter _tenantService;
        public EfSqlContext(DbContextOptions<EfSqlContext> options, IQueryFilter tenantService) : base(options)
        {
            _tenantService = tenantService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 获取所有实体类型
            foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            {
                if (App.Configuration["SqlSettings:GlobalSettingsTableName"].ObjToBool())
                {
                    // 全局设置表名生成规则
                    if (entity.ClrType.Name.EndsWith("DO"))
                    {
                        // 替换DO后缀 并将属性名转换成小写，并将驼峰命名方式转换为下划线命名方式
                        var tableName = entity.ClrType.Name.Replace("DO", "").ToLowerHump();
                        entity.SetTableName(tableName);
                    }
                }

                if (App.Configuration["SqlSettings:GlobalSettingsColumnName"].ObjToBool())
                {
                    // 全局设置列名生成规则
                    foreach (var property in entity.GetProperties())
                    {
                        // 将属性名转换成小写，并将驼峰命名方式转换为下划线命名方式
                        property.SetColumnName(property.Name.ToLowerHump());
                    }
                }

                if (!App.Configuration["SqlSettings:GlobalFilter"].ObjToBool())
                    return;

                entity.AppendAllQueryFilter(t => !EF.Property<bool>(t, "Deleted"), "Deleted");
                entity.AppendAllQueryFilter(t => EF.Property<long>(t, "TenantId") == _tenantService.GetCurrentTenantId(), "TenantId");
            }

            // modelBuilder.Entity<SysDeptDO>().HasQueryFilter(t => EF.Property<long>(t, "TenantId") == _tenantService.GetCurrentTenantId());
            // modelBuilder.AppendQueryFilter<SysDeptDO>(t => EF.Property<long>(t, "TenantId") == _tenantService.GetCurrentTenantId());
        }
        #endregion
    }

    public static class EntityFrameworkCoreExtensions
    {
        /// <summary>
        /// 为一个实体新增全局过滤器 如果之前有过滤器 两个过滤器就会合并 ( EF Core 原始方法不支持多次添加 )
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="expression"></param>
        public static void AppendQueryFilter<T>(this IMutableEntityType entityType,
            Expression<Func<T, bool>> expression)
        {
            if (!typeof(T).IsAssignableFrom(entityType.ClrType))
                return;
            var parameterType = Expression.Parameter(entityType.ClrType);
            var expressionFilter = ReplacingExpressionVisitor.Replace(
                expression.Parameters.Single(), parameterType, expression.Body);

            var currentQueryFilter = entityType.GetQueryFilter();
            if (currentQueryFilter != null)
            {
                var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
                    currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);
                expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
            }

            var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
            entityType.SetQueryFilter(lambdaExpression);
        }
        /// <summary>
        /// 为所有实体附加全局过滤器 
        /// 如果之前有过滤器 两个过滤器就会合并 ( EF Core 原始方法不支持多次添加 )
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="expression"></param>
        public static void AppendAllQueryFilter(this IMutableEntityType entityType,
            Expression<Func<object, bool>> expression, string columnName)
        {
            var hasTenantIdProperty = entityType.GetProperties().Any(property => property.Name == columnName);
            if (!hasTenantIdProperty)
                return;
            var ignoreEntity = new string[] { "SysUserDO" };
            if (ignoreEntity.Contains(entityType.ClrType.Name))
                return;
            var parameterType = Expression.Parameter(entityType.ClrType);
            var expressionFilter = ReplacingExpressionVisitor.Replace(
                expression.Parameters.Single(), parameterType, expression.Body);

            var currentQueryFilter = entityType.GetQueryFilter();
            if (currentQueryFilter != null)
            {
                var currentExpressionFilter = ReplacingExpressionVisitor.Replace(
                    currentQueryFilter.Parameters.Single(), parameterType, currentQueryFilter.Body);
                expressionFilter = Expression.AndAlso(currentExpressionFilter, expressionFilter);
            }

            var lambdaExpression = Expression.Lambda(expressionFilter, parameterType);
            entityType.SetQueryFilter(lambdaExpression);
        }
    }
}
