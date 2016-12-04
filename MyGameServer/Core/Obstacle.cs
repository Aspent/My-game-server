using System.Drawing;

namespace MyGameServer.Core
{
    class Obstacle
    {
        #region Fields

        readonly RectangleF _form;
        readonly int _texture;
       
        #endregion

        #region Constructors

        public Obstacle(RectangleF form, int texture)
        {
            _form = form;
            _texture = texture;
        }

        #endregion

        #region Properties

        public RectangleF Form
        {
            get { return _form; } 
        }

        public int Texture
        {
            get { return _texture; }
        }

        #endregion
    }
}
