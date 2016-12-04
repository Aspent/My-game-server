using System.IO;

namespace MyGameServer.Core
{
    class ShotCharacteristics
    {
        #region Fields

        readonly float _width;
        readonly float _height;
        readonly int _leftTexture;
        readonly int _rightTexture;
        readonly string _name;
        int _currentTexture;

        #endregion

        #region Constructors

        public ShotCharacteristics(float width, float height, int leftTexture, int rightTexture, string name)
        {
            _width = width;
            _height = height;
            _leftTexture = leftTexture;
            _rightTexture = rightTexture;
            _currentTexture = _leftTexture;
            _name = name;
        }

        public ShotCharacteristics(string fileName)
        {
            var strings = File.ReadAllLines("Shots/" + fileName);
            _width = float.Parse(strings[0]);
            _height = float.Parse(strings[1]);
            _leftTexture = int.Parse(strings[2]);
            _rightTexture = int.Parse(strings[3]);
            _currentTexture = _leftTexture;
            _name = strings[4];
        }

        #endregion

        #region Properties

        public float Width
        {
            get { return _width; }
        }

        public float Height
        {
            get { return _height; }
        }

        public int LeftTexture
        {
            get { return _leftTexture; }
        }

        public int RightTexture
        {
            get { return _rightTexture; }
        }

        public int Texture
        {
            get { return _currentTexture; }
        }

        public string Name
        {
            get { return _name; }
        }

        #endregion

        #region Methods

        public void TurnLeft()
        {
            _currentTexture = _leftTexture;
        }

        public void TurnRight()
        {
            _currentTexture = _rightTexture;
        }

        #endregion
    }
}
