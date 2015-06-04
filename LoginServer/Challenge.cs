using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnCMaster.LoginServer
{
    class Challenge
    {
        public static byte[] GenerateServerChallenge(Client client)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var list = Enumerable.Repeat(0, 10).Select(x=>chars[random.Next(chars.Length)]);
            var Challenge = string.Join("", list);

            client.ServerChallenge = Challenge;

            string message = String.Format(@"\lc\1\challenge\{0}\id\1\final\", client.ServerChallenge);
            return Encoding.Default.GetBytes(message);
        }
    }
}
