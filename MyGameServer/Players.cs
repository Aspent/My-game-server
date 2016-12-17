using System.Collections.Generic;
using MyGameServer.Core;

namespace MyGameServer
{
    static class Players
    {
        static List<Player>  _players = new List<Player>();

        public static List<Player> GamePlayers
        {
            get { return _players; }
        }
    }
}
