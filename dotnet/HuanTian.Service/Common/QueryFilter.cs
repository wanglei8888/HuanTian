namespace HuanTian.Service
{
    public class QueryFilter : IQueryFilter, ISingleton
    {
        /// <summary>
        /// 获取租户Id 
        /// </summary>
        /// <returns></returns>
        public long GetCurrentTenantId()
        {
            // 因为Ef core 过滤器，无法使用静态数据app.gettenantId直接使用会失效 只会读取第一遍数据
            return App.GetTenantId();
        }
    }
}
