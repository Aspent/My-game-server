using System.Collections.Generic;
using MyGameServer.Core;
using MyGameServer.Service;
using OpenTK;
using OpenTK.Input;

namespace MyGameServer
{
    class PlayerController
    {
        #region Fields

        readonly List<Command> _commands = new List<Command>();

        #endregion
                      
        #region Constructors

        public PlayerController()
        {
            var movingLeft = new Command(new List<Key>(), new List<Command.PlayerAction>());
            movingLeft.Keys.Add(Key.A);
            movingLeft.Actions.Add(MoveLeft);
            
            var movingRight = new Command(new List<Key>(), new List<Command.PlayerAction>());
            movingRight.Keys.Add(Key.D);
            movingRight.Actions.Add(MoveRight);

            var movingUp = new Command(new List<Key>(), new List<Command.PlayerAction>());
            movingUp.Keys.Add(Key.W);
            movingUp.Actions.Add(MoveUp);

            var movingDown = new Command(new List<Key>(), new List<Command.PlayerAction>());
            movingDown.Keys.Add(Key.S);
            movingDown.Actions.Add(MoveDown);

            var shootingLeft = new Command(new List<Key>(), new List<Command.PlayerAction>());
            shootingLeft.Keys.Add(Key.Left);
            shootingLeft.Actions.Add(ShootLeft);

            var shootingRight = new Command(new List<Key>(), new List<Command.PlayerAction>());
            shootingRight.Keys.Add(Key.Right);
            shootingRight.Actions.Add(ShootRight);

            var shootingUp = new Command(new List<Key>(), new List<Command.PlayerAction>());
            shootingUp.Keys.Add(Key.Up);
            shootingUp.Actions.Add(ShootUp);

            var shootingDown = new Command(new List<Key>(), new List<Command.PlayerAction>());
            shootingDown.Keys.Add(Key.Down);
            shootingDown.Actions.Add(ShootDown);

            var turningLeverLeft = new Command(new List<Key>(), new List<Command.PlayerAction>());
            turningLeverLeft.Keys.Add(Key.Q);
            turningLeverLeft.Actions.Add(TurnLeverLeft);

            var turningLeverRight = new Command(new List<Key>(), new List<Command.PlayerAction>());
            turningLeverRight.Keys.Add(Key.E);
            turningLeverRight.Actions.Add(TurnLeverRight);

            _commands.Add(movingLeft);
            _commands.Add(movingRight);
            _commands.Add(movingUp);
            _commands.Add(movingDown);
            _commands.Add(shootingLeft);
            _commands.Add(shootingRight);
            _commands.Add(shootingUp);
            _commands.Add(shootingDown);
            _commands.Add(turningLeverLeft);
            _commands.Add(turningLeverRight);    
        }

        #endregion

        #region Methods

        private void MoveLeft(Person player, Room room)
        {
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

        public void Control(Player player, Room room)
        {
            var state = OpenTK.Input.Keyboard.GetState();
            var st = OpenTK.Input.Mouse.GetState();
            if (st[MouseButton.Left])
            {
                Shoot(player, room);
            }
            bool _allKeys = true;
            foreach(var t in _commands)
            {
                foreach (var key in t.Keys)
                {
                    if (!state[key])
                    {
                        _allKeys = false;
                    }
                }
                if(_allKeys)
                {
                    foreach(var act in t.Actions)
                    {
                        act.Invoke(player, room);
                    }
                    
                }
                _allKeys = true;
            }
        }

        #endregion
    }
}
