using System;
using MyGameServer.Core;

namespace MyGameServer.NetCommands
{
    class AddItemCommand : NetCommand
    {
        public AddItemCommand(float x, float y, float width, float height, int texture)
        {
            _command = new byte[42];
            _command[0] = 0;
            _command[1] = 9;
            BitConverter.GetBytes(x).CopyTo(_command, 6);
            BitConverter.GetBytes(y).CopyTo(_command, 14);
            BitConverter.GetBytes(width).CopyTo(_command, 22);
            BitConverter.GetBytes(height).CopyTo(_command, 30);
            BitConverter.GetBytes(texture).CopyTo(_command, 38);
        }

        public AddItemCommand(Item item)
        {
            _command = new byte[42];
            _command[0] = 0;
            _command[1] = 9;
            BitConverter.GetBytes(item.Form.X).CopyTo(_command, 6);
            BitConverter.GetBytes(item.Form.Y).CopyTo(_command, 14);
            BitConverter.GetBytes(item.Form.Width).CopyTo(_command, 22);
            BitConverter.GetBytes(item.Form.Height).CopyTo(_command, 30);
            BitConverter.GetBytes(item.Texture).CopyTo(_command, 38);
        }
    }
}
