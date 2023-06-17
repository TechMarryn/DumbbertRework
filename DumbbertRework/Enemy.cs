using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DumbbertRework
{
    internal class Enemy
    {
        #region Variables
        private double _hp;
        private readonly double _originalHp;
        private int _damage;
        private readonly int _velikostX;
        private readonly int _velikostY;
        private readonly int _speed;
        private readonly int _windowWidth;
        private readonly int _windowHeight;
        private bool _spawn;
        private readonly Texture2D _texture;
        private Color _color;
        private Vector2 _position;
        private readonly GraphicsDevice _graphicsDevice;
        #endregion

        #region Properties
        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }
        public double HP
        {
            get => _hp;
            set => _hp = value;
        }
        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }
        #endregion

        #region Constructor
        public Enemy(int damage, double hp, int speed, int sizeX, int sizeY, Vector2 position, GraphicsDevice graphicsDevice, Color color, int windowWidth, int windowHeight)
        {
            _speed = speed;
            _velikostY = sizeY;
            _velikostX = sizeX;
            _color = color;
            _windowWidth = windowWidth;
            _windowHeight = windowHeight;
            _position = position;
            _graphicsDevice = graphicsDevice;
            _damage = damage;
            _hp = hp;
            _originalHp = hp;
            _texture = ReadyTexture();
        }
        #endregion

        #region Functions
        private Texture2D ReadyTexture()
        {
            var textureColorData = new Color[_velikostX * _velikostY];

            for (int index = 0; index < textureColorData.Length; index++) { textureColorData[index] = Color.White; }

            var texture = new Texture2D(_graphicsDevice, _velikostX, _velikostY);
            texture.SetData(textureColorData);

            return texture;
        }

        public void Update(GraphicsDeviceManager graphicsDeviceManager, Barricade barricade)
        {
            Move();
            AttackBarricade(barricade);
        }

        public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(_texture, _position, _color);

        public void Spawner()
        {
            _position = new Vector2(1700, 800);
            _spawn = false;
        }

        public void Move()
        {
            if (_hp <= 0) { _spawn = true; }
            if (_spawn == true)
            {
                Spawner();
                _hp = _originalHp;
            }
            else { _position -= Vector2.Normalize(Vector2.UnitX) * _speed; }
        }

        public void AttackBarricade(Barricade barricade)
        {
            if (_position.X <= barricade.Position.X)
            {
                if (barricade.Hp > 0)
                {
                    _position.X = barricade.Position.X;
                    barricade.Hp -= _damage;
                }
                if (barricade.Hp <= 0)
                {
                    //gameover = true;
                }
            }
            else if (_position.X < barricade.Position.X)
            {
                //gameover = true
            }
        }
        #endregion
    }
}
