using System.Text;

namespace MyGameServer
{
    static class NetCommandBuilder
    {
        private static StringBuilder _roomCommand = new StringBuilder();
        private static StringBuilder _levelCommand = new StringBuilder();
        private static StringBuilder _gameCommand = new StringBuilder();


        public static void AppendToRoomCommand(string command)
        {
            _roomCommand.Append(command);
            _roomCommand.Append("/");
        }

        public static void AppendToLevelCommand(string command)
        {
            _levelCommand.Append(command);
            _levelCommand.Append("/");
        }

        public static void AppendToGameCommand(string command)
        {
            _gameCommand.Append(command);
            _gameCommand.Append("/");
        }

        public static void Clear()
        {
            _roomCommand.Clear();
            _levelCommand.Clear();
            _gameCommand.Clear();
        }

        public static string RoomCommand
        {
            get { return _roomCommand.ToString(); }
        }

        public static string LevelCommand
        {
            get { return _levelCommand.ToString(); }
        }

        public static string GameCommand
        {
            get { return _gameCommand.ToString(); }
        }
    }
}
