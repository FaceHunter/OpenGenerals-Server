using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnCMaster.LoginServer
{
    class Parser
    {
        public static List<KeyValuePair<string, string>> Parse(string Message)
        {
            List<KeyValuePair<string, string>> Output = new List<KeyValuePair<string, string>>();
            string[] keyvalues = Message.Substring(1).Split('\\');

            for(int i = 0; i < keyvalues.Length; i+=2)
            {
                KeyValuePair<string, string> tmp = new KeyValuePair<string, string>(keyvalues[i], keyvalues[i + 1]);
                Log.Debug(String.Format("{0} -> {1}", tmp.Key, tmp.Value));
                Output.Add(tmp);
            }

            return Output;
        }
    }
}
