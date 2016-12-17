namespace MyGameServer.Core
{
    class NetCommand
    {
        protected byte[] _command;

        public byte[] Command
        {
            get { return _command; }
        }
    }
}
