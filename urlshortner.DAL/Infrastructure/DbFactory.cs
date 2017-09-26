using urlshortner.DAL.Interfaces;

namespace urlshortner.DAL.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory 
    {
        DatabaseContext Context;

        public DatabaseContext Init()
        {
            return Context ?? (Context = new DatabaseContext());
        }

        protected override void DisposeCore()
        {
            if (Context != null)
                Context.Dispose();
        }
    }
}
