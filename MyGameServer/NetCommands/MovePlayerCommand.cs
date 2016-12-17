﻿using System;
using MyGameServer.Core;

namespace MyGameServer.NetCommands
{
    class MovePlayerCommand : NetCommand
    {
        public MovePlayerCommand(int index, float x, float y, int texture)
        {
            _command = new byte[42];
            _command[0] = 0;
            _command[1] = 7;
            BitConverter.GetBytes(index).CopyTo(_command, 2);
            BitConverter.GetBytes(x).CopyTo(_command, 6);
            BitConverter.GetBytes(y).CopyTo(_command, 14);
            BitConverter.GetBytes(texture).CopyTo(_command, 38);
        }

        public MovePlayerCommand(int index, Player player)
        {
            _command = new byte[42];
            _command[0] = 0;
            _command[1] = 7;
            BitConverter.GetBytes(index).CopyTo(_command, 2);
            BitConverter.GetBytes(player.X).CopyTo(_command, 6);
            BitConverter.GetBytes(player.Y).CopyTo(_command, 14);
            BitConverter.GetBytes(player.Texture).CopyTo(_command, 38);
        }
    }
}
