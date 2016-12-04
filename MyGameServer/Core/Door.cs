using System.Drawing;

namespace MyGameServer.Core
{
    class Door
    {
        #region Fields

        readonly Room _nextRoom;
        readonly RectangleF _form;
        readonly int _texture;
        readonly int _closedTexture;
        bool _isLocked = true;
        readonly string _position;

        #endregion

        #region Constructors

        public Door(Room thisRoom, Room nextRoom, string position)
        {
            _nextRoom = nextRoom;
            var form = thisRoom.Form;
            if(position == "top")
            {
                _form = new RectangleF(form.Left + form.Width/2 - 215.0f/3000, form.Top, 430.0f / 3000,
                    -thisRoom.Border.Height);
                _texture = 10;
                _closedTexture = 82;
            }
            if (position == "bot")
            {
                _form = new RectangleF(form.Left + form.Width / 2 - 215.0f / 3000, form.Bottom + thisRoom.Border.Height
                    , 430.0f / 3000, -thisRoom.Border.Height);
                _texture = 11;
                _closedTexture = 83;
            }
            if (position == "left")
            {
                _form = new RectangleF(form.Left, form.Bottom - form.Height/2 + 266.0f/3000,
                    thisRoom.Border.Height, -430.0f / 3000);
                _texture = 12;
                _closedTexture = 84;
            }
            if (position == "right")
            {
                _form = new RectangleF(form.Right - thisRoom.Border.Height, form.Bottom - form.Height / 2 + 266.0f / 3000,
                     thisRoom.Border.Height, -430.0f / 3000);
                _texture = 13;
                _closedTexture = 85;
            }
            
            _position = position;
        }

        #endregion

        #region Properties

        public RectangleF Form
        {
            get { return _form; }
        }

        public Room NextRoom
        {
            get { return _nextRoom; }
        }

        public bool IsLocked
        {
            get { return _isLocked; }
            set { _isLocked = value; }
        }

        public int Texture
        {
            get { return _texture; }
        }

        public int ClosedTexture
        {
            get { return _closedTexture; }
        }

        public string Position
        {
            get { return _position; }
        }

        #endregion
    }
}
