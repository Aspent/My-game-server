using System;
using OpenTK;

namespace MyGameServer.Core
{
    class Boss : Enemy
    {
        public delegate void BossAction(Boss boss, Room room); 

        #region Fields

        readonly BossAction _skill;
        bool _canUseSkill;
        bool _isFinal;

        #endregion

        #region Constructors

        public Boss(float x, float y, float width, float height, float speed, double attackSpeed, 
            int leftTexture, int rightTexture, ShotCharacteristics shotChar, int hp, int damage,
            float shotSpeed, float shotRange, string name, BossAction skill) 
            : base(x, y, width, height, speed, attackSpeed, leftTexture, rightTexture, shotChar, hp, damage, 
            shotSpeed, shotRange, name)
        {
            _canUseSkill = true;
            _skill = skill;
            _isFinal = false;
        }

        #endregion

        #region Properties

        public BossAction Skill
        {
            get { return _skill; }
        }

        public bool CanUseSkill
        {
            get { return _canUseSkill; }
            set { _canUseSkill = value; }
        }

        public bool IsFinal
        {
            get { return _isFinal; }
            set { _isFinal = value; }
        }

        #endregion

        #region Methods

        public void UseSkill(Boss boss, Room room)
        {
            _skill.Invoke(this, room);
            _canUseSkill = false;
        }

        public static void ShootRound5(Boss boss, Room room)
        {
            var bossCenterX = boss.Form.Left + boss.Form.Width / 2;
            var bossCenterY = boss.Form.Top + boss.Form.Height / 2;
            for (double i = 0; i < 2 * Math.PI; i += Math.PI*2 / 5)
            {
                var direction = new Vector2(Convert.ToSingle(Math.Cos(i)), Convert.ToSingle(Math.Sin(i)));
                boss.Shoot(room, new Shot(bossCenterX + direction.X * boss.Width,
                    bossCenterY + direction.Y * boss.Height, direction, boss));
                boss.CanShoot = true;
                //room.Shots.Add(new Shot(bossCenterX + direction.X * boss.Width,
                //    bossCenterY + direction.Y * boss.Height, direction, boss));
            }
        }

        public static void ShootRound10(Boss boss, Room room)
        {
            var bossCenterX = boss.Form.Left + boss.Form.Width/2;
            var bossCenterY = boss.Form.Top + boss.Form.Height/2;
            for (double i = 0; i < 2 * Math.PI; i += Math.PI / 5) 
            {
                var direction = new Vector2(Convert.ToSingle(Math.Cos(i)), Convert.ToSingle(Math.Sin(i)));
                boss.Shoot(room, new Shot(bossCenterX + direction.X * boss.Width,
                    bossCenterY + direction.Y * boss.Height, direction, boss));
                boss.CanShoot = true;
            }
        }

        public static void SummonUp(Boss boss, Room room)
        {
            if(boss.Form.Left - room.Form.Left < room.Form.Right - boss.Form.Right)
            {
                room.SummonedEnemies.Add(new Enemy(boss.Form.Right + 0.05f, boss.Form.Top, "Bag.f"));
                if (boss.Form.Bottom - room.Form.Bottom > 0.22f)
                {
                    room.SummonedEnemies.Add(new Enemy(boss.Form.Right + 0.05f, boss.Form.Bottom, "Bag.f"));
                }
            }
            else
            {
                room.SummonedEnemies.Add(new Enemy(boss.Form.Left - 0.25f, boss.Form.Top, "Bag.f"));
                Console.WriteLine(boss.Form.Bottom - room.Form.Bottom);
                if (boss.Form.Bottom - room.Form.Bottom > 0.3f)
                {
                    room.SummonedEnemies.Add(new Enemy(boss.Form.Left - 0.25f, boss.Form.Bottom + 0.1f, "Bag.f"));
                }
            }

        }
        
        #endregion
    }
}
