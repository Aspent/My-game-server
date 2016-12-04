using System.Windows.Forms;
using MyGameServer.Core;


namespace MyGameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var resolution = SystemInformation.PrimaryMonitorSize;
            using (var game = new Game(resolution.Width, resolution.Height))
            {
                game.Run();
            }
            //var networker = new NetWorker();
            //var data = networker.Receive();
            //Console.WriteLine(Encoding.UTF8.GetString(data));
            //var message = new StringBuilder();
            //message.Append("mvl/");
            //message.Append("mvr/");
            //var strs = message.ToString().Split('/');
            //foreach (var t in strs)
            //{
            //    Console.WriteLine(t);
            //}
            //Console.ReadKey();
            
        }
    }
}
