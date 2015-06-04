using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CnCMaster
{
    class Users
    {
        static Dictionary<string, Client> users = new Dictionary<string, Client>();

        public static int UserAmount
        {
            get { return users.Count; }
        }

        public static void AddUser(Client client)
        {
            users.Add(client.Nick, client);
        }
    }
}
