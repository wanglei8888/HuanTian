using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace HuanTian.Entities;
/// <summary>
/// 用户信息表
/// </summary>
[Comment("用户信息表")]
public class SysUserInfoDO:BaseEntityBusiness
{
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
    /// 状态
    /// </summary>
    [Required]
    [Comment("状态")]
    public UserStatus Status { get; set; }
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
    public DateTime LastLoginTime { get; set; }
    /// <summary>
    /// 权限ID
    /// </summary>
    [MaxLength(200)]
    [Comment("权限ID")]
    public string? RoleId { get; set; }
    /// <summary>
    /// 系统语言
    /// </summary>
    [Comment("系统语言")]
    public SystemLanguage Language { get; set; }
}
public enum UserStatus
{
    /// <summary>
    /// 启用
    /// </summary>
    [Comment("启用")]
    Enable,
    /// <summary>
    /// 禁用
    /// </summary>
    [Comment("禁用")]
    UnEnable
}
public enum SystemLanguage
{
    /// <summary>
    /// 中文
    /// </summary>
    [Comment("中文")]
    Chinese,
    /// <summary>
    /// 英文
    /// </summary>
    [Comment("英文")]
    English
}
