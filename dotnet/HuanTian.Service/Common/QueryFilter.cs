namespace HuanTian.Service
{
    public class QueryFilter : IQueryFilter, IScoped
    {
        public long GetCurrentTenantId()
        {
            return App.GetTenantId();
        }
    }
}
