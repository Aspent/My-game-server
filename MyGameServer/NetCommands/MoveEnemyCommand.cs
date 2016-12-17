using System;
using MyGameServer.Core;

namespace MyGameServer.NetCommands
{
    class MoveEnemyCommand : NetCommand
    {
        public MoveEnemyCommand(int index, float x, float y, int texture)
        {
            _command = new byte[42];
            _command[0] = 0;
            _command[1] = 1;
            BitConverter.GetBytes(index).CopyTo(_command, 2);
            BitConverter.GetBytes(x).CopyTo(_command, 6);
            BitConverter.GetBytes(y).CopyTo(_command, 14);
            BitConverter.GetBytes(texture).CopyTo(_command, 38);
        }

        public MoveEnemyCommand(int index, Enemy enemy)
        {
            _command = new byte[42];
            _command[0] = 0;
            _command[1] = 1;
            BitConverter.GetBytes(index).CopyTo(_command, 2);
            BitConverter.GetBytes(enemy.X).CopyTo(_command, 6);
            BitConverter.GetBytes(enemy.Y).CopyTo(_command, 14);
            BitConverter.GetBytes(enemy.Texture).CopyTo(_command, 38);
        }
    }
}
