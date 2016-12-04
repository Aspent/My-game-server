using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameServer.Core;
using OpenTK;

namespace MyGameServer
{
    class BossCancerController : IEnemyController
    {
        private readonly Enemy _boss;
        private readonly Room _room;

        public BossCancerController(Enemy enemy, Room room)
        {
            _boss = enemy;
            _room = room;
        }

        public void Control()
        {
            var player = _room.Player;
            if (_boss.IsWaiting) return;
            var distance = new Vector2(player.X - _boss.X, player.Y - _boss.Y).Length;
            var direction = new Vector2(player.X - _boss.X, player.Y - _boss.Y);
            if (direction.X < 0)
            {
                _boss.TurnLeft();
            }
            if (direction.X > 0)
            {
                _boss.TurnRight();
            }
            if (distance > 0.8f * _boss.ShotRange)
            {
                if (_boss.CanMove(direction, _room))
                {
                    _boss.Move(direction);
                }
            }
            else
            {
                direction = new Vector2(player.X - _boss.XAttack, player.YAttack - _boss.Y);
                _boss.Shoot(_room, new Shot(_boss.XAttack, _boss.YAttack, direction, _boss));
            }
            var bEnemy = _boss as Boss;            
            if (bEnemy.CanUseSkill && bEnemy.Hp < bEnemy.MaxHp / 2)
            {
                bEnemy.UseSkill(bEnemy, _room);
            }
        }
    }
}
