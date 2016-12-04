using System;
using System.Collections.Generic;
using System.Drawing;
using MyGameServer.Core;

namespace MyGameServer
{
    class RoomsRepository
    {
        #region Fields

        readonly List<Room> _rooms = new List<Room>();

        #endregion

        #region Constructors

        public RoomsRepository()
        {
            var room = new Room(new RectangleF(-0.85f, 0.5f, 1.7f, -1.4f), 3);
            
            room.Enemies.Add(new Enemy(room.Form.Left + 0.5f,  room.Form.Bottom + 0.5f, "Dog.f"));
            room.Items.Add(new Item(room.Form.Left + 0.3f,room.Form.Bottom + 0.5f, ItemEffect.UpSpeed, "Boot.f"));

            _rooms.Add(room);

            room = new Room(new RectangleF(-0.85f, 0.5f, 1.7f, -1.4f), 3);

            room.Enemies.Add(new Enemy(room.Form.Left + 0.5f, room.Form.Bottom + 0.5f, "Dog.f"));
            room.Enemies.Add(new Enemy(room.Form.Left + 1.0f, room.Form.Bottom + 1.0f, "Dog.f"));

            _rooms.Add(room);
        }

        #endregion

        #region Properties

        public List<Room> Rooms
        {
            get { return _rooms; }
        }

        #endregion

        #region Methods

        public Room GetRandomRoom()
        {
            var random = new Random();
            var index = random.Next(_rooms.Count);
            return _rooms[index];
        }

        #endregion
    }
}
