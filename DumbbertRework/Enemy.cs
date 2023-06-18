using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DumbbertRework
{
    class Enemy
    {
        private int _health, _baseHealth;
        private readonly int sizeX, sizeY, _damage;
        private float _speed;
        private Texture2D texture;
        private Vector2 _position, _basePosition;
        private readonly Animation animation;
        private readonly ContentManager content;
        private Rectangle _hitbox;

        public int HP
        {
            get => _health;
            set => _health = value;
        }
        
        public int BaseHealth
        {
            get => _baseHealth;
            set => _baseHealth = value;
        }
        
        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }
        
        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public Vector2 ZakladniPozice => _basePosition;

        public Rectangle Hitbox => _hitbox;

        public Enemy(int damage, int health, float speed, int sizeX, int sizeY, Vector2 position, ContentManager content)
        {
            animation = new Animation(1, 1, 60);
            this.content = content;
            _health = _baseHealth = health;
            texture = content.Load<Texture2D>("pepega/PepegaL-1");
            _speed = speed;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            _position = _basePosition = position;
            _damage = damage;
            _hitbox = new((int)position.X, (int)position.Y, sizeX, sizeY);
        }

        public void SwapTextures()
        {
            switch (animation.EnemyTexture)
            {
                case 1:
                    {
                        texture = content.Load<Texture2D>("pepega/PepegaL-1");
                        break;
                    }
                case 2:
                    {
                        texture = content.Load<Texture2D>("pepega/PepegaL-2");
                        break;
                    }
                case 3:
                    {
                        texture = content.Load<Texture2D>("pepega/PepegaL-3");
                        break;
                    }
                case 4:
                    {
                        texture = content.Load<Texture2D>("pepega/PepegaL-4");
                        break;
                    }
                case 5:
                    {
                        texture = content.Load<Texture2D>("pepega/PepegaL-5");
                        break;
                    }
                case 6:
                    {
                        texture = content.Load<Texture2D>("pepega/PepegaR-5");
                        break;
                    }
                case 7:
                    {
                        texture = content.Load<Texture2D>("pepega/PepegaR-4");
                        break;
                    }
                case 8:
                    {
                        texture = content.Load<Texture2D>("pepega/PepegaR-3");
                        break;
                    }
                case 9:
                    {
                        texture = content.Load<Texture2D>("pepega/PepegaR-2");
                        break;
                    }
                case 10:
                    {
                        texture = content.Load<Texture2D>("pepega/PepegaR-1");
                        break;
                    }
            }
        }

        public void Update(Barricade barricade, Clock clock, Spawner spawner, Shop shop, bool cheat)
        {
            _hitbox = new((int)_position.X, (int)_position.Y, sizeX, sizeY);
            BasicFunction(barricade, clock, spawner, shop, cheat);
            animation.Update();
            SwapTextures();
        }

        public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(texture, _position, Color.White);

        public void BasicFunction(Barricade barricade, Clock clock, Spawner spawner, Shop shop, bool cheat)
        {
            if (_health > 0)
            {
                _position -= Vector2.Normalize(Vector2.UnitX) * _speed;
                if (_position.X <= 0)
                {
                    Environment.Exit(1);
                }
                HitBarricade(barricade, clock, cheat);
            }
            else
            {
                spawner.DeathCount++;
                shop.Money += shop.MoneyPerEnemy;
                _position = _basePosition;
                _health = _baseHealth;
            }
        }

        public void HitBarricade(Barricade barricade, Clock clock, bool cheat)
        {
            if (_health <= 0) { return; }
            if (Position.X > barricade.Position.X) {  return; }
            if (barricade.Health <= 0) { return; }
            _position.X = barricade.Position.X;
            clock.Countdown();
            if (!clock.Done) { return; }
            if (cheat) { barricade.Health = barricade.MaximumHealth; }
            else { barricade.Health -= _damage; }
        }
    }
}