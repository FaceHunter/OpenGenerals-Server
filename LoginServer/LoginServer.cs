using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading;

namespace CnCMaster.LoginServer
{
    class LoginServer
    {
        private static TcpListener listener;
        public static List<Client> clients = new List<Client>();

        public static int Serial = 0;

        public static void Start(int _port)
        {
            listener = new TcpListener(IPAddress.Any, _port);
            Log.Info("Listening on port " + _port);
            listener.Start();
        }

        public static void StartAccept()
        {
            listener.BeginAcceptTcpClient(AcceptCallBack, listener);
        }

        private static void AcceptCallBack(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;
            Socket clientSock = listener.EndAcceptSocket(ar);

            Log.Info("Accepting socket from " + clientSock.RemoteEndPoint);

            Client client = new Client();
            client.ClientSocket = clientSock;

            clients.Add(client);

            Send(client, Challenge.GenerateServerChallenge(client));


            clientSock.BeginReceive(client.buffer, 0, client.buffer.Length, SocketFlags.None, ReceiveCallback, client);
            listener.BeginAcceptSocket(AcceptCallBack, listener);

        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            Client client = (Client)ar.AsyncState;
            int bytes;
            try
            {
                bytes = client.ClientSocket.EndReceive(ar);
            }
            catch (Exception E)
            {
                Close(client);
                return;
            }

            if (bytes > 0)
            {
                string data = Encoding.Default.GetString(client.buffer, 0, bytes);
                string hex = ByteArrayToString(Encoding.Default.GetBytes(data));


                Log.Debug("Received: " + data);

                PacketHandler.Handle(client, Parser.Parse(data));

                client.buffer = new byte[1024];
                client.ClientSocket.BeginReceive(client.buffer, 0, client.buffer.Length, SocketFlags.None, ReceiveCallback, client);
            }
            else
            {
                Close(client);
            }

        }

        private static void SendCallBack(IAsyncResult ar)
        {
            Client client = (Client)ar.AsyncState;
            int sendBytes = client.ClientSocket.EndSend(ar);
            //Log.Debug("Send " + sendBytes.ToString() + " bytes!");
        }

        public static void Close(Client client)
        {
            clients.Remove(client);
            //Log.Debug("Closing client " + client.ID);

            client.ClientSocket.Close();
        }

        public static void Send(Client client, byte[] bytes)
        {
            client.ClientSocket.BeginSend(bytes, 0, bytes.Length, SocketFlags.None, SendCallBack, client);
        }

        public static void CleanupThread()
        {
            while (true)
            {
                Thread.Sleep(10000);
                Serial++;
                if (clients.Count == 0)
                    continue;

            }
        }

        public static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "").ToLower();
        }
    }
}
