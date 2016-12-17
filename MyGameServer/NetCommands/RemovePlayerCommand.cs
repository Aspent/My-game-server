using System;
using MyGameServer.Core;

namespace MyGameServer.NetCommands
{
    class RemovePlayerCommand : NetCommand
    {
        public RemovePlayerCommand(int index)
        {
            _command = new byte[6];
            _command[0] = 0;
            _command[1] = 8;
            BitConverter.GetBytes(index).CopyTo(_command, 2);
        }
    }
}
