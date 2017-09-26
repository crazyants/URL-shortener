namespace urlshortener.Controllers
{
    public interface ICookieManager
    {
        string InitCookies();
        void ChangeCookie(string newValue);
        void AddToCookie(string valToAdd);
        string GetCookie();
        void ClearCookie();
    }
}
