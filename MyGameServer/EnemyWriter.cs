using System.IO;
using MyGameServer.Core;

namespace MyGameServer
{
    class EnemyWriter
    {
        #region Methods

        public void WriteInFile(string fileName, Enemy enemy)
        {
            using (var file = File.CreateText("Enemies/" + fileName))
            {
                file.WriteLine(enemy.Width);
                file.WriteLine(enemy.Height);
                file.WriteLine(enemy.Speed);
                file.WriteLine(enemy.AttackSpeed);
                file.WriteLine(enemy.LeftTexture);
                file.WriteLine(enemy.RightTexture);
                file.WriteLine(enemy.ShotChar.Name);
                file.WriteLine(enemy.MaxHp);
                file.WriteLine(enemy.Damage);
                file.WriteLine(enemy.ShotSpeed);
                file.WriteLine(enemy.ShotRange);
                file.WriteLine(enemy.Name);
            }
        }

        #endregion
    }
}
