using System;
using System.Diagnostics;
using System.Web;
using urlshortener.Controllers;

namespace urlshortener
{
    public class CookieManager : ICookieManager
    {
        HttpCookie Cookie;
        private string CookieName;
        private int CookieDuration;

        public CookieManager()
        {
            CookieName = "URLs";
            CookieDuration = 7;
        }

        public string InitCookies()
        {
            Cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (Cookie == null)
            {
                Cookie = new HttpCookie(CookieName)
                {
                    Value = "",
                    Expires = DateTime.Today.AddDays(CookieDuration)
                };
                HttpContext.Current.Response.SetCookie(Cookie);
            }
            return Cookie.Value;
        }

        public void ChangeCookie(string newValue)
        {
            InitCookies();
            if (Cookie != null)
            {
                Cookie.Value = newValue;
                Cookie.Expires = DateTime.Today.AddDays(CookieDuration);
                HttpContext.Current.Response.SetCookie(Cookie);
            }
        }

        public void AddToCookie(string valToAdd)
        {
            InitCookies();
            if (Cookie != null)
            {
                Cookie.Value += valToAdd;
                Cookie.Expires = DateTime.Today.AddDays(CookieDuration);
                HttpContext.Current.Response.SetCookie(Cookie);
            }
        }

        public string GetCookie()
        {
            InitCookies();
            return Cookie.Value.ToString();
        }

        public void ClearCookie()
        {
            Cookie.Value = "";
            HttpContext.Current.Response.SetCookie(Cookie);
        }
    }
}