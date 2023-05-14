namespace HuanTian.Entities
{
    public class LoginInput
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string? Password { get; set; }
        
    }
    public class LoginOutput
    { 
        public string? Token { get; set; }
    }
}