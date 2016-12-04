using MyGameServer.Core;
using MyGameServer.Service;

namespace MyGameServer
{
    class RoomSupervisor
    {
        #region Fields

        readonly Game _game;

        #endregion

        #region Constructors

        public RoomSupervisor(Game game)
        {
            _game = game;
            OnShotBorderCollision += ShotBorderHandle;
            OnShotPlayerCollision += ShotPlayerHandle;
            OnShotEnemyCollision += ShotEnemyHandle;
            OnPlayerItemCollision += PlayerItemHandle;
        }

        #endregion

        #region Events

        public event ShotBorderHandler OnShotBorderCollision;

        public event ShotPlayerHandler OnShotPlayerCollision;

        public event ShotEnemyHandler OnShotEnemyCollision;

        public event PlayerItemHandler OnPlayerItemCollision;



        #endregion

        #region Handle Methods

        private void ShotBorderHandle(Shot shot, Room room)
        {
            shot.IsRemoved = true;
        }

        private void ShotPlayerHandle(Shot shot, Player player)
        {
            player.TakeDamage(shot.Damage);
            shot.IsRemoved = true;
        }

        private void ShotEnemyHandle(Shot shot, Enemy enemy)
        {
            if (shot.Owner is Player)
            {
                enemy.TakeDamage(shot.Damage);              
            }
            shot.IsRemoved = true;
        }


        private void PlayerItemHandle(Player player, Item item)
        {
            item.Effect.Invoke(player, _game);
            item.IsPicked = true;
        }

        #endregion

        #region Delegates

        public delegate void ShotBorderHandler(Shot shot, Room room);

        public delegate void ShotPlayerHandler(Shot shot, Player player);

        public delegate void ShotEnemyHandler(Shot shot, Enemy enemy);

        public delegate void PlayerFinishZoneHandler(Player player, FinishZone finishZone);

        public delegate void PlayerItemHandler(Player player, Item item);

        #endregion

        #region Methods

        private static bool ShotIsRemoved(Shot shot)
        {
            return (shot.Range <= 0 || shot.IsRemoved);
        }

        private static bool EnemyIsDead(Enemy enemy)
        {
            return (enemy.Hp <= 0);
        }

        private static bool ItemIsPicked(Item item)
        {
            return item.IsPicked;
        }

        public void Run(Player player, Room room)
        {
            foreach (var t in room.Enemies)
            {
                if (!t.IsWaiting)
                {
                    //var distance = new Vector2(player.X - t.X, player.Y - t.Y).Length;
                    //var direction = new Vector2(player.X - t.X, player.Y - t.Y);
                    ////var direction = new Vector2(t.X - player.X, t.Y - player.Y);
                    //if (direction.X < 0)
                    //{
                    //    t.TurnLeft();
                    //}
                    //if (direction.X > 0)
                    //{
                    //    t.TurnRight();
                    //}
                    //if (distance > 0.8f * t.ShotRange)
                    //{
                    //    if(t.CanMove(direction, room))
                    //    {
                    //        t.Move(direction);
                    //    }
                    //}
                    //else
                    //{
                    //    direction = new Vector2(player.X - t.XAttack, player.YAttack - t.Y);
                    //    t.Shoot(room, new Shot(t.XAttack, t.YAttack, direction, t));
                    //}

                    

                    if(t is Boss)
                    {
                        var bEnemy = t as Boss;
                        var bRoom = room as BossRoom;
                        if (bEnemy.CanUseSkill && bEnemy.Hp < bEnemy.MaxHp / 2) 
                        {
                            bEnemy.UseSkill(bEnemy, bRoom);
                        }
                    }
                }
            }

            if(room.SummonedEnemies.Count != 0)
            {
                foreach(var t in room.SummonedEnemies)
                {
                    t.IsWaiting = false;
                    room.Enemies.Add(t);
                }
                room.SummonedEnemies.Clear();
            }

            
            foreach (var t in room.Shots)
            {
                t.Move();
            }
            room.Shots.RemoveAll(ShotIsRemoved);

            var collisionChecker = new CollisionChecker();
            foreach (var t in room.Shots)
            {
                if (collisionChecker.IsCollided(t, room))
                {
                    OnShotBorderCollision(t, room);
                }
                if (collisionChecker.IsCollided(t, player))
                {
                    if (t.Owner.GetType() != player.GetType())
                    {
                        OnShotPlayerCollision?.Invoke(t, player);
                    }
                }
                foreach(var item in room.Enemies)
                {
                    if (collisionChecker.IsCollided(t, item))
                    {
                        if (t.Owner != item)
                        {
                            OnShotEnemyCollision(t, item);
                        }
                    }
                }
            }

            foreach (var t in room.Items)
            {
                if(collisionChecker.IsCollided(player, t) && t.IsAvailable)
                {
                    OnPlayerItemCollision(player, t);
                }
            }

            if(room is BossRoom)
            {
                var bRoom = room as BossRoom;
                if(collisionChecker.IsCollided(player, bRoom.FinishZone))
                {
                    if(bRoom.FinishZone.IsActive)
                    {
                        if (bRoom.Boss.IsFinal)
                        {
                            //_game.Win();
                        }
                        else
                        {
                            _game.GoToNextLevel();                           
                        }
                    }
                }
            }

            if (room is ChallengeRoom)
            {
                var chalRoom = room as ChallengeRoom;
                if(collisionChecker.IsCollided(player, chalRoom.Note))
                {
                    //new TaskDrawer(_game.Textures).Draw(chalRoom.Note);
                }
                
            }

            room.Shots.RemoveAll(ShotIsRemoved);
            room.Enemies.RemoveAll(EnemyIsDead);
            room.Items.RemoveAll(ItemIsPicked);

        }

        #endregion
    }
}
