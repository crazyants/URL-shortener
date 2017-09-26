using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using urlshortener.Models;
using urlshortner.BLL.Interfaces;
using urlshortner.Entities.Models;

namespace urlshortener.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShortnerService ShortnerService;
        private readonly ILinkShortnerService LinkShortner;
        private readonly ICookieManager CookieController;

        public HomeController(IShortnerService shortnerService, ILinkShortnerService linkShortner, ICookieManager cookieManager)
        {
            ShortnerService = shortnerService;
            LinkShortner = linkShortner;
            CookieController = cookieManager;
        }

        public ActionResult Index()
        {
            if (Session["UserID"] == null)
                Session["UserID"] = "";
            return View();
        }

        public ActionResult URLForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult URLForm(ShortnerViewModel shortnerViewModel)
        {
            int lastId;

            if (ModelState.IsValid)
            {
                URLModel newModel = new URLModel
                {
                    LongURL = shortnerViewModel.SingleURL.LongURL,
                    ClickCount = 0
                };
                lastId = ShortnerService.AddURL(newModel, HttpContext.Request.Url.Host, HttpContext.Request.Url.Port, Session["UserID"].ToString());
                if (string.IsNullOrEmpty(Session["UserID"].ToString()))
                {
                    CookieController.AddToCookie($".{lastId}");
                }
                ShortnerService.SaveURL();
            }
            else
                ModelState.AddModelError("", "");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult GetList()
        {
            IEnumerable<URLModel> links = ShortnerService.GetURLs(Session["UserID"].ToString(), CookieController.GetCookie());
            
            if (links == null)
                return PartialView(new List<URLModel>());
            return PartialView(links);
        }

        public ActionResult ActionLinkRedirect(int id)
        {
            ShortnerService.AdjustClickCounter(id);
            return Redirect(ShortnerService.GetURLForRedirect(id));
        }

        public ActionResult RedirectToLong(string shortURL)
        {
            int tmpId;

            tmpId = LinkShortner.Decode(shortURL);
            ShortnerService.AdjustClickCounter(tmpId);
            return Redirect(ShortnerService.GetURLForRedirect(tmpId));
        }
    }
}