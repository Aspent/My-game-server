using MyGameServer.Core;
using MyGameServer.Service;

namespace MyGameServer
{
    class DefaultLevelSupervisor : ILevelSupervisor
    {
        private readonly Level _level;
        private readonly NetPlayerController _controller = new NetPlayerController();
        public DefaultLevelSupervisor(Level level)
        {
            _level = level;
        }

        public void Run()
        {
            var currentRoom = _level.CurrentRoom;
            var player = currentRoom.Player;
            _controller.Control(player, currentRoom);
            _level.RoomSupervisors[currentRoom].Run();
            
            //player.Controller.Control(player, currentRoom);
            //_controller.Control(player, currentRoom);
            new PlayerUpdater().Update(player);
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
                    currentRoom.Shots.Clear();
                    //currentRoom = currentRoom.TopDoor.NextRoom;
                    _level.CurrentRoom = currentRoom.TopDoor.NextRoom;
                    //NetCommandBuilder.AppendToLevelCommand("change_room/" + _level.Rooms.IndexOf(_level.CurrentRoom));
                    _level.CurrentRoom.Player = player;
                    currentRoom = _level.CurrentRoom;
                    player.MoveTo(currentRoom.BotDoor.Form.Left, currentRoom.BotDoor.Form.Top + 0.25f);
                    //player.MoveTo(0, 0);
                    foreach (var t in currentRoom.Enemies)
                    {
                        t.Timer.Start();
                    }
                }
            }
            if ((currentRoom.BotDoor != null) && (collisionChecker.IsCollided(player, currentRoom.BotDoor)))
            {
                if (!currentRoom.BotDoor.IsLocked)
                {
                    currentRoom.Shots.Clear();
                    //currentRoom = currentRoom.BotDoor.NextRoom;
                    _level.CurrentRoom = currentRoom.BotDoor.NextRoom;
                    //currentRoom.Player = player;
                    _level.CurrentRoom.Player = player;
                    currentRoom = _level.CurrentRoom;
                    player.MoveTo(currentRoom.TopDoor.Form.Left, currentRoom.TopDoor.Form.Bottom);
                    foreach (var t in currentRoom.Enemies)
                    {
                        t.Timer.Start();
                    }
                }
            }
            if ((currentRoom.LeftDoor != null) && (collisionChecker.IsCollided(player, currentRoom.LeftDoor)))
            {
                if (!currentRoom.LeftDoor.IsLocked)
                {
                    currentRoom.Shots.Clear();
                    //currentRoom = currentRoom.LeftDoor.NextRoom;
                    _level.CurrentRoom = currentRoom.LeftDoor.NextRoom;
                    //currentRoom.Player = player;
                    _level.CurrentRoom.Player = player;
                    currentRoom = _level.CurrentRoom;
                    player.MoveTo(currentRoom.RightDoor.Form.Left - player.Form.Width - 0.005f, currentRoom.RightDoor.Form.Top);
                    foreach (var t in currentRoom.Enemies)
                    {
                        t.Timer.Start();
                    }
                }
            }
            if ((currentRoom.RightDoor != null) && (collisionChecker.IsCollided(player, currentRoom.RightDoor)))
            {
                if (!currentRoom.RightDoor.IsLocked)
                {
                    currentRoom.Shots.Clear();
                    //currentRoom = currentRoom.RightDoor.NextRoom;
                    _level.CurrentRoom = currentRoom.RightDoor.NextRoom;
                    //currentRoom.Player = player;
                    _level.CurrentRoom.Player = player;
                    currentRoom = _level.CurrentRoom;
                    player.MoveTo(currentRoom.LeftDoor.Form.Right, currentRoom.LeftDoor.Form.Top);
                    foreach (var t in currentRoom.Enemies)
                    {
                        t.Timer.Start();
                    }
                }
            }
            NetCommandBuilder.AppendToLevelCommand("change_room/" + _level.Rooms.IndexOf(_level.CurrentRoom));

        }


       
    }
}
