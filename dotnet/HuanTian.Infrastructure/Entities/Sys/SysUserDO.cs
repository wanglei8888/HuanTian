using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HuanTian.Infrastructure;
/// <summary>
/// 用户信息表
/// </summary>
[Comment("用户信息表")]
public class SysUserDO : BaseEntityBusiness
{
    /// <summary>
    /// 所属部门Id
    /// </summary>
    [Required]
    [Comment("所属部门Id")]
    public long DeptId { get; set; }
    /// <summary>
    /// 名字
    /// </summary>
    [Required]
    [MaxLength(50)]
    [Comment("名字")]
    public string? Name { get; set; }
    /// <summary>
    /// 名字
    /// </summary>
    [MaxLength(200)]
    [Comment("图片路径")]
    public string? Avatar { get; set; }
    /// <summary>
    /// 用户名
    /// </summary>
    [Required]
    [MaxLength(50)]
    [Comment("用户名")]
    public string UserName { get; set; }
    /// <summary>
    /// 用户密码
    /// </summary>
    [Required]
    [MaxLength(50)]
    [Comment("用户密码")]
    public string? Password { get; set; }
    /// <summary>
    /// 账号类型
    /// </summary>
    [Required]
    [Comment("账号类型")]
    public SysUserTypeEnum Type { get; set; }
    /// <summary>
    /// 启用
    /// </summary>
    [Required]
    [Comment("启用")]
    public bool Enable { get; set; }
    /// <summary>
    /// 电话
    /// </summary>
    [MaxLength(50)]
    [Comment("电话")]
    public string? Telephone { get; set; }
    /// <summary>
    /// 最后登陆IP
    /// </summary>
    [MaxLength(100)]
    [Comment("最后登陆IP")]
    public string? LastLoginIp { get; set; }
    /// <summary>
    /// 最后登陆时间
    /// </summary>
    [Comment("最后登陆时间")]
    public DateTime? LastLoginTime { get; set; }
    /// <summary>
    /// 系统语言
    /// </summary>
    [Comment("系统语言")]
    [EnumDataType(typeof(SystemLanguageEnum))]
    public SystemLanguageEnum Language { get; set; }
}
public enum SysUserTypeEnum
{
    /// <summary>
    /// 普通用户
    /// </summary>
    Ordinary = 0,
    /// <summary>
    /// 系统管理员
    /// </summary>
    Admin = 1,
    /// <summary>
    /// 超级管理员
    /// </summary>
    SuperAdmin = 2
}