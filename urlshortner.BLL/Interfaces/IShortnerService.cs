using urlshortner.Entities.Models;
using System.Collections.Generic;

namespace urlshortner.BLL.Interfaces
{
    public interface IShortnerService
    {
        IEnumerable<URLModel> GetURLs(string sessionUserId, string cookieValues);
        IEnumerable<URLModel> GetURLsByUserID(int id);
        URLModel GetURLByID(int id);
        string GetURLForRedirect(int id);
        void AdjustClickCounter(int id);
        int AddShortenedURLToLast(string host, int port);
        int AddURL(URLModel url, string host, int port, string sessionUserId);
        void SaveURL();
    }
}
