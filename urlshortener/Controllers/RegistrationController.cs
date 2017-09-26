using System.Web.Mvc;
using urlshortener.Models;
using urlshortner.BLL.Interfaces;
using urlshortner.Entities.Models;

namespace urlshortener.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserService userService;
        
        public RegistrationController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Register", "Registration");
        }

        public ActionResult Register()
        {
            return View(new UserViewModel { });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                if (userService.GetUser(user.Email) == null)
                {
                    userService.AddUser(new UserModel { Nickname = user.Nickname, Email = user.Email, Password = user.Password });
                    return RedirectToAction("Login", "Registration");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "");
            } 
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new UserLoginModel{ });
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel user)
        {
            int loggedUserId;
            string loggedUserNickname = string.Empty;

            if (ModelState.IsValid)
            {
                if ((loggedUserId = userService.LogIn(out loggedUserNickname, new UserModel { Nickname = user.Nickname, Email = user.Email, Password = user.Password })) != -1)
                {
                    Session["Nickname"] = loggedUserNickname;
                    Session["UserID"] = loggedUserId;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
                ModelState.AddModelError("", "");
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}