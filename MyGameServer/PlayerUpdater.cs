using System;
using MyGameServer.Core;

namespace MyGameServer
{
    class PlayerUpdater
    {
        public void Update(Player player)
        {
            Network.NetWorker.Send(BitConverter.GetBytes(player.X));
            Network.NetWorker.Send(BitConverter.GetBytes(player.Y));
        }
    }
}
