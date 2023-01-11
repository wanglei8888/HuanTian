using AutoMapper;
using HuanTian.Domain;

namespace HuanTian.DtoModel
{
    public class LoginInput
    {
        public string? password { get; set; }
        public string? username { get; set; }
         
    }
    public class LoginOutput: UserInfoDO
    { 
        public string? token { get; set; }
    }
    public class LoginProfile : Profile
    {
        public LoginProfile() {
            CreateMap<DbModel, ViewModel>();
            CreateMap<UserInfoDO, LoginOutput>();
               // .ForMember(x=>x.RoleId,b=> b.MapFrom(q => q.Deleted));
        }
        
    }
    public class DbModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class ViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}