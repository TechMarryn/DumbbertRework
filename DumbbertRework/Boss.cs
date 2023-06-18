using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DumbbertRework
{
    internal class Boss
    {
        private int _health;
        private readonly int velikostX, velikostY;
        private float _speed;
        private bool _exists, _died;
        private Vector2 position, spawnPosition;
        private readonly Texture2D texture;
        private Rectangle _hitbox;

        public int HP
        {
            get => _health;
            set => _health = value;
        }

        public bool Exists
        {
            get => _exists;
            set => _exists = value;
        }

        public bool Died
        {
            get => _died;
            set => _died = value;
        }

        public Rectangle Hitbox { get => _hitbox; }

        public Vector2 Pozice
        {
            get => position;
            set => position = value;
        }
        
        public Boss(int health, float speed, int sizeX, int sizeY, Vector2 spawnPosition, ContentManager contentManager)
        {
            _health = health;
            _speed = speed;
            velikostX = sizeX;
            velikostY = sizeY;
            this.spawnPosition = spawnPosition;
            position = spawnPosition;
            texture = contentManager.Load<Texture2D>("Boss");
            _hitbox = new((int)position.X, (int)position.Y, sizeX, sizeY);
        }

        private void CanExist(Barricade barricade, bool cheat)
        {
            if (!Exists) { return; }
            if (!_died) { Walk(barricade, cheat); }
            else
            {
                _speed = 0;
                position = spawnPosition + new Vector2(50, 0);
            }
        }

        private void Walk(Barricade barricade, bool cheat)
        {
            if (position.X <= barricade.Position.X)
            {
                if (cheat) { barricade.Health = barricade.MaximumHealth; }
                else { barricade.Health = 0; }
            }
            position -= Vector2.Normalize(Vector2.UnitX) * _speed;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_died) { spriteBatch.Draw(texture, position, Color.White); }
            else { spriteBatch.Draw(texture, position, Color.Transparent); }
        }

        public void Update(Barricade barricade, bool cheat)
        {
            _hitbox = new Rectangle((int)position.X, (int)position.Y, velikostX, velikostY);
            CanExist(barricade, cheat);
            _died = _health <= 0;
        }
    }
}
