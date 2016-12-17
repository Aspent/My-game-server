using System;
using System.Net;
using System.Net.Sockets;

namespace MyGameServer.Net
{
    class NetWorker
    {
        private readonly Socket _serverSocket;
        private EndPoint _remote;

        public NetWorker(Socket serverSocket)
        {
            //var ipep = new IPEndPoint(IPAddress.Any, 30322);
            //_serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //_serverSocket.Bind(ipep);
            _serverSocket = serverSocket;

            //var sender = new IPEndPoint(IPAddress.Any, 0);

            //_remote = sender;
            //_remote = remote;
        }

        public NetWorker()
        {
            var ipep = new IPEndPoint(IPAddress.Any, 30322);
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _serverSocket.Bind(ipep);
            //_serverSocket = serverSocket;

            var sender = new IPEndPoint(IPAddress.Any, 0);
            _remote = sender;
        }

        public void Send(byte[] data)
        {
            _serverSocket.SendTo(data, _remote);
        }

        public void SendTo(byte[] data, EndPoint remote)
        {
            Console.WriteLine(data.Length);
            _serverSocket.SendTo(data, remote);
        }

        public byte[] Receive()
        {
            var data = new byte[1000];
            _serverSocket.ReceiveFrom(data, ref _remote);
            
            return data;
        }  
    }
}
