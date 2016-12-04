using MyGameServer.Core;
using OpenTK;

namespace MyGameServer
{
    class DefaultEnemyController : IEnemyController
    {
        private readonly Enemy _enemy;
        private readonly Room _room;

        public DefaultEnemyController(Enemy enemy, Room room)
        {
            _enemy = enemy;
            _room = room;
        }

        public void Control()
        {
            var player = _room.Player;
            if (_enemy.IsWaiting) return;
            var distance = new Vector2(player.X - _enemy.X, player.Y - _enemy.Y).Length;
            var direction = new Vector2(player.X - _enemy.X, player.Y - _enemy.Y);
            if (direction.X < 0)
            {
                _enemy.TurnLeft();
            }
            if (direction.X > 0)
            {
                _enemy.TurnRight();
            }
            if (distance > 0.8f * _enemy.ShotRange)
            {
                if (_enemy.CanMove(direction, _room))
                {
                    _enemy.Move(direction);
                }
            }
            else
            {
                direction = new Vector2(player.X - _enemy.XAttack, player.YAttack - _enemy.Y);
                _enemy.Shoot(_room, new Shot(_enemy.XAttack, _enemy.YAttack, direction, _enemy));
            }
        }
    }
}
