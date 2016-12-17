using MyGameServer.Net;

namespace MyGameServer.Core
{
    static class Network
    {
        private static ServerSocket serverSocket = new ServerSocket();
        private static readonly NetWorker _netWorker = new NetWorker(serverSocket.Socket);
        //private static readonly NetWorker _netWorker = new NetWorker();

        public static NetWorker NetWorker
        {
            get { return _netWorker; }
        }

        public static ServerSocket ServerSocket
        {
            get { return serverSocket; }
        }
    }
}
