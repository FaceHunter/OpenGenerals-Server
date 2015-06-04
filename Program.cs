using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CnCMaster
{
    class Program
    {
        public static void PrintInfo()
        {
            DateTime StartTime = DateTime.Now;

            while (true)
            {
                Console.Clear();

                Console.WriteLine("=========== CnCMaster v0.0.1 ===========\n");
                Console.WriteLine("Current clients: {0}", LoginServer.LoginServer.clients.Count);
                //Console.WriteLine("Current clients ingame: " + 0);
                //Console.WriteLine("Current servers online: " + 0);
                Console.WriteLine("Start time: " + StartTime);

                Console.WriteLine("\n\n=========== Log ===========\n");
                foreach (string S in Log.logBuffer)
                    Console.WriteLine(S);

                System.Threading.Thread.Sleep(5000);
            }

        }

        static void Main(string[] args)
        {
            new Thread(new ThreadStart(PrintInfo)).Start();
            Log.Initialize("cnc.txt", LogLevel.All, false);
            LoginServer.LoginServer.Start(29900);
            new Thread(new ThreadStart(LoginServer.LoginServer.StartAccept)).Start();
        }
    }
}
