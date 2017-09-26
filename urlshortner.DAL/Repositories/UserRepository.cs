using urlshortner.DAL.Interfaces;
using urlshortner.Entities.Models;

namespace urlshortner.DAL.Repositories
{
    public class UserRepository : RepositoryBase<UserModel>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
    }

    public interface IUserRepository : IRepository<UserModel>
    {

    }
}
