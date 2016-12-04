using System.Drawing;

namespace MyGameServer.Core
{
    class FinishZone
    {
        #region Fields

        readonly RectangleF _form;
        readonly int _texture;
        bool _isActive;
       
        #endregion

        #region Constructors

        public FinishZone(RectangleF form, int texture)
        {
            _form = form;
            _texture = texture;
            _isActive = false;
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

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        #endregion
    }
}
