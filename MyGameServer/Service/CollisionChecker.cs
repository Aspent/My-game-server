using System.Drawing;
using MyGameServer.Core;

namespace MyGameServer.Service
{
    class CollisionChecker
    {
        #region Methods

        public bool IsCollided(Player player, Room room)
        {

            var intersectionDeter = new IntersectionDeterminant();
            var border = room.Border;
            var doors = room.GetAllDoors();
            
            foreach(var t in doors)
            {
                if(IsCollided(player, t) && !t.IsLocked)
                {
                    return false;
                }
            }

            if (intersectionDeter.IsIntersected(player.Form,
                new RectangleF(room.Form.Left, room.Form.Top, border.Width, room.Form.Height))) 
            {
                return true;
            }

            if (intersectionDeter.IsIntersected(player.Form,
                new RectangleF(room.Form.Left, room.Form.Top, room.Form.Width, -border.Height)))
            {
                return true;
            }

            if (intersectionDeter.IsIntersected(player.Form,
                new RectangleF(room.Form.Right - border.Width, room.Form.Top, border.Width, room.Form.Height)))
            {
                return true;
            }

            if (intersectionDeter.IsIntersected(player.Form,
                new RectangleF(room.Form.Left, room.Form.Bottom + border.Height, room.Form.Width, -border.Height))) 
            {
                return true;
            }

            return false;
        }

        public bool IsCollided(Player player, Obstacle obstacle)
        {            
            var intersectionDeter = new IntersectionDeterminant();

            if (intersectionDeter.IsIntersected(player.Form, obstacle.Form)) 
            {
                return true;
            }
            return false;
        }

        public bool IsCollided(Player player, Enemy enemy)
        {
            var intersectionDeter = new IntersectionDeterminant();

            if (intersectionDeter.IsIntersected(player.Form, enemy.Form))
            {
                return true;
            }
            return false;
        }

        public bool IsCollided(Shot shot, Room room)
        {
            var intersectionDeter = new IntersectionDeterminant();
            var border = room.Border;
            if (intersectionDeter.IsIntersected(shot.Form,
                new RectangleF(room.Form.Left, room.Form.Top, border.Width, room.Form.Height)))
            {
                return true;
            }

            if (intersectionDeter.IsIntersected(shot.Form,
                new RectangleF(room.Form.Left, room.Form.Top, room.Form.Width, -border.Height)))
            {
                return true;
            }

            if (intersectionDeter.IsIntersected(shot.Form,
                new RectangleF(room.Form.Right - border.Width, room.Form.Top, border.Width, room.Form.Height)))
            {
                return true;
            }

            if (intersectionDeter.IsIntersected(shot.Form,
                new RectangleF(room.Form.Left, room.Form.Bottom + border.Height, room.Form.Width, -border.Height)))
            {
                return true;
            }

            return false;
        }

        public bool IsCollided(Shot shot, Player player)
        {
            var intersectionDeter = new IntersectionDeterminant();
            return intersectionDeter.IsIntersected(shot.Form, player.Form);         
        }

        public bool IsCollided(Shot shot, Enemy enemy)
        {
            var intersectionDeter = new IntersectionDeterminant();
            return intersectionDeter.IsIntersected(shot.Form, enemy.Form);
        }

        public bool IsCollided(Player player, FinishZone finishZone)
        {
            var intersectionDeter = new IntersectionDeterminant();
            return intersectionDeter.IsIntersected(player.Form, finishZone.Form);
        }

        public bool IsCollided(Player player, Item item)
        {
            var intersectionDeter = new IntersectionDeterminant();
            return intersectionDeter.IsIntersected(player.Form, item.Form);
        }

        public bool IsCollided(Player player, Door door)
        {
            var intersectionDeter = new IntersectionDeterminant();
            return intersectionDeter.IsIntersected(player.Form, door.Form);
        }

        public bool IsCollided(Enemy enemy, Room room)
        {

            var intersectionDeter = new IntersectionDeterminant();
            var border = room.Border;

            if (intersectionDeter.IsIntersected(enemy.Form,
                new RectangleF(room.Form.Left, room.Form.Top, border.Width, room.Form.Height)))
            {
                return true;
            }

            if (intersectionDeter.IsIntersected(enemy.Form,
                new RectangleF(room.Form.Left, room.Form.Top, room.Form.Width, -border.Height)))
            {
                return true;
            }

            if (intersectionDeter.IsIntersected(enemy.Form,
                new RectangleF(room.Form.Right - border.Width, room.Form.Top, border.Width, room.Form.Height)))
            {
                return true;
            }

            if (intersectionDeter.IsIntersected(enemy.Form,
                new RectangleF(room.Form.Left, room.Form.Bottom + border.Height, room.Form.Width, -border.Height)))
            {
                return true;
            }

            return false;
        }

        public bool IsCollided(Enemy enemy, Obstacle obstacle)
        {
            var intersectionDeter = new IntersectionDeterminant();

            if (intersectionDeter.IsIntersected(enemy.Form, obstacle.Form))
            {
                return true;
            }
            return false;
        }

        public bool IsCollided(Enemy enemy1, Enemy enemy2)
        {
            var intersectionDeter = new IntersectionDeterminant();

            if (intersectionDeter.IsIntersected(enemy1.Form, enemy2.Form))
            {
                return true;
            }
            return false;
        }

        public bool IsCollided(Player player, Lever lever)
        {
            var intersectionDeter = new IntersectionDeterminant();
            return intersectionDeter.IsIntersected(player.Form, lever.Form);
        }

        public bool IsCollided(Player player, Note note)
        {
            var intersectionDeter = new IntersectionDeterminant();
            return intersectionDeter.IsIntersected(player.Form, note.Form);
        }

        #endregion
    }
}
