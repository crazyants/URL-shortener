using System.Security.Cryptography;
using System.Linq;
using urlshortner.BLL.Interfaces;
using urlshortner.DAL.Interfaces;
using urlshortner.DAL.Repositories;
using urlshortner.Entities.Models;

namespace urlshortner.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IURLRepository urlRepository;
        private readonly IUnitOfWork unitOfWork;
        
        public UserService(IUserRepository userRepository, IURLRepository urlRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.urlRepository = urlRepository;
            this.unitOfWork = unitOfWork;
        }

        public void AddUser(UserModel user)
        {
            byte[] tmpData = System.Text.Encoding.ASCII.GetBytes(user.Password);
            tmpData = new SHA256Managed().ComputeHash(tmpData);
            user.Password = System.Text.Encoding.ASCII.GetString(tmpData);
            userRepository.Add(user);
            SaveUser();
        }

        public int LogIn(out string loggedUserNickname, UserModel user)
        {
            UserModel tmp;
            byte[] tmpHashBytes = System.Text.Encoding.ASCII.GetBytes(user.Password);
            string tmpHashedPassword;

            loggedUserNickname = null;
            tmpHashBytes = new SHA256Managed().ComputeHash(tmpHashBytes);
            tmpHashedPassword = System.Text.Encoding.ASCII.GetString(tmpHashBytes);
            tmp = userRepository.GetWhere(x => x.Email == user.Email && x.Password == tmpHashedPassword).LastOrDefault();
            if (tmp != null)
            {
                loggedUserNickname = tmp.Nickname;
                return tmp.Id;
            }
            return -1 ;
        }

        public UserModel GetUser(int id)
        {
            return userRepository.GetWhere(x => x.Id == id).ToList().LastOrDefault();
        }

        public UserModel GetUser(string email)
        {
            return userRepository.GetWhere(x => x.Email == email).ToList().LastOrDefault();
        }

        public void SaveUser()
        {
            unitOfWork.Commit();
        }
    }
}
