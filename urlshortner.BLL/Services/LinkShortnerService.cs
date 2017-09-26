using System;
using System.Collections.Generic;
using System.Linq;
using urlshortner.BLL.Interfaces;

namespace urlshortner.BLL.Services
{
    public class LinkShortnerService : ILinkShortnerService
    {
        const string Base62Characters = "U0Zsnk1uGBO2Dt3LrRo4vpl5qFHW6MxdQ7Sjh8CTN9cKXiIezYaAbJVmgPwfEy";
        private int ConvertionBase = Base62Characters.Length;

        const int IdToKeyOffset = 500000;

        public LinkShortnerService() { }

        public string Encode(long input)
        {
            char[] baseCharacters = Base62Characters.ToCharArray();
            var result = new Stack<char>();

            input = input + IdToKeyOffset;
            while (input != 0)
            {
                result.Push(baseCharacters[input % ConvertionBase]);
                input /= ConvertionBase;
            }
            return (new string(result.ToArray()));
        }

        public int Decode(string input)
        {
            var reversed = input.ToLower().Reverse();
            int result = 0;
            int characterPosition = 0;

            foreach (char c in reversed)
            {
                result += Base62Characters.IndexOf(c) * (int)Math.Pow(ConvertionBase, characterPosition);
                characterPosition++;
            }
            return (result - IdToKeyOffset);
        }

        public string GetShortLink(int id, String hostString, int port)
        {
            string shortLink;

            shortLink = "http://" + hostString + ":" + port + "/" + Encode(id);
            return shortLink;
        }
    }
}
