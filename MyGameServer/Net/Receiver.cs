using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using MyGameServer.Core;

namespace MyGameServer.Net
{
    class Receiver
    {
        //private string _command;
        private readonly ServerSocket _socket;
        private EndPoint _remote = new IPEndPoint(IPAddress.Any, 0);
        private readonly Dictionary<Player, EndPoint> _playerRemotes;
        private readonly Dictionary<EndPoint, Player> _remotesPlayers;
        private readonly HashSet<EndPoint> _remotes;
        private readonly Dictionary<Player, string> _commands;
        private int _id;

        public Receiver(ServerSocket socket)
        {
            _socket = socket;
            _id = 0;
        }

        public Receiver(ServerSocket socket, Dictionary<Player, EndPoint> playerRemotes, 
            Dictionary<EndPoint, Player> remotesPlayers, HashSet<EndPoint> remotes, Dictionary<Player,
                string> commands)
        {
            _socket = socket;
            _playerRemotes = playerRemotes;
            _remotesPlayers = remotesPlayers;
            _remotes = remotes;
            _commands = commands;
            _id = 0;
        }


        public void Receive()
        {
            while (true)
            {
                var data = new byte[1000];
                
                _socket.Socket.ReceiveFrom(data, ref _remote);
                var cmd = Encoding.UTF8.GetString(data).Split('/');
                var command = Encoding.UTF8.GetString(data);
                if (_remotes.Contains(_remote))
                {
                    var player = _remotesPlayers[_remote];
                    _commands[player] = command;
                    
                }
                else
                {
                    if (cmd[0] == "connect")
                    {
                        Console.WriteLine("connected");
                        _remotes.Add(_remote);
                        var player = new Player(_id, -0.4f, 0.0f, 359.0f/4000, 982.0f/4000, 0.4f/60, 1000, 86, 87,
                            new ShotCharacteristics("Fireball.f"), 70, 29, 0.4f/60, 0.5f, "Boy");
                        _id++;

                        _playerRemotes[player] = _remote;
                        _remotesPlayers[_remote] = player;
                        //foreach (var t in Players.GamePlayers)
                        //{
                        //    NetCommandBuilder.AppendToRoomCommand(t, "player_add/" + player.X + "/" + player.Y + "/" +
                        //         player.Width + "/" + player.Height + "/" + player.Texture);
                        //}
                        Players.GamePlayers.Add(player);
                        NetCommandBuilder.RoomCommands[player] = new StringBuilder();
                        NetCommandBuilder.LevelCommands[player] = new StringBuilder();
                        NetCommandBuilder.GameCommands[player] = new StringBuilder();
                    }
                }
            }


        }

    }
}
