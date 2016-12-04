using System.IO;
using MyGameServer.Core;

namespace MyGameServer
{
    class ShotCharacteristicLoader
    {
        #region Methods

        public ShotCharacteristics LoadFromFile(string fileName)
        {
            var strings = File.ReadAllLines("Shots/" + fileName);
            var width = float.Parse(strings[0]);
            var height = float.Parse(strings[1]);
            var texture = int.Parse(strings[2]);
            var name = strings[3];
            return new ShotCharacteristics(width, height, texture, texture, name);
        }

        #endregion
    }
}
