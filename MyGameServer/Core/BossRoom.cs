using System.Drawing;

namespace MyGameServer.Core
{
    class BossRoom : Room
    {
        #region Fields

        readonly Boss _boss;
        readonly FinishZone _finishZone;
        //protected Dictionary<Enemy, IEnemyController> _enemyControllers
        //    = new Dictionary<Enemy, IEnemyController>();

        #endregion

        #region Constructors

        public BossRoom(RectangleF form, int texture, Boss boss) : base(form, texture)
        {
            _boss = boss;
            _enemyControllers[_boss] = new BossCancerController(_boss, this);
            _enemies.Add(_boss);
            _finishZone = new FinishZone(new RectangleF(_form.X + 0.7f, _form.Bottom + 0.9f, 0.2f, -0.2f), 8);
        }

        #endregion

        #region Properties

        public Boss Boss
        {
            get { return _boss; }
        }

        public FinishZone FinishZone
        {
            get { return _finishZone; }
        }

        #endregion
    }
}
