using System;
using MyGameServer.Core;

namespace MyGameServer.NetCommands
{
    class AddPlayerCommand : NetCommand
    {
        public AddPlayerCommand(float x, float y, float width, float height, int texture)
        {
            _command = new byte[42];
            _command[0] = 0;
            _command[1] = 6;
            BitConverter.GetBytes(x).CopyTo(_command, 6);
            BitConverter.GetBytes(y).CopyTo(_command, 14);
            BitConverter.GetBytes(width).CopyTo(_command, 22);
            BitConverter.GetBytes(height).CopyTo(_command, 30);
            BitConverter.GetBytes(texture).CopyTo(_command, 38);
        }

        public AddPlayerCommand(Player player)
        {
            _command = new byte[42];
            _command[0] = 0;
            _command[1] = 6;
            BitConverter.GetBytes(player.X).CopyTo(_command, 6);
            BitConverter.GetBytes(player.Y).CopyTo(_command, 14);
            BitConverter.GetBytes(player.Width).CopyTo(_command, 22);
            BitConverter.GetBytes(player.Height).CopyTo(_command, 30);
            BitConverter.GetBytes(player.Texture).CopyTo(_command, 38);
        }
    }
}
