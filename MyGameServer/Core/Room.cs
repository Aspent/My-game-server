using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MyGameServer.Core
{
    class Room
    {
        #region Fields

        protected RectangleF _form;
        protected int _texture;
        protected List<Obstacle> _obstacles = new List<Obstacle>();
        protected RoomBorder _border;
        protected List<Enemy> _enemies = new List<Enemy>();
        protected List<Enemy> _summonedEndmies = new List<Enemy>();
        protected List<Shot> _shots = new List<Shot>();
        protected List<Item> _items = new List<Item>();
        protected Door _topDoor;
        protected Door _botDoor;
        protected Door _leftDoor;
        protected Door _rightDoor;
        protected Player _player;
        protected List<Player> _players = new List<Player>(); 
        protected Dictionary<Enemy, IEnemyController> _enemyControllers 
            = new Dictionary<Enemy, IEnemyController>(); 
        

        #endregion

        #region Constructors

        public Room(RectangleF form, int texture)
        {
            _form = form;
            _texture = texture;
            _border = new RoomBorder(0.15f, 0.15f, 0);            
        }

        public Room(string fileName)
        {
            var strings = File.ReadAllLines("Rooms/" + fileName);
            _form = new RectangleF(float.Parse(strings[0]), float.Parse(strings[1]), 
                float.Parse(strings[2]), float.Parse(strings[3]));
            _border = new RoomBorder(float.Parse(strings[4]), float.Parse(strings[5]), 
                int.Parse(strings[6]));
            _texture = int.Parse(strings[7]);
            for(var i = 0; i< int.Parse(strings[8]); i++)
            {
                var x = float.Parse(strings[9 + 3*i]);
                var y = float.Parse(strings[10 + 3*i]);
                var name = strings[11 + 3 * i];
                var enemy = new Enemy(x, y, name);
                _enemies.Add(enemy);
                _enemyControllers[enemy] = new DefaultEnemyController(enemy, this);
            }
            
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

        public List<Obstacle> Obstacles
        {
            get { return _obstacles; }
        }

        public RoomBorder Border
        {
            get { return _border; }
        }

        public List<Enemy> Enemies
        {
            get { return _enemies; }
        }

        public List<Enemy> SummonedEnemies
        {
            get { return _summonedEndmies; }
        }

        public List<Shot> Shots
        {
            get { return _shots; }
        }

        public List<Item> Items
        {
            get { return _items; }
        }

        public Door TopDoor
        {
            get { return _topDoor; }
            set { _topDoor = value; }
        }

        public Door BotDoor
        {
            get { return _botDoor; }
            set { _botDoor = value; }
        }

        public Door LeftDoor
        {
            get { return _leftDoor; }
            set { _leftDoor = value; }
        }

        public Door RightDoor
        {
            get { return _rightDoor; }
            set { _rightDoor = value; }
        }

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public Dictionary<Enemy, IEnemyController> EnemyControllers
        {
            get { return _enemyControllers; }
        }

        public List<Player> Players
        {
            get { return _players; }
        } 


        #endregion

        #region Methods

        public List<Door> GetAllDoors()
        {
            var doors = new List<Door>();
            if(TopDoor != null)
            {
                doors.Add(TopDoor);
            }
            if (BotDoor != null)
            {
                doors.Add(BotDoor);
            }
            if (LeftDoor != null)
            {
                doors.Add(LeftDoor);
            }
            if (RightDoor != null)
            {
                doors.Add(RightDoor);
            }
            return doors;
        }

        #endregion

    }    
}
