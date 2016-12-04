using System;
using System.Collections.Generic;
using System.Text;
using MyGameServer.Net;
using MyGameServer.Core;
using OpenTK;
using MyGameServer.Service;

namespace MyGameServer
{
    class NetPlayerController
    {
        private readonly NetWorker _networker = Network.NetWorker;
        private readonly Dictionary<string, PlayerAction> _playerActions = new Dictionary<string,PlayerAction>(); 

        public delegate void PlayerAction(Player player, Room room);

        public NetPlayerController()
        {
            _playerActions["mvl"] = MoveLeft;
            _playerActions["mvr"] = MoveRight;
            _playerActions["mvu"] = MoveUp;
            _playerActions["mvd"] = MoveDown;

            _playerActions["shl"] = ShootLeft;
            _playerActions["shr"] = ShootRight;
            _playerActions["shu"] = ShootUp;
            _playerActions["shd"] = ShootDown;

            _playerActions["tll"] = TurnLeverLeft;
            _playerActions["tlr"] = TurnLeverRight;
        }

        public void Control(Player player, Room room)
        {

            var message = Encoding.UTF8.GetString(_networker.Receive());
            var strings = message.Split('/');
            foreach (var t in strings)
            {
                if (_playerActions.ContainsKey(t))
                {
                    //Console.WriteLine(t);
                    _playerActions[t].Invoke(player, room);
                }
            }
            
            //Console.WriteLine(message);
        }

        #region Methods

        private void MoveLeft(Person player, Room room)
        {
            Console.WriteLine("I try to move left");
            var direction = new Vector2(-1, 0);
            if (player.CanMove(direction, room))
            {
                player.Move(direction);              
            }
        }

        private void MoveRight(Person player, Room room)
        {
            var direction = new Vector2(1, 0);
            if (player.CanMove(direction, room))
            {
                player.Move(direction);
            }
        }

        private void MoveUp(Person player, Room room)
        {
            var direction = new Vector2(0, 1);
            if (player.CanMove(direction, room))
            {
                player.Move(direction);
            }
        }

        private void MoveDown(Person player, Room room)
        {
            var direction = new Vector2(0, -1);
            if (player.CanMove(direction, room))
            {
                player.Move(direction);
            }
        }

        private void ShootLeft(Player player, Room room)
        {
            var direction = new Vector2(-1, 0);
            player.TurnLeft();
            player.Shoot(room, new Shot(player.XAttack, player.YAttack, direction,
                player));
        }

        private void ShootRight(Player player, Room room)
        {
            player.TurnRight();
            var direction = new Vector2(1, 0);
            player.Shoot(room, new Shot(player.XAttack, player.YAttack, direction,
                player));
        }

        private void ShootUp(Player player, Room room)
        {
            var direction = new Vector2(0, 1);
            player.Shoot(room, new Shot(player.XAttack, player.YAttack, direction,
                player));
        }

        private void ShootDown(Player player, Room room)
        {
            var direction = new Vector2(0, -1);
            player.Shoot(room, new Shot(player.XAttack, player.YAttack, direction,
                player));
        }

        private void Shoot(Player player, Room room)
        {
            var w = GameInfo.Width;
            var h = GameInfo.Height;
            var pos = OpenTK.Input.Mouse.GetCursorState();
            var x = -1.0f * w / h + 2.0f * pos.X / h;
            var y = 1.0f - 2.0f * pos.Y / h;

            if (x - player.XAttack < 0)
            {
                player.TurnLeft();
            }
            else
            {
                player.TurnRight();
            }

            var direction = new Vector2(x - player.XAttack, y - player.YAttack);
            player.Shoot(room, new Shot(player.XAttack, player.YAttack, direction,
                player));
        }

        private void TurnLeverLeft(Player player, Room room)
        {
            if (room is ChallengeRoom)
            {
                var chalRoom = room as ChallengeRoom;
                foreach (var t in chalRoom.Levers)
                {
                    if (new IntersectionDeterminant().IsIntersected(t.InteractionForm, player.Form))
                    {
                        t.TurnLeft();
                    }
                }
            }
        }

        private void TurnLeverRight(Player player, Room room)
        {
            if (room is ChallengeRoom)
            {
                var chalRoom = room as ChallengeRoom;
                foreach (var t in chalRoom.Levers)
                {
                    if (new IntersectionDeterminant().IsIntersected(t.InteractionForm, player.Form))
                    {
                        t.TurnRight();
                    }
                }
            }
        }

        

        #endregion

    }
}
