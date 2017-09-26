using urlshortner.DAL.Interfaces;

namespace urlshortner.DAL.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory Factory;
        private DatabaseContext context;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.Factory = dbFactory;
        }

        public DatabaseContext Context
        {
            get { return context ?? (context = Factory.Init()); }
        }

        public void Commit()
        {
            Context.Commit();
        }
    }
}
