using MyGameServer.Net;

namespace MyGameServer.Core
{
    static class Network
    {
        private static readonly NetWorker _netWorker = new NetWorker();

        public static NetWorker NetWorker
        {
            get { return _netWorker; }
        }
    }
}
