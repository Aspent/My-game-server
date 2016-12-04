using System.Collections.Generic;
using System.Drawing;

namespace MyGameServer.Core
{
    class ChallengeRoom : Room
    {
        #region Fields

        readonly List<Lever> _levers = new List<Lever>();
        readonly Note _note;

        #endregion

        #region Constructors

        public ChallengeRoom(RectangleF form, int texture, Note note) : base(form, texture)
        {
            _note = note;
            int[] leverTextures = new int[] { 69, 70, 71 };
            _levers.Add(new Lever(new RectangleF(form.Right - form.Width / 4, form.Top + form.Height / 4 + 0.15f,
                0.15f, -0.15f), leverTextures, 0.1f));
            _levers.Add(new Lever(new RectangleF(form.Right - form.Width / 4, form.Top + form.Height * 2 / 4 + 0.15f,
                0.15f, -0.15f), leverTextures, 0.1f));
            _levers.Add(new Lever(new RectangleF(form.Right - form.Width / 4, form.Top + form.Height * 3 / 4 + 0.15f,
                0.15f, -0.15f), leverTextures, 0.1f));

        }

        #endregion

        #region Properties

        public List<Lever> Levers
        {
            get { return _levers; }
        }

        public Note Note
        {
            get { return _note; }
        }

        #endregion
    }
}
