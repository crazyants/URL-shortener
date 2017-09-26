using urlshortner.Entities.Models;
namespace urlshortner.BLL.Interfaces
{
    public interface IUserService
    {
        void AddUser(UserModel user);
        int LogIn(out string loggedUserNickname, UserModel user);
        UserModel GetUser(int id);
        UserModel GetUser(string email);
    }
}
