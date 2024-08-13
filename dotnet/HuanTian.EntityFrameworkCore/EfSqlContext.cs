using HuanTian.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using SqlSugar.Extensions;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace HuanTian.EntityFrameworkCore
{
    public class EfSqlContext : DbContext
    {
        #region 表格实体

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
        #endregion

        #region 配置文件
        private readonly IQueryFilter _tenantService;
        public EfSqlContext(DbContextOptions<EfSqlContext> options, IQueryFilter tenantService) : base(options)
        {
            _tenantService = tenantService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var xmlFilePath = Path.Combine(AppContext.BaseDirectory, "HuanTian.Infrastructure.xml");
            // 加载 XML 文件
            var xmlDoc = XDocument.Load(xmlFilePath);
            // 获取所有实体类型
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                #region 统一表名、列名生成规则
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
                #endregion

                // 获取实体类型的所有属性 
                foreach (var property in entity.ClrType.GetProperties())
                {
                    #region 获取属性上的 [DefaultValue] 特性 修改默认值
                    var defaultValueAttribute = property.GetCustomAttribute<DefaultValueAttribute>();

                    if (defaultValueAttribute != null)
                    {
                        // 获取特性中定义的默认值
                        var defaultValue = defaultValueAttribute.Value;

                        // 使用 Fluent API 设置数据库列的默认值
                        modelBuilder.Entity(entity.ClrType)
                            .Property(property.Name)
                            .HasDefaultValue(defaultValue);
                    } 
                    #endregion

                    #region 自动生成枚举类型的备注
                    var propertyName = property.Name;
                    // 实体所属命名空间
                    var entityTypeFullName = entity.ClrType.FullName;
                    // 实体类型
                    var enumType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                    var isEnum = property.PropertyType.IsEnum || (Nullable.GetUnderlyingType(property.PropertyType)?.IsEnum == true);
                    if (isEnum)
                    {
                        var enumName = $"F:{enumType.FullName}";
                        var enumMasterName = $"P:{entityTypeFullName}.{propertyName}";

                        Console.WriteLine($"FullName:{enumType.FullName},entityTypeFullName:{entityTypeFullName} ");
                        // 获取枚举的所有成员
                        var enumValues = xmlDoc.Descendants("member")
                                               .Where(x => x.Attribute("name")?.Value.StartsWith(enumName) == true)
                                               .Select(x => new
                                               {
                                                   Name = x.Attribute("name")?.Value.Substring(enumName.Length + 1),
                                                   Summary = x.Element("summary")?.Value.Trim()
                                               }).ToList();
                        // 获取实体的备注
                        var entityComment = xmlDoc.Descendants("member")
                                                  .FirstOrDefault(x => x.Attribute("name")?.Value == enumMasterName)?
                                                  .Element("summary")?.Value.Trim();

                        if (enumValues.Any())
                        {
                            var enumFieldComment = $"{entityComment} (";
                            enumFieldComment += string.Join(", ", enumValues.Select(ev =>
                            {
                                var enumMember = Enum.Parse(enumType, ev.Name!);
                                var enumValueNumber = Convert.ToInt32(enumMember);
                                return $"{enumValueNumber} {ev.Summary}";
                            }));
                            enumFieldComment += ")";

                            Console.WriteLine("最终备注:" + enumFieldComment);

                            modelBuilder.Entity(entity.ClrType)
                                        .Property(property.Name)
                                        .HasComment(enumFieldComment);
                        }
                    } 
                    #endregion
                }

                if (!App.Configuration["SqlSettings:GlobalFilter"].ObjToBool())
                    continue;
                // 为所有实体附加全局过滤器
                entity.AppendQueryFilter(t => !EF.Property<bool>(t, "Deleted"), "Deleted");
                entity.AppendQueryFilter(t => EF.Property<long>(t, "TenantId") == _tenantService.GetCurrentTenantId(), "TenantId");
            }

            // modelBuilder.Entity<SysDeptDO>().HasQueryFilter(t => EF.Property<long>(t, "TenantId") == _tenantService.GetCurrentTenantId());
            // modelBuilder.AppendQueryFilter<SysDeptDO>(t => EF.Property<long>(t, "TenantId") == _tenantService.GetCurrentTenantId());
        }
        #endregion
    }

    public static class EntityFrameworkCoreExtensions
    {
        /// <summary>
        /// 为所有实体附加全局过滤器 
        /// 如果之前有过滤器 两个过滤器就会合并 ( EF Core 原始方法不支持多次添加 )
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="expression"></param>
        public static void AppendQueryFilter(this IMutableEntityType entityType,
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
