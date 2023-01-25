using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuanTian.Interface
{
    public interface IMneuService
    {
        public Task<IEnumerable<string>> GetUserMenu(int userId);
    }
}
