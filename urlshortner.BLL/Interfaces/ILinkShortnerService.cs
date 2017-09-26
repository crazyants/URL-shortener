using System;

namespace urlshortner.BLL.Interfaces
{
    public interface ILinkShortnerService
    {
        string Encode(long input);
        int Decode(string input);
        string GetShortLink(int id, String hostString, int port);
    }
}
