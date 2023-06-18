using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DumbbertRework
{
    internal class Barricade
    {
        private Texture2D _texture;
        private readonly ContentManager content;
        private int _health, _maximumHealth;
        private Vector2 _position;

        public Texture2D Texture => _texture;

        public int Health
        {
            get => _health;
            set => _health = value;
        }

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public int MaximumHealth
        {
            get => _maximumHealth;
            set => _maximumHealth = value;
        }

        public Barricade(int health, Vector2 position, ContentManager content)
        {
            _health = health;
            _maximumHealth = health;
            _position = position;
            this.content = content;
            TextureSwap();
        }

        public void TextureSwap()
        {
            switch (_health)
            {
                case 200:
                    {
                        _texture = content.Load<Texture2D>("barricade/barricade_100");
                        return;
                    }
                case 160:
                    {
                        _texture = content.Load<Texture2D>("barricade/barricade_80");
                        return;
                    }
                case 120:
                    {
                        _texture = content.Load<Texture2D>("barricade/barricade_60");
                        return;
                    }
                case 80:
                    {
                        _texture = content.Load<Texture2D>("barricade/barricade_40");
                        return;
                    }
                case 40:
                    {
                        _texture = content.Load<Texture2D>("barricade/barricade_20");
                        return;
                    }
                case 0:
                    {
                        _texture = content.Load<Texture2D>("barricade/barricade_0");
                        return;
                    }
            }
        }
    }
}
