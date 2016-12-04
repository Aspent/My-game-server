using System.Drawing;
using System.IO;

namespace MyGameServer.Core
{
    class Item
    {
        public delegate void ItemEffect(Player player, Game game);

        #region Fields

        readonly RectangleF _form;
        readonly int _texture;
        bool _isAvailable;
        bool _isPicked;
        readonly ItemEffect _effect;
        readonly string _name;
       
        #endregion

        #region Constructors

        public Item(RectangleF form, int texture, ItemEffect effect, string name)
        {
            _form = form;
            _texture = texture;
            _isAvailable = false;
            _isPicked = false;
            _effect = effect;
            _name = name;
        }

        public Item(float x, float y, ItemEffect effect, string fileName)
        {
            var strings = File.ReadAllLines("Items/" + fileName);
            _form = new RectangleF(x, y, float.Parse(strings[0]), float.Parse(strings[1]));
            _texture = int.Parse(strings[2]);
            _effect = effect;
        
            _name = strings[3];
        }

        #endregion

        #region Properties

        public RectangleF Form
        {
            get { return _form; } 
        }

        public int Texture
        {
            get { return _texture; }
        }

        public ItemEffect Effect
        {
            get { return _effect; }
        }

        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { _isAvailable = value; }
        }

        public bool IsPicked
        {
            get { return _isPicked; }
            set { _isPicked = value; }
        }

        public string Name
        {
            get { return _name; }
        }

        #endregion
    }
}
