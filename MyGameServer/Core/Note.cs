using System.Drawing;

namespace MyGameServer.Core
{
    class Note
    {
        #region Fields

        readonly RectangleF _form;
        readonly int _texture;
        readonly int _taskTexture;
        readonly int[] _trueState;

        #endregion

        #region Constructors

        public Note(RectangleF form, int texture, int taskTexture, int[] trueState)
        {
            _form = form;
            _texture = texture;
            _taskTexture = taskTexture;
            _trueState = trueState;
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

        public int TaskTexture
        {
            get { return _taskTexture; }
        }

        public int[] TrueState
        {
            get { return _trueState; }
        }

        #endregion


    }
}
