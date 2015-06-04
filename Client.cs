using System;
using System.Net.Sockets;

namespace CnCMaster
{
    class Client
    {
        public Socket ClientSocket;
        public byte[] buffer = new byte[1024];
        public ClientState state;
        public string UserChallenge;
        public string ServerChallenge;

        public string Nick;
        public string Email;
        public string Password;
        public string Password_MD5;
    }

    enum ClientState
    {
        AwaitingChallenge,
        ReceivedChallenge
    }
}
