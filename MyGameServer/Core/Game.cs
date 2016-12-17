using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using MyGameServer.Net;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace MyGameServer.Core
{
    class Game : GameWindow
    {
        #region Fields

        readonly int[] _textures = new int[100];
        Room _currentRoom;
        readonly List<Level> _levels = new List<Level>();
        //Player _player;

        bool _isPausing = true;
 
        int _currentLevel;
        readonly List<string> _firstLevelFileNames = new List<string>();
        readonly List<string> _secondLevelFileNames = new List<string>();
        readonly List<string> _thirdLevelFileNames = new List<string>();


        readonly Dictionary<string, Item.ItemEffect> _itemEffects = new Dictionary<string,Item.ItemEffect>();
        readonly List<string> _itemNames = new List<string>();
        private readonly Dictionary<Level, ILevelSupervisor> _levelSupervisors = new Dictionary<Level, ILevelSupervisor>();
        //private List<Player> _players = new List<Player>(); 


        private Dictionary<Player, EndPoint> _playerRemotes = new Dictionary<Player, EndPoint>();
        private Dictionary<EndPoint, Player> _remotesPlayers = new Dictionary<EndPoint, Player>();
        private HashSet<EndPoint> _remotes = new HashSet<EndPoint>();
        private Dictionary<Player, string> _commands = new Dictionary<Player, string>();




        #endregion

        public void UnPause()
        {
            _isPausing = false;
        }

        public void StartGame()
        {
            _itemNames.Clear();
            _levels.Clear();

            _itemNames.Add("Boot");
            _itemNames.Add("Pizza");
            _itemNames.Add("BigHeart");
            _itemNames.Add("Glasses");
            _itemNames.Add("Zelie");
            _itemNames.Add("Pulemet");
            _itemNames.Add("Milk");
            _itemNames.Add("Molotok");

            //var player = new Player(-0.4f, 0.0f, 359.0f / 4000, 982.0f / 4000, 0.4f / 60, 1000, 86, 87,
            //      new ShotCharacteristics("Fireball.f"), 70, 29, 0.4f / 60, 0.5f, "Boy");
            //_players.Add(player);
            //_currentRoom.Players.Add(player);

            //var levelGenerator = new LevelGenerator(_firstLevelTemplates);
            //var level1 = levelGenerator.Generate(_itemNames, _itemEffects, _firstLevelFileNames);
            var level1 = new Level();
            _levels.Add(level1);
            //_levelSupervisors[level1] = new LevelSupervisor(this);


            //levelGenerator = new LevelGenerator(_secondLevelTemplates);
            var level2 = new Level();
            //var level2 = levelGenerator.Generate(_itemNames, _itemEffects, _secondLevelFileNames);
            _levels.Add(level2);
            //_levelSupervisors[level2] = new LevelSupervisor(this);

            //levelGenerator = new LevelGenerator(_thirdLevelTemplates);
            var level3 = new Level();
            //var level3 = levelGenerator.Generate(_itemNames, _itemEffects, _thirdLevelFileNames);
            _levels.Add(level3);

            _levels[_levels.Count - 1].BossRoom.Boss.IsFinal = true;

            _currentLevel = 0;
            _currentRoom = _levels[_currentLevel].Rooms[0];
            //_currentRoom.Player = _player;
            //_currentRoom.Players.Add(player);
            _isPausing = false;

 

            _levelSupervisors[level1] = new DefaultLevelSupervisor(level1, _commands, _playerRemotes);
            _levelSupervisors[level2] = new DefaultLevelSupervisor(level2, _commands, _playerRemotes);
            _levelSupervisors[level3] = new DefaultLevelSupervisor(level3, _commands, _playerRemotes);
            var receiver = new Receiver(Network.ServerSocket, _playerRemotes, _remotesPlayers,
                _remotes, _commands);
            var receiverThread = new Thread(receiver.Receive);
            receiverThread.Start();

        }

        #region Constructors

        public Game(int width, int height) : base(width, height)
        {
            _itemEffects["Boot"] = ItemEffect.UpSpeed;
            _itemEffects["Pizza"] = ItemEffect.UpMaxHealth;
            _itemEffects["BigHeart"] = ItemEffect.UpMaxHealth;
            _itemEffects["Glasses"] = ItemEffect.UpRange;
            _itemEffects["Zelie"] = ItemEffect.UpAttackSpeed;
            _itemEffects["Pulemet"] = ItemEffect.UpAttackSpeed;
            _itemEffects["Milk"] = ItemEffect.UpDamage;
            _itemEffects["Molotok"] = ItemEffect.UpDamage;
            _itemEffects["Heart"] = ItemEffect.UpHealth;

            _itemNames.Add("Boot");
            _itemNames.Add("Pizza");
            _itemNames.Add("BigHeart");
            _itemNames.Add("Glasses");
            _itemNames.Add("Zelie");
            _itemNames.Add("Pulemet");
            _itemNames.Add("Milk");
            _itemNames.Add("Molotok");

            _firstLevelFileNames.Add("Room1.f");
            _firstLevelFileNames.Add("Room2.f");
            _firstLevelFileNames.Add("Room2Snake.f");
            _firstLevelFileNames.Add("Room2Tree.f");
            _firstLevelFileNames.Add("RoomTreeSnake.f");

            _secondLevelFileNames.Add("Room2GhostSkeleton.f");
            _secondLevelFileNames.Add("RoomSkeletonGhostCenter.f");
            _secondLevelFileNames.Add("RoomGhostsDog.f");
            _secondLevelFileNames.Add("Room2Skeleton.f");

            _thirdLevelFileNames.Add("Room2Bag.f");
            _thirdLevelFileNames.Add("RoomBagBlackboard.f");
            _thirdLevelFileNames.Add("Room2Blackboard.f");


            //_firstLevelTemplates.Add(new FirstLevelTemplate());
            //_firstLevelTemplates.Add(new FirstLevelTemplate2());

            //_secondLevelTemplates.Add(new SecondLevelTemplate1());
            //_secondLevelTemplates.Add(new SecondLevelTemplate2());

            //_thirdLevelTemplates.Add(new ThirdLevelTemplate());


            var w = Width;
            var h = Height;

            //_mainMenu = new Menu(new RectangleF(-1.0f*w/h, 1, 2.0f*w/h, -2), 14);
            
            //_mainMenu.Buttons.Add(new Button(new RectangleF(-0.3f, 0.2f, 0.6f, -0.2f), StartGame, 15, 60));
            //_mainMenu.Buttons.Add(new Button(new RectangleF(-0.3f, -0.2f, 0.6f, -0.2f), ShowControls, 65, 66));
            //_mainMenu.Buttons.Add(new Button(new RectangleF(-0.3f, -0.6f, 0.6f, -0.2f), Exit, 63, 64));

            //_currentMenu = _mainMenu;

            //_pauseMenu = new Menu(new RectangleF(-1.0f * w / h, 1, 2.0f * w / h, -2), 14);
            //_pauseMenu.Buttons.Add(new Button(new RectangleF(-0.3f, 0.2f, 0.6f, -0.2f), UnPause, 61, 62));
            //_pauseMenu.Buttons.Add(new Button(new RectangleF(-0.3f, -0.2f, 0.6f, -0.2f), ShowControls, 65, 66));
            //_pauseMenu.Buttons.Add(new Button(new RectangleF(-0.3f, -0.6f, 0.6f, -0.2f), GoToMainMenu, 67, 68));

            //_winnerMenu = new Menu(new RectangleF(-1.0f * w / h, 1, 2.0f * w / h, -2), 73);
            //_winnerMenu.Buttons.Add(new Button(new RectangleF(-0.3f, -0.6f, 0.6f, -0.2f), GoToMainMenu, 67, 68));

            //_loserMenu = new Menu(new RectangleF(-1.0f * w / h, 1, 2.0f * w / h, -2), 74);
            //_loserMenu.Buttons.Add(new Button(new RectangleF(-0.3f, -0.6f, 0.6f, -0.2f), GoToMainMenu, 67, 68));

            //_controlMenu = new Menu(new RectangleF(-1.0f * w / h, 1, 2.0f * w / h, -2), 72);
            //_controlMenu.Buttons.Add(new Button(new RectangleF(-0.3f, -0.6f, 0.6f, -0.2f), GoToPrevMenu, 67, 68));
            //_prevMenu = _mainMenu;
            StartGame();
            
        }

        #endregion

        #region Methods

        public void GoToNextLevel()
        {
            _currentLevel++;
            _currentRoom = _levels[_currentLevel].Rooms[_levels[_currentLevel].StartRoomIndex];
            foreach (var t in Players.GamePlayers)
            {
                _currentRoom.Players.Add(t);
                t.MoveTo(-0.4f, 0);
            }
            //_currentRoom.Player = _player;
            //_player.MoveTo(-0.4f, 0);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            var nRange = 1.0f;
            var w = Width;
            var h = Height;
            GL.Viewport(0, 0, w, h);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            if (w <= h)
            {
                GL.Ortho(-nRange, nRange, -nRange * h / w, nRange * h / w, -nRange, nRange);
            }
            else
            {
                GL.Ortho(-nRange * w / h, nRange * w / h, -nRange, nRange, -nRange, nRange);
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //Console.WriteLine(Players.GamePlayers.Count);
            if (Players.GamePlayers.Count == 0) return;
            
            GameInfo.Width = Width;
            GameInfo.Height = Height;
            NetCommandBuilder.Clear();
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            {

                _levelSupervisors[_levels[_currentLevel]].Run();
            }
            foreach (var t in Players.GamePlayers)
            {
                //Console.WriteLine("send command = {0}", NetCommandBuilder.RoomCommands[t]);
                Network.NetWorker.SendTo(Encoding.UTF8.GetBytes(NetCommandBuilder.LevelCommands[t].ToString()), _playerRemotes[t]);
                Network.NetWorker.SendTo(Encoding.UTF8.GetBytes(NetCommandBuilder.RoomCommands[t].ToString()), _playerRemotes[t]);
                
            }
            //_playerRemotes[]
            //Network.NetWorker.Send(Encoding.UTF8.GetBytes(NetCommandBuilder.RoomCommand));
            //Network.NetWorker.Send(Encoding.UTF8.GetBytes(NetCommandBuilder.LevelCommand));
            SwapBuffers();
        }

        #endregion

        //public Player Player => _player;

        public Room CurrentRoom => _currentRoom;

        public int[] Textures => _textures;
    }
}
