using System.Collections.Generic;
using System.Net;
using MyGameServer.Core;

namespace MyGameServer
{
    class PlayerUpdater
    {
        private Dictionary<Player, EndPoint> _playersRemotes;

        public PlayerUpdater(Dictionary<Player, EndPoint> playersRemotes)
        {
            _playersRemotes = playersRemotes;
        }

        public void Update(Player player)
        {
            //Network.NetWorker.SendTo(BitConverter.GetBytes(player.X), _playersRemotes[player]);
            //Network.NetWorker.SendTo(BitConverter.GetBytes(player.Y), _playersRemotes[player]);
            //NetCommandBuilder.AppendToRoomCommand(player, "player_move/" + );
        }
    }
}
