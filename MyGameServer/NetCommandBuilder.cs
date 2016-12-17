using System;
using System.Collections.Generic;
using System.Text;
using MyGameServer.Core;

namespace MyGameServer
{
    static class NetCommandBuilder
    {
        //private static StringBuilder _roomCommand = new StringBuilder();
        //private static StringBuilder _levelCommand = new StringBuilder();
        //private static StringBuilder _gameCommand = new StringBuilder();

        private static Dictionary<Player, StringBuilder> _roomCommands = new Dictionary<Player, StringBuilder>();
        private static Dictionary<Player, StringBuilder> _levelCommands = new Dictionary<Player, StringBuilder>();
        private static Dictionary<Player, StringBuilder> _gameCommands = new Dictionary<Player, StringBuilder>();


        //public static void AppendToRoomCommand(string command)
        //{
        //    _roomCommand.Append(command);
        //    _roomCommand.Append("/");
        //}

        //public static void AppendToLevelCommand(string command)
        //{
        //    _levelCommand.Append(command);
        //    _levelCommand.Append("/");
        //}

        public static void AppendToGameCommand(Player player, string command)
        {
            if (!_gameCommands.ContainsKey(player))
            {
                return;
            }

            _gameCommands[player].Append(command);
            _gameCommands[player].Append("/");
        }

        public static void AppendToRoomCommand(Player player, string command)
        {
            Console.WriteLine(_roomCommands.Count);
            if (!_roomCommands.ContainsKey(player))
            {
                Console.WriteLine("Don't know this player");
                return;
            }
            //Console.WriteLine("Add to room command");
            _roomCommands[player].Append(command);
            _roomCommands[player].Append("/");
        }

        public static void AppendToLevelCommand(Player player, string command)
        {
            if (!_levelCommands.ContainsKey(player))
            {
                return;
            }
            _levelCommands[player].Append(command);
            _levelCommands[player].Append("/");
        }

        //public static void AppendToGameCommand(string command)
        //{
        //    _gameCommand.Append(command);
        //    _gameCommand.Append("/");
        //}

        public static void Clear()
        {
            foreach (var key in _roomCommands.Keys)
            {
                _roomCommands[key].Clear();
                _levelCommands[key].Clear();
                _gameCommands[key].Clear();
            }
            //_roomCommand.Clear();
            //_levelCommand.Clear();
            //_gameCommand.Clear();
        }

        //public static string RoomCommand
        //{
        //    get { return _roomCommand.ToString(); }
        //}

        //public static string LevelCommand
        //{
        //    get { return _levelCommand.ToString(); }
        //}

        //public static string GameCommand
        //{
        //    get { return _gameCommand.ToString(); }
        //}

        public static Dictionary<Player, StringBuilder> RoomCommands
        {
            get { return _roomCommands; }
        }

        public static Dictionary<Player, StringBuilder> LevelCommands
        {
            get { return _levelCommands; }
        }

        public static Dictionary<Player, StringBuilder> GameCommands
        {
            get { return _gameCommands; }
        }
    }
}
