using urlshortner.Entities.Models;
using urlshortner.BLL.Interfaces;
using System.Collections.Generic;
using urlshortner.DAL.Repositories;
using urlshortner.DAL.Interfaces;
using System.Linq;
using System;
using System.Diagnostics;

namespace urlshortner.BLL.Services
{
    public class ShortnerService : IShortnerService
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IURLRepository UrlRepository;
        private readonly IUserRepository UserRepository;
        private readonly ILinkShortnerService LinkShortner;

        public ShortnerService(IURLRepository urlRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, ILinkShortnerService linkShorner)
        {
            UrlRepository = urlRepository;
            UserRepository = userRepository;
            UnitOfWork = unitOfWork;
            LinkShortner = linkShorner;
        }

        public int AddShortenedURLToLast(string host, int port)
        {
            URLModel tmp;
            tmp = UrlRepository.GetAll().LastOrDefault();
            UrlRepository.Update(tmp);
            tmp.ShortURL = LinkShortner.GetShortLink(tmp.Id, host, port);
            return tmp.Id;
        }

        public int AddURL(URLModel url, string host, int port, string sessionUserId)
        {
            int lastId;

            UrlRepository.Add(url);
            if (!UrlIsValid(url.LongURL))
                url.LongURL = "http://" + url.LongURL;
            SaveURL();
            lastId = AddShortenedURLToLast(host, port);
            SaveURL();
            if (!string.IsNullOrEmpty(sessionUserId))
            {
                var tmpUserID = Convert.ToInt32(sessionUserId);
                var userFoundById = UserRepository.GetWhere(x => x.Id == tmpUserID).FirstOrDefault();

                UserRepository.Update(userFoundById);
                userFoundById.URLs.Add(url);
                UnitOfWork.Commit();
            }
            return lastId;
        }

        public void AdjustClickCounter(int id)
        {
            URLModel tmp;

            tmp = UrlRepository.GetById(id);
            UrlRepository.Update(tmp);
            tmp.ClickCount++;
            SaveURL();
        }

        public URLModel GetURLByID(int id)
        {
            var url = UrlRepository.GetById(id);

            return url;
        }

        public string GetURLForRedirect(int id)
        {
            URLModel tmp;
            string link;

            tmp = UrlRepository.GetById(id);
            link = tmp.LongURL;
            if (!UrlIsValid(link))
                link = "http://" + link;
            return link;
        }

        public IEnumerable<URLModel> GetURLs(string sessionUserId, string cookieValues)
        {
            IEnumerable<URLModel> urlsToReturn = null;
            List<int> urlsFound = new List<int>();
            string idForUrlsToFind = "";
            
            if (string.IsNullOrEmpty(sessionUserId))
            {
                idForUrlsToFind = cookieValues;
                if (!string.IsNullOrEmpty(idForUrlsToFind))
                {
                    urlsFound = StringToList(idForUrlsToFind);
                }
                urlsToReturn = UrlRepository.GetWhere(e => urlsFound.Contains(e.Id));
            }
            else
            {
                var tempID = Convert.ToInt32(sessionUserId);
                var tempUser = UserRepository.GetWhere(x => x.Id == tempID).LastOrDefault();

                urlsToReturn = tempUser.URLs;
            }
            return urlsToReturn;
        }

        public IEnumerable<URLModel> GetURLsByUserID(int id)
        {
            return UrlRepository.GetWhere(x => x.Id == id);
        }

        public void SaveURL()
        {
            UnitOfWork.Commit();
        }

        private List<int> StringToList(string str)
        {
            List<string> splited = new List<string>();
            List<int> result = new List<int>();

            splited = str.Split('.').ToList();
            for (int i = 0; i < splited.Count; i++)
                if (Int32.TryParse(splited[i], out int tmp))
                    result.Add(Convert.ToInt32(splited[i]));
            return result;
        }

        private bool UrlIsValid(string uriName)
        {
            return (Uri.TryCreate(uriName, UriKind.Absolute, out Uri uriResult)
                        && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps));
        }
    }
}