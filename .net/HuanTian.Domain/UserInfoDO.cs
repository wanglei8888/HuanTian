﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace HuanTian.Domain;
/// <summary>
/// 用户信息表
/// </summary>
[Table("user_info")]
[Description("用户信息表")]

public class UserInfoDO
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID { get; set; }
    /// <summary>
    /// 名字
    /// </summary>
    [Required]
    [Column("name")]
    [MaxLength(50)]
    [Description("名字")]
    public string? Name { get; set; }
    /// <summary>
    /// 名字
    /// </summary>
    [Required]
    [Column("avatar")]
    [MaxLength(200)]
    [Description("图片路径")]
    public string? Avatar { get; set; }
    /// <summary>
    /// 用户名
    /// </summary>
    [Required]
    [Column("user_name")]
    [MaxLength(50)]
    [Description("用户名")]
    public string? UserName { get; set; }
    /// <summary>
    /// 用户密码
    /// </summary>
    [Required]
    [Column("password")]
    [MaxLength(50)]
    [Description("用户密码")]
    public string? Password { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    [Required]
    [Column("status")]
    [Description("状态")]
    public UserStatus Status { get; set; }
    /// <summary>
    /// 电话
    /// </summary>
    [Required]
    [Column("telephone")]
    [MaxLength(50)]
    [Description("电话")]
    public string? Telephone { get; set; }
    /// <summary>
    /// 最后登陆IP
    /// </summary>
    [Required]
    [Column("last_login_ip")]
    [MaxLength(100)]
    [Description("最后登陆IP")]
    public string? LastLoginIp { get; set; }
    /// <summary>
    /// 最后登陆时间
    /// </summary>
    [Required]
    [Column("last_login_time")]
    [Description("最后登陆时间")]
    public DateTime LastLoginTime { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    [Required]
    [Column("creator_id")]
    [MaxLength(100)]
    [Description("创建人")]
    public string? CreatorId { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    [Required]
    [Column("create_time")]
    [Description("创建时间")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// 是否删除
    /// </summary>
    [Required]
    [Column("deleted")]
    [Description("是否删除")]
    public int Deleted { get; set; }
    /// <summary>
    /// 权限ID
    /// </summary>
    [Required]
    [Column("role_id")]
    [MaxLength(200)]
    [Description("权限ID")]
    public string? RoleId { get; set; }
    /// <summary>
    /// 系统语言
    /// </summary>
    [Required]
    [Column("lang")]
    [Description("系统语言")]
    public SystemLangguage Langguage { get; set; }
}
public enum UserStatus
{
    /// <summary>
    /// 启用
    /// </summary>
    [Description("启用")]
    Enable,
    /// <summary>
    /// 禁用
    /// </summary>
    [Description("禁用")]
    UnEnable
}
public enum SystemLangguage
{
    /// <summary>
    /// 中文
    /// </summary>
    [Description("中文")]
    Chinese,
    /// <summary>
    /// 英文
    /// </summary>
    [Description("英文")]
    English
}
