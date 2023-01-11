using HuanTian.Interface;

namespace HuanTian.Service
{
    public class TestService : ITestService
    {
        public int AddNum(int a, int b)
        {
            return a + b;
        }
    }
}