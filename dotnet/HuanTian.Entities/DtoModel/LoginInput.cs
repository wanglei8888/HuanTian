namespace HuanTian.Entities
{
    public class LoginInput
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string? username { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string? password { get; set; }
        
    }
    public class LoginOutput: SysUserInfoDO
    { 
        public string? Token { get; set; }
        public new string? Password { get; set; }
    }
}