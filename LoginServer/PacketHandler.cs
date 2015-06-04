using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CnCMaster.LoginServer
{
    class PacketHandler
    {
        public static void Handle(Client client, List<KeyValuePair<string, string>> pack)
        {
            Log.Data("Got " + pack[0].Key + " package!");
            Dictionary<string, string> args = new Dictionary<string,string>();

            foreach(KeyValuePair<string, string> kvp in pack)
            {
                args.Add(kvp.Key, kvp.Value);
            }

            switch(pack[0].Key)
            {
                case "newuser":
                    client.Email = args["email"];
                    client.Nick = args["nick"];
                    client.Password = args["password"];
                    client.Password_MD5 = new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(args["password"]), 0, args["password"].Length);
                    Users.AddUser(client);
                    LoginServer.Send(client, Encoding.Default.GetBytes(String.Format(@"\nur\\userid\{0}\profileid\{1}\id\1\final\", Users.UserAmount, Users.UserAmount)));
                    break;

                case "login":
                    string response = 
                    ushort session = Session.GenerateSession(args["user"]);
            }
        }
    }
}
