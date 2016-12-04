using System.IO;
using MyGameServer.Core;

namespace MyGameServer.Service
{
    class RoomWriter
    {
        #region Methods

        public void WriteInFile(string fileName, Room room)
        {
            using (var file = File.CreateText("Rooms/" + fileName))
            {
                file.WriteLine(room.Form.X);
                file.WriteLine(room.Form.Y);
                file.WriteLine(room.Form.Width);
                file.WriteLine(room.Form.Height);
                file.WriteLine(room.Border.Width);
                file.WriteLine(room.Border.Height);
                file.WriteLine(room.Border.Texture);
                file.WriteLine(room.Texture);
                file.WriteLine(room.Enemies.Count);
                foreach(var t in room.Enemies)
                {
                    file.WriteLine(t.X);
                    file.WriteLine(t.Y);
                    file.WriteLine(t.Name + ".f");
                }
            }
        }

        #endregion
    }
}
