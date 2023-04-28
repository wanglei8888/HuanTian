using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuanTian.Entities;
/// <summary>
/// 用户信息表
/// </summary>
[Comment("用户信息表")]
public class SysUserInfoDO
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
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
    [Required]
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
    [Required]
    [MaxLength(50)]
    [Comment("电话")]
    public string? Telephone { get; set; }
    /// <summary>
    /// 最后登陆IP
    /// </summary>
    [Required]
    [MaxLength(100)]
    [Comment("最后登陆IP")]
    public string? LastLoginIp { get; set; }
    /// <summary>
    /// 最后登陆时间
    /// </summary>
    [Required]
    [Comment("最后登陆时间")]
    public DateTime LastLoginTime { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    [Required]
    [MaxLength(100)]
    [Comment("创建人")]
    public string? CreatorId { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [Required]
    [Comment("创建时间")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// 是否删除
    /// </summary>
    [Required]
    [Comment("是否删除")]
    public int Deleted { get; set; }
    /// <summary>
    /// 权限ID
    /// </summary>
    [Required]
    [MaxLength(200)]
    [Comment("权限ID")]
    public string? RoleId { get; set; }
    /// <summary>
    /// 系统语言
    /// </summary>
    [Required]
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
