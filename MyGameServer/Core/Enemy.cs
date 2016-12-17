using System;
using System.IO;
using System.Timers;
using MyGameServer.Service;
using OpenTK;

namespace MyGameServer.Core
{
    class Enemy : Person
    {
        #region Fields

        protected bool _isWaiting;
        protected Timer _timer;

        #endregion

        #region Constructors

        public Enemy(float x, float y, float width, float height, float speed, double attackSpeed, 
            int leftTexture, int rightTexture, ShotCharacteristics shotChar, int hp, int damage,
            float shotSpeed, float shotRange, string name) 
            : base(x, y, width, height, speed, attackSpeed, leftTexture, rightTexture, shotChar, hp, damage, 
            shotSpeed, shotRange, name)
        {
            _isWaiting = true;
            _timer = new Timer(1000);
            _isWaiting = true;
            _timer.AutoReset = false;
            _timer.Elapsed += OnTimedEvent;

        }

        public Enemy(float x, float y, string fileName)
        {
            _x = x;
            _y = y;
            var strings = File.ReadAllLines("Enemies/" + fileName);
            _width = float.Parse(strings[0]);
            _height = float.Parse(strings[1]);
            _speed = float.Parse(strings[2]);
            _attackSpeed = double.Parse(strings[3]);
            _leftTexture = int.Parse(strings[4]);
            _rightTexture = int.Parse(strings[5]);

            _currentTexture = _leftTexture;
            _shotChar = new ShotCharacteristics(strings[6] + ".f");
            
            _maxHp = int.Parse(strings[7]);
            _hp = _maxHp;
            _damage = int.Parse(strings[8]);
            _shotSpeed = float.Parse(strings[9]);
            _shotRange = float.Parse(strings[10]);
            _name = strings[11];    
            _canShoot = true;
            _isDead = false;

            _isWaiting = true;
            _timer = new Timer(1000);
            _isWaiting = true;
            _timer.AutoReset = false;
            _timer.Elapsed += OnTimedEvent;
        }

        #endregion

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            _isWaiting = false;
        }

        #region Properties

        public bool IsWaiting
        {
            get { return _isWaiting; }
            set { _isWaiting = value; }
        }

        public Timer Timer
        {
            get { return _timer; }
        }

        #endregion

        #region Methods

        public override bool CanMove(Vector2 direction, Room room)
        {
            var collisionChecker = new CollisionChecker();
            direction.Normalize();
            var movedEnemy = new Enemy(_x + _speed * direction.X, _y + _speed * direction.Y,
                _width, _height, _speed, _attackSpeed, _leftTexture, _rightTexture, _shotChar, _hp, _damage,
                _shotSpeed, _shotRange, _name);
            if (collisionChecker.IsCollided(movedEnemy, room))
            {
                return false;
            }

            foreach (var t in room.Enemies)
            {
                if (collisionChecker.IsCollided(movedEnemy, t) && t != this)
                {
                    return false;
                }
            }

            if (collisionChecker.IsCollided(room.Players[0], movedEnemy))
            {
                return false;
            }



            return base.CanMove(direction, room);
        }


        #endregion
    }
}
