using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace urlshortner.BLL.Services
{
    public class LinkShortner
    {
        private int _id;
        //const string characterString = "UZsnkuGBODtLrRovplqFHWMxdQSjhCTNcKXiIezYaAbJVmgPwfEy";
        const string characterString = "qwertyuiopasdfghjklzxcvbnm1234567890";
        int convertBase = characterString.Length;
        private string _host;
        private string _port;

        public LinkShortner(string host, int port)
        {
            _host = host;
            _port = port.ToString();
        }

        public LinkShortner(int id)
        {
            _id = id;
        }

        public LinkShortner() { }

        /// <summary>
        /// Encode the given number into a convertBase base string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Encode(long input)
        {
            if (input < 0)
                throw new ArgumentOutOfRangeException("input", input, "input cannot be negative");

            char[] clistarr = characterString.ToCharArray();
            var result = new Stack<char>();
            while (input != 0)
            {
                result.Push(clistarr[input % convertBase]);
                input /= convertBase;
            }
            return new string(result.ToArray());
        }

        /// <summary>
        /// Decode the convertBase value base Encoded string into a number
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int Decode(string input)
        {
            var reversed = input.ToLower().Reverse();
            int result = 0;
            int pos = 0;
            foreach (char c in reversed)
            {
                result += characterString.IndexOf(c) * (int)Math.Pow(convertBase, pos);
                pos++;
            }
            return (result);
        }

        public string GetShortLink(int id)
        {
            string shortLink;

            shortLink = "http://" + _host + ":" + _port + "/" + Encode(id);
            return shortLink;
        }
    }
}
