﻿using System;
using MyGameServer.Core;

namespace MyGameServer.NetCommands
{
    class RemoveShotCommand : NetCommand
    {
        public RemoveShotCommand(int index)
        {
            _command = new byte[6];
            _command[0] = 0;
            _command[1] = 5;
            BitConverter.GetBytes(index).CopyTo(_command, 2);
        }
    }
}
