using System;
using System.Drawing;
using System.Timers;

namespace MyGameServer.Core
{
    class Lever
    {
        #region Fields

        readonly RectangleF _form;
        readonly RectangleF _interactionForm;
        readonly int[] _textures = new int[3];
        bool _isActive;
        int _currentState;

        readonly Timer _timer = new Timer(200);
        bool _canBeTurned = true;
       
        #endregion

        #region Constructors

        public Lever(RectangleF form, int[] textures, float interactionDistance)
        {
            _form = form;
            _textures = textures;
            _isActive = true;
            _currentState = 0;
            _interactionForm = new RectangleF(form.X - interactionDistance, form.Y + interactionDistance,
                form.Width + 2 * interactionDistance, form.Height);

            _timer.AutoReset = false;
            _timer.Elapsed += OnTimedEvent;
        }

        #endregion

        #region Properties

        public RectangleF Form
        {
            get { return _form; } 
        }

        public RectangleF InteractionForm
        {
            get { return _interactionForm; }
        }

        public int[] Textures
        {
            get { return _textures; }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        public int CurrentState
        {
            get { return _currentState; }
        }

        #endregion

        #region Methods

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            _canBeTurned = true;
        }

        public void TurnLeft()
        {
            if (_canBeTurned)
            {
                if (_currentState > 0)
                {
                    _currentState--;
                    _canBeTurned = false;
                    _timer.Start();
                }
            }
        }

        public void TurnRight()
        {
            if (_canBeTurned)
            {
                if (_currentState < 2)
                {
                    _currentState++;
                    _canBeTurned = false;
                    _timer.Start();
                }
            }
        }

        #endregion
    }
}
