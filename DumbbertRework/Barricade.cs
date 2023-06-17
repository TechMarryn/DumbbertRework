using Microsoft.Xna.Framework;

namespace DumbbertRework
{
    internal class Barricade
    {
        #region Variables
        private int _hp;
        private Vector2 _position;
        private float _sizeX;
        #endregion

        #region Properties
        public int Hp
        {
            get => _hp;
            set => _hp = value;
        }
        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }
        public float SizeX
        {
            get => _sizeX;
            set => _sizeX = value;
        }
        #endregion

        #region Constructor
        public Barricade(int hp, Vector2 position, float sizeX)
        {
            _hp = hp;
            _position = position;
            _sizeX = sizeX;
        }
        #endregion
    }
}
