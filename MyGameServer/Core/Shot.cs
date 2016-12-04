using System.Drawing;
using OpenTK;

namespace MyGameServer.Core
{
    class Shot
    {
        #region Fields

        float _x;
        float _y;
        Vector2 _direction;
        bool _isRemoved;
        readonly Person _owner;
        float _range;
        int _damage;
        float _speed;


        #endregion

        #region Constructors

        public Shot(float x, float y, Vector2 direction, Person owner)
        {
            _x = x;
            _y = y;
            _direction = direction;
            _isRemoved = false;
            _owner = owner;
            _range = owner.ShotRange;
            _damage = owner.Damage;
            _speed = owner.ShotSpeed;
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
            get { return _owner.ShotChar.Width; }
        }

        public float Height
        {
            get { return _owner.ShotChar.Height; }
        }

        public RectangleF Form
        {
            get { return new RectangleF(_x, _y, Width, -Height); }
        }

        public float Speed
        {
            get { return _owner.ShotSpeed; }
        }

        public int Texture
        {
            get { return _owner.ShotChar.Texture; }
        }

        public float Range
        {
            get { return _range; }
        }

        public int Damage
        {
            get { return _owner.Damage; }
        }

        public bool IsRemoved
        {
            get { return _isRemoved; }
            set { _isRemoved = value; }
        }

        public Person Owner
        {
            get { return _owner; }
        }

        #endregion

        #region Methods

        public void Move()
        {
            _direction.Normalize();
            _x += Speed * _direction.X;
            _y += Speed * _direction.Y;
            _range -= Speed;
        }

        #endregion
    }
}
