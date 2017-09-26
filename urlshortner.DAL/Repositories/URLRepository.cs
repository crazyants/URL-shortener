using urlshortner.DAL.Interfaces;
using urlshortner.Entities.Models;

namespace urlshortner.DAL.Repositories
{
    public class URLRepository : RepositoryBase<URLModel>, IURLRepository
    {
        public URLRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface IURLRepository : IRepository<URLModel>
    {

    }
}
