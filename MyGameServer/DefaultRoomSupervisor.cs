using System.Text;
using MyGameServer.Core;
using MyGameServer.Service;

namespace MyGameServer
{
    class DefaultRoomSupervisor : IRoomSupervisor
    {
        private readonly Room _room;

        public DefaultRoomSupervisor(Room room)
        {
            _room = room;
            OnShotBorderCollision += ShotBorderHandle;
            OnShotPlayerCollision += ShotPlayerHandle;
            OnShotEnemyCollision += ShotEnemyHandle;
            OnPlayerItemCollision += PlayerItemHandle;
        }

        #region Events

        public event RoomSupervisor.ShotBorderHandler OnShotBorderCollision;

        public event RoomSupervisor.ShotPlayerHandler OnShotPlayerCollision;

        public event RoomSupervisor.ShotEnemyHandler OnShotEnemyCollision;

        public event RoomSupervisor.PlayerItemHandler OnPlayerItemCollision;

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
            //item.Effect.Invoke(player, _game);
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

        public void Run()
        {
            var player = _room.Player;
            foreach (var t in _room.Enemies)
            {
                if (!t.IsWaiting)
                {
                    _room.EnemyControllers[t].Control();
                    NetCommandBuilder.AppendToRoomCommand("enemy_move/" + _room.Enemies.IndexOf(t) + "/" 
                        +  t.X + "/" + t.Y + "/" + t.Texture);

                    //if (t is Boss)
                    //{
                    //    var bEnemy = t as Boss;
                    //    var bRoom = _room as BossRoom;
                    //    if (bEnemy.CanUseSkill && bEnemy.Hp < bEnemy.MaxHp / 2)
                    //    {
                    //        bEnemy.UseSkill(bEnemy, bRoom);
                    //    }
                    //}
                }
            }

            if (_room.SummonedEnemies.Count != 0)
            {
                foreach (var t in _room.SummonedEnemies)
                {
                    t.IsWaiting = false;
                    _room.Enemies.Add(t);
                    NetCommandBuilder.AppendToRoomCommand("enemy_add/" + t.X + "/" + t.Y + "/" +
                                             t.Form.Width + "/" + t.Form.Height + "/" + t.Texture);
                }
                _room.SummonedEnemies.Clear();
            }


            foreach (var t in _room.Shots)
            {
                t.Move();
                NetCommandBuilder.AppendToRoomCommand("shot_move/" + _room.Shots.IndexOf(t) + "/" 
                    + t.X + "/" + t.Y + "/"  + t.Texture);
            }
            foreach (var t in _room.Shots)
            {
                if (ShotIsRemoved(t))
                {
                    NetCommandBuilder.AppendToRoomCommand("shot_remove/" + _room.Shots.IndexOf(t));
                }
            }
            _room.Shots.RemoveAll(ShotIsRemoved);

            var collisionChecker = new CollisionChecker();
            foreach (var t in _room.Shots)
            {
                if (collisionChecker.IsCollided(t, _room))
                {
                    OnShotBorderCollision?.Invoke(t, _room);
                }
                if (collisionChecker.IsCollided(t, player))
                {
                    if (t.Owner.GetType() != player.GetType())
                    {
                        OnShotPlayerCollision?.Invoke(t, player);
                    }
                }
                foreach (var item in _room.Enemies)
                {
                    if (collisionChecker.IsCollided(t, item))
                    {
                        if (t.Owner != item)
                        {
                            OnShotEnemyCollision?.Invoke(t, item);
                        }
                    }
                }
            }

            foreach (var t in _room.Items)
            {
                if (collisionChecker.IsCollided(player, t) && t.IsAvailable)
                {
                    OnPlayerItemCollision?.Invoke(player, t);
                }
            }
            foreach (var t in _room.Shots)
            {
                if (ShotIsRemoved(t))
                {
                    NetCommandBuilder.AppendToRoomCommand("shot_remove/" + _room.Shots.IndexOf(t));
                }
            }
            _room.Shots.RemoveAll(ShotIsRemoved);
            foreach (var t in _room.Enemies)
            {
                if (EnemyIsDead(t))
                {
                    NetCommandBuilder.AppendToRoomCommand("enemy_remove/" + _room.Enemies.IndexOf(t));
                }
            }
            _room.Enemies.RemoveAll(EnemyIsDead);
            _room.Items.RemoveAll(ItemIsPicked);
            //Network.NetWorker.Send(Encoding.UTF8.GetBytes(NetCommandBuilder.Command));

        }

        #endregion
    }
}
