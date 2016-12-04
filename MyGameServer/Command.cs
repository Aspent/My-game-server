using System.Collections.Generic;
using MyGameServer.Core;
using OpenTK.Input;

namespace MyGameServer
{
    class Command
    {
        public delegate void PlayerAction(Player player, Room room);

        #region Fields

        readonly List<Key> _keys;
        readonly List<PlayerAction> _actions;

        #endregion

        #region Constructors

        public Command(List<Key> keys, List<PlayerAction> actions)
        {
            _keys = keys;
            _actions = actions;            
        }

        #endregion

        #region Properties

        public List<Key> Keys
        {
            get { return _keys; }
        }

        public List<PlayerAction> Actions
        {
            get { return _actions; }
        }

        #endregion
    }
}
