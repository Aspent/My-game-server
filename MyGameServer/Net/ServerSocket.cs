using System.Net;
using System.Net.Sockets;

namespace MyGameServer.Net
{
    class ServerSocket
    {
        private readonly Socket _socket;


        public ServerSocket()
        {
            var ipep = new IPEndPoint(IPAddress.Any, 30322);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.Bind(ipep);
        }

        public ServerSocket(Socket serverSocket)
        {
            _socket = serverSocket;
        }

        public Socket Socket
        {
            get { return _socket; }
        }
    }
}
