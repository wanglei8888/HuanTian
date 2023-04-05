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
        public string? token { get; set; }
    }
    //public class LoginProfile : Profile
    //{
    //    public LoginProfile() {
    //        CreateMap<SysUserInfoDO, LoginOutput>();
    //           // .ForMember(x=>x.RoleId,b=> b.MapFrom(q => q.Deleted));
    //    }
        
    //}
}