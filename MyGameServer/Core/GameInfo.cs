namespace MyGameServer.Core
{
    static class GameInfo
    {
        private static int _width;
        private static int _height;

        public static int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public static int Height
        {
            get { return _height; }
            set { _height = value; }
        }
    }
}
