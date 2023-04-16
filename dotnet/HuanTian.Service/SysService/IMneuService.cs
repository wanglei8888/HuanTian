using HuanTian.Entities;

namespace HuanTian.Service
{
    public interface IMneuService
    {
        public Task<dynamic> GetUserMenu(int userId);
    }
}
