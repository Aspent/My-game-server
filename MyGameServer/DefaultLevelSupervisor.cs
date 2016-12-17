using System.Collections.Generic;
using System.Net;
using MyGameServer.Core;
using MyGameServer.Service;

namespace MyGameServer
{
    class DefaultLevelSupervisor : ILevelSupervisor
    {
        private readonly Level _level;
        private readonly NetPlayerController _controller = new NetPlayerController();
        private Dictionary<Player, Room> _currentRooms = new Dictionary<Player, Room>();
        //private string _command;
        private Dictionary<Player, string> _commands;
        //private Dictionary<Player, EndPoint> _playerRemotes; 

        public DefaultLevelSupervisor(Level level, Dictionary<Player, string> commands, 
            Dictionary<Player, EndPoint> playerRemotes)
        {
            _level = level;
            _commands = commands;
            //_command = command;
            var players = Players.GamePlayers;
            foreach (var t in players)
            {
                _currentRooms[t] = _level.Rooms[_level.StartRoomIndex];
            }
            //_playerRemotes = playerRemotes;

        }

        public void Run()
        {
            foreach (var player in Players.GamePlayers)
            {

                if (!_currentRooms.ContainsKey(player))
                {
                    _currentRooms[player] = _level.Rooms[_level.StartRoomIndex];
                    LoadRoom(player, _level.Rooms[_level.StartRoomIndex]);

                    foreach (var t in _level.Rooms[_level.StartRoomIndex].Players)
                    {
                        NetCommandBuilder.AppendToRoomCommand(t, "player_add/" + player.X + "/" + player.Y + "/" +
                                 player.Width + "/" + player.Height + "/" + player.Texture);
                    }

                    _level.Rooms[_level.StartRoomIndex].Players.Add(player);



                    NetCommandBuilder.AppendToRoomCommand(player, "player_add/" + player.X + "/" + player.Y + "/" +
                                 player.Width + "/" + player.Height + "/" + player.Texture);

                }
                

                var currentRoom = _currentRooms[player];
                if (_commands.ContainsKey(player))
                {
                    _controller.Control(player, currentRoom, _commands[player]);
                }
                _level.RoomSupervisors[currentRoom].Run();

                //new PlayerUpdater(_playerRemotes).Update(player);
                var collisionChecker = new CollisionChecker();
                if (currentRoom.Enemies.Count == 0 && !(currentRoom is ChallengeRoom))
                {

                    var doors = currentRoom.GetAllDoors();
                    foreach (var t in doors)
                    {
                        t.IsLocked = false;
                    }
                }


                if ((currentRoom.TopDoor != null) && (collisionChecker.IsCollided(player, currentRoom.TopDoor)))
                {
                    if (!currentRoom.TopDoor.IsLocked)
                    {
                        //currentRoom.Shots.Clear();
                        var index = currentRoom.Players.IndexOf(player);
                        currentRoom.Players.Remove(player);

                        

                        foreach (var t in currentRoom.Players)
                        {
                            NetCommandBuilder.AppendToRoomCommand(t, "player_remove/" + index);
                        }

                        currentRoom = currentRoom.TopDoor.NextRoom;
                        foreach (var t in currentRoom.Players)
                        {
                            NetCommandBuilder.AppendToRoomCommand(t, "player_add/" + player.X + "/" + player.Y + "/" +
                                 player.Width + "/" + player.Height + "/" + player.Texture);
                        }
                        LoadRoom(player, currentRoom);
                        currentRoom.Players.Add(player);
                        player.MoveTo(currentRoom.BotDoor.Form.Left, currentRoom.BotDoor.Form.Top + 0.25f);
                        _currentRooms[player] = currentRoom;
                        foreach (var t in currentRoom.Enemies)
                        {
                            t.Timer.Start();
                        }
                    }
                    NetCommandBuilder.AppendToLevelCommand(player, "change_room/" + _level.Rooms.IndexOf(currentRoom));
                }
                if ((currentRoom.BotDoor != null) && (collisionChecker.IsCollided(player, currentRoom.BotDoor)))
                {
                    if (!currentRoom.BotDoor.IsLocked)
                    {
                        //currentRoom.Shots.Clear();
                        var index = currentRoom.Players.IndexOf(player);
                        currentRoom.Players.Remove(player);
                        foreach (var t in currentRoom.Players)
                        {
                            NetCommandBuilder.AppendToRoomCommand(t, "player_remove/" + index);
                        }
                        currentRoom = currentRoom.BotDoor.NextRoom;
                        foreach (var t in currentRoom.Players)
                        {
                            NetCommandBuilder.AppendToRoomCommand(t, "player_add/" + player.X + "/" + player.Y + "/" +
                                 player.Width + "/" + player.Height + "/" + player.Texture);
                        }
                        LoadRoom(player, currentRoom);
                        currentRoom.Players.Add(player);
                        player.MoveTo(currentRoom.TopDoor.Form.Left, currentRoom.TopDoor.Form.Bottom);
                        _currentRooms[player] = currentRoom;
                        foreach (var t in currentRoom.Enemies)
                        {
                            t.Timer.Start();
                        }
                        NetCommandBuilder.AppendToLevelCommand(player, "change_room/" + _level.Rooms.IndexOf(currentRoom));
                    }
                }
                if ((currentRoom.LeftDoor != null) && (collisionChecker.IsCollided(player, currentRoom.LeftDoor)))
                {
                    if (!currentRoom.LeftDoor.IsLocked)
                    {
                        //currentRoom.Shots.Clear();
                        var index = currentRoom.Players.IndexOf(player);
                        currentRoom.Players.Remove(player);
                        foreach (var t in currentRoom.Players)
                        {
                            NetCommandBuilder.AppendToRoomCommand(t, "player_remove/" + index);
                        }
                        currentRoom = currentRoom.LeftDoor.NextRoom;
                        foreach (var t in currentRoom.Players)
                        {
                            NetCommandBuilder.AppendToRoomCommand(t, "player_add/" + player.X + "/" + player.Y + "/" +
                                 player.Width + "/" + player.Height + "/" + player.Texture);
                        }
                        LoadRoom(player, currentRoom);
                        currentRoom.Players.Add(player);
                        player.MoveTo(currentRoom.RightDoor.Form.Left - player.Form.Width - 0.005f,
                            currentRoom.RightDoor.Form.Top);
                        _currentRooms[player] = currentRoom;
                        foreach (var t in currentRoom.Enemies)
                        {
                            t.Timer.Start();
                        }
                        NetCommandBuilder.AppendToLevelCommand(player, "change_room/" + _level.Rooms.IndexOf(currentRoom));
                    }
                }
                if ((currentRoom.RightDoor != null) && (collisionChecker.IsCollided(player, currentRoom.RightDoor)))
                {
                    if (!currentRoom.RightDoor.IsLocked)
                    {
                       // currentRoom.Shots.Clear();
                        var index = currentRoom.Players.IndexOf(player);
                        currentRoom.Players.Remove(player);
                        foreach (var t in currentRoom.Players)
                        {
                            NetCommandBuilder.AppendToRoomCommand(t, "player_remove/" + index);
                        }
                        currentRoom = currentRoom.RightDoor.NextRoom;
                        foreach (var t in currentRoom.Players)
                        {
                            NetCommandBuilder.AppendToRoomCommand(t, "player_add/" + player.X + "/" + player.Y + "/" +
                                 player.Width + "/" + player.Height + "/" + player.Texture);
                        }
                        LoadRoom(player, currentRoom);
                        currentRoom.Players.Add(player);
                        player.MoveTo(currentRoom.LeftDoor.Form.Right, currentRoom.LeftDoor.Form.Top);
                        _currentRooms[player] = currentRoom;
                        foreach (var t in currentRoom.Enemies)
                        {
                            t.Timer.Start();
                        }
                        NetCommandBuilder.AppendToLevelCommand(player, "change_room/" + _level.Rooms.IndexOf(currentRoom));
                    }
                }
                //NetCommandBuilder.AppendToLevelCommand(player, "change_room/" + _level.Rooms.IndexOf(currentRoom));
            }

        }

        public void LoadRoom(Player player, Room room)
        {
            NetCommandBuilder.RoomCommands[player].Clear();
            var currentRoom = room;
            foreach (var t in currentRoom.Enemies)
            {
                NetCommandBuilder.AppendToRoomCommand(player, "enemy_add/" + t.X + "/" + t.Y + "/" +
                                 t.Width + "/" + t.Height + "/" + t.Texture);
            }
            foreach (var shot in currentRoom.Shots)
            {
                NetCommandBuilder.AppendToRoomCommand(player, "shot_add/" + shot.X + "/" + shot.Y + "/"
                    + shot.Form.Width + "/" + shot.Form.Height + "/" + shot.Texture);
            }
            foreach (var t in currentRoom.Players)
            {
                NetCommandBuilder.AppendToRoomCommand(player, "player_add/" + t.X + "/" + t.Y + "/" +
                                 t.Width + "/" + t.Height + "/" + t.Texture);
            }
        }


       
    }
}
