using System;
using MyGameServer.Core;

namespace MyGameServer.NetCommands
{
    class AddEnemyCommand : NetCommand
    {
        public AddEnemyCommand(float x, float y, float width, float height, int texture)
        {
            _command = new byte[42];
            _command[0] = 0;
            _command[1] = 0;
            BitConverter.GetBytes(x).CopyTo(_command, 6);
            BitConverter.GetBytes(y).CopyTo(_command, 14);
            BitConverter.GetBytes(width).CopyTo(_command, 22);
            BitConverter.GetBytes(height).CopyTo(_command, 30);
            BitConverter.GetBytes(texture).CopyTo(_command, 38);
        }

        public AddEnemyCommand(Enemy enemy)
        {
            _command = new byte[42];
            _command[0] = 0;
            _command[1] = 0;
            BitConverter.GetBytes(enemy.X).CopyTo(_command, 6);
            BitConverter.GetBytes(enemy.Y).CopyTo(_command, 14);
            BitConverter.GetBytes(enemy.Width).CopyTo(_command, 22);
            BitConverter.GetBytes(enemy.Height).CopyTo(_command, 30);
            BitConverter.GetBytes(enemy.Texture).CopyTo(_command, 38);
        }
    }
}
