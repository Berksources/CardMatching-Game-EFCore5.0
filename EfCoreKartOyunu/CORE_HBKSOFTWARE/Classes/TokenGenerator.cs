using CORE_HBKSOFTWARE.Interfaces;
using System;
using System.Linq;

namespace CORE_HBKSOFTWARE.Classes
{
    public class TokenGenerator : ITokenGenerator
    {
        public string GetToken(string code)
        {
            string authToken = "";
            if (code == null)
            {
                var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                var resultToken = new string(
                   Enumerable.Repeat(allChar, 48).Select(token => token[random.Next(token.Length)]).ToArray());
                authToken = resultToken.ToString();
            }
            return (authToken);
        }
    }
}