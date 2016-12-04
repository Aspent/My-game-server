using MyGameServer.Core;

namespace MyGameServer
{
    interface IController
    {
        void Control(Person person, Room room);
    }
}
