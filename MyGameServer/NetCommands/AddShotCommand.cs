using System;
using MyGameServer.Core;

namespace MyGameServer.NetCommands 
{
    class AddShotCommand : NetCommand
    {
        public AddShotCommand(float x, float y, float width, float height, int texture)
        {
            _command = new byte[42];
            _command[0] = 0;
            _command[1] = 3;
            BitConverter.GetBytes(x).CopyTo(_command, 6);
            BitConverter.GetBytes(y).CopyTo(_command, 14);
            BitConverter.GetBytes(width).CopyTo(_command, 22);
            BitConverter.GetBytes(height).CopyTo(_command, 30);
            BitConverter.GetBytes(texture).CopyTo(_command, 38);
        }

        public AddShotCommand(Shot shot)
        {
            _command = new byte[42];
            _command[0] = 0;
            _command[1] = 3;
            BitConverter.GetBytes(shot.X).CopyTo(_command, 6);
            BitConverter.GetBytes(shot.Y).CopyTo(_command, 14);
            BitConverter.GetBytes(shot.Width).CopyTo(_command, 22);
            BitConverter.GetBytes(shot.Height).CopyTo(_command, 30);
            BitConverter.GetBytes(shot.Texture).CopyTo(_command, 38);
        }
    }
}
