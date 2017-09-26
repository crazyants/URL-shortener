using System;
using System.Diagnostics;
using System.Web;

namespace urlshortner.BLL.Services
{
    public class CookieController
    {
        private HttpResponseBase _res;
        private HttpRequestBase _req;
        private HttpCookie _urlCookies;
        private string _name;
        private int _durationInDays;
        private string _savedCookie;

        public CookieController(string name, int durationInDays)
        {
            _name = name;
            _durationInDays = durationInDays;
        }

        public void SetContext(HttpRequestBase req, HttpResponseBase res)
        {
            _req = req;
            _res = res;
        }

        public string InitCookies()
        {
            _urlCookies = HttpContext.Current.Request.Cookies[_name];
            if (_urlCookies == null)
            {
                _urlCookies = new HttpCookie(_name);
                _urlCookies.Value = "";
                _urlCookies.Expires = DateTime.Today.AddDays(_durationInDays);
                HttpContext.Current.Response.SetCookie(_urlCookies);
            }
            else
            {
                _savedCookie = _urlCookies.Value;
            }
            return _urlCookies.Value;
        }

        public void ChangeCookie(string newValue)
        {
            InitCookies();
            if (_urlCookies != null)
            {
                _urlCookies.Value = newValue;
                _urlCookies.Expires = DateTime.Today.AddDays(_durationInDays);
                HttpContext.Current.Response.SetCookie(_urlCookies);
            }
        }

        public void AddToCookie(string valToAdd)
        {
            InitCookies();
            if (_urlCookies != null)
            {
                _urlCookies.Value += valToAdd;
                _urlCookies.Expires = DateTime.Today.AddDays(_durationInDays);
                HttpContext.Current.Response.SetCookie(_urlCookies);
            }
        }

        public string GetCookie()
        {
            InitCookies();
            return _urlCookies.Value.ToString();
        }

        public void ClearCookie()
        {
            _urlCookies.Value = "";
            HttpContext.Current.Response.SetCookie(_urlCookies);
        }
    }
}