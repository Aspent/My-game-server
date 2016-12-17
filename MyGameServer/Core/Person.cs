using System.Drawing;
using System.Timers;
using OpenTK;

namespace MyGameServer.Core
{
    class Person
    {
        #region Fields
        
        protected float _x;
        protected float _y;
        protected float _height;
        protected float _width;
        protected float _speed;
        protected int _leftTexture;
        protected int _rightTexture;
        protected int _currentTexture;
        protected bool _canShoot;
        protected int _hp;
        protected int _maxHp;
        protected double _attackSpeed;
        protected bool _isDead;
        protected ShotCharacteristics _shotChar;
        protected int _damage;
        protected float _shotSpeed;
        protected float _shotRange;
        protected string _name;
        protected float _xAttack;
        protected float _yAttack;
        bool _turnedLeft = true;
        Timer _timer = new Timer();

        #endregion

        #region Constructors

        public Person()
        {

        }

        public Person(float x, float y, float width, float height, float speed, double attackSpeed,
            int leftTexture, int rightTexture, ShotCharacteristics shotChar, int hp, int damage, float shotSpeed,
            float shotRange, string name)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _speed = speed;
            _leftTexture = leftTexture;
            _rightTexture = rightTexture;
            _currentTexture = leftTexture;
            _attackSpeed = attackSpeed;
            _canShoot = true;
            _isDead = false;
            _shotChar = shotChar;
            _hp = hp;
            _maxHp = hp;
            _damage = damage;
            _shotSpeed = shotSpeed;
            _shotRange = shotRange;
            _name = name;
            _xAttack = Form.Left;
            _yAttack = Form.Bottom - Form.Height / 2;

            _timer.Interval = _attackSpeed;
            _timer.AutoReset = false;
            _timer.Elapsed += OnTimedEvent;
        }

        #endregion

        #region Properties

        public float X
        {
            get { return _x; }
        }

        public float Y
        {
            get { return _y; }
        }

        public float Width
        {
            get { return _width; }
        }

        public float Height
        {
            get { return _height; }
        }

        public RectangleF Form
        {
            get { return new RectangleF(_x, _y, _width, -_height); }
        }

        public float Speed
        {
            get { return _speed; }
        }

        public int Texture
        {
            get { return _currentTexture; }
        }

        public int LeftTexture
        {
            get { return _leftTexture; }
        }

        public int RightTexture
        {
            get { return _rightTexture; }
        }

        public int Hp
        {
            get { return _hp; }
        }

        public bool IsDead
        {
            get { return _isDead; }
            set { _isDead = value; }
        }

        public ShotCharacteristics ShotChar
        {
            get { return _shotChar; }
        }

        public int MaxHp
        {
            get { return _maxHp; }
        }

        public int Damage
        {
            get { return _damage; }
        }

        public float ShotSpeed
        {
            get { return _shotSpeed; }
        }

        public float ShotRange
        {
            get { return _shotRange; }
        }

        public double AttackSpeed
        {
            get { return _attackSpeed; }
        }

        public string Name
        {
            get { return _name; }
        }

        public float XAttack
        {
            get
            {
                if (_turnedLeft)
                {
                    return Form.Left;
                }
                else
                {
                    return Form.Right - _shotChar.Width;
                }
            }
        }

        virtual public float YAttack
        {
            get { return Form.Bottom - Form.Height / 2; }
        }

        public bool CanShoot
        {
            get { return _canShoot; }
            set { _canShoot = value; }
        }

        #endregion

        #region Methods

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            _canShoot = true;
        }

        public virtual bool CanMove(Vector2 direction, Room room)
        {
            return true;
        }

        public void Move(Vector2 direction)
        {
            direction.Normalize();
            _x += _speed * direction.X;
            _y += _speed * direction.Y;

        }

        public void MoveTo(float x, float y)
        {
            _x = x;
            _y = y;            
        }

        public void Shoot(Room room, Shot shot)
        {
            //Console.WriteLine(_canShoot);
            if (!_canShoot) return;
            room.Shots.Add(shot);
            foreach (var player in room.Players)
            {
                NetCommandBuilder.AppendToRoomCommand(player, "shot_add/" + shot.X + "/" + shot.Y + "/" + shot.Form.Width
                                     + "/" + shot.Form.Height + "/" + shot.Texture);
            }
            
            _canShoot = false;
            _timer.Interval = _attackSpeed;
            _timer.Elapsed += OnTimedEvent;
            _timer.Start();
        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;
        }

        public void UpMaxHealth()
        {
            _maxHp++;
            _hp++;
        }

        public void UpHealth()
        {
            if (_hp < _maxHp)
            {
                _hp++;
            }
        }

        public void UpSpeed()
        {
            _speed += 0.1f/60;
        }

        public void UpDamage()
        {
            _damage += 1;
        }

        public void UpRange()
        {
            _shotRange += 0.1f;
        }

        public void UpAttackSpeed()
        {
            _attackSpeed -= 200;
        }

        public void TurnLeft()
        {
            _currentTexture = _leftTexture;
            _turnedLeft = true;
            _shotChar.TurnLeft();

        }

        public void TurnRight()
        {
            _currentTexture = _rightTexture;
            _turnedLeft = false;
            _shotChar.TurnRight();

        }

        #endregion
    }
}
