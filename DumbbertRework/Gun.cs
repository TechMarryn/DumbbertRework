using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DumbbertRework
{
    class Gun
    {
        private int _gunDamage, _width;
        private readonly Texture2D texture;
        private readonly GraphicsDevice _graphicsDevice;
        private readonly int _height, widthMaximum;
        private Vector2 _position;
        private Rectangle rectangle;
        private Texture2D _gunTexture;
        private readonly ContentManager _content;
        private readonly Animation animation;
        private int _whichTexture = 0;

        public int Dmg
        {
            get => _gunDamage;
            set => _gunDamage = value;
        }

        public Vector2 Position => _position;

        public int WhichTexture
        {
            get => _whichTexture;
            set => _whichTexture = value;
        }

        public Texture2D GunTexture
        {
            get => _gunTexture;
            set => _gunTexture = value;
        }

        public Gun(int gunDamage, GraphicsDevice graphicsDevice, Vector2 position, int width, int height, ContentManager content)
        {
            _content = content;
            animation = new(1, 1, 30);
            _gunDamage = gunDamage;
            _graphicsDevice = graphicsDevice;
            _position = position;
            _width = widthMaximum = width;
            _height = height;
            texture = ReadyTexture();
            rectangle = RayCastSize();
            _gunTexture = content.Load<Texture2D>("lmg/lmg_basic"); ;
        }

        private Texture2D ReadyTexture()
        {
            Texture2D texture;

            Color[] textureColorData = new Color[_width * _height];

            for (int i = 0; i < textureColorData.Length; i++)
            {
                textureColorData[i] = Color.White;
            }

            texture = new(_graphicsDevice, _width, _height);
            texture.SetData(textureColorData);

            return texture;
        }

        private void SwappingTextures(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                if (animation.AttackTexture == 1)
                {
                    switch (WhichTexture)
                    {
                        case 0:
                            _gunTexture = _content.Load<Texture2D>("lmg/lmg_basic");
                            break;
                        case 1:
                            _gunTexture = _content.Load<Texture2D>("skiny/tyler_nam");
                            break;
                        case 2:
                            _gunTexture = _content.Load<Texture2D>("skiny/tyler_scam");
                            break;
                        case 3:
                            _gunTexture = _content.Load<Texture2D>("skiny/tyler_something");
                            break;
                        case 4:
                            _gunTexture = _content.Load<Texture2D>("skiny/tyler_ter");
                            break;
                    }
                }
                else
                {
                    switch (WhichTexture)
                    {
                        case 0:
                            _gunTexture = _content.Load<Texture2D>("lmg/lmg_vystrel");
                            break;
                        case 1:
                            _gunTexture = _content.Load<Texture2D>("skiny/tyler_nam_vystrel");
                            break;
                        case 2:
                            _gunTexture = _content.Load<Texture2D>("skiny/tyler_scam_vystrel");
                            break;
                        case 3:
                            _gunTexture = _content.Load<Texture2D>("skiny/tyler_something_vystrel");
                            break;
                        case 4:
                            _gunTexture = _content.Load<Texture2D>("skiny/tyler_ter_vystrel");
                            break;
                    }
                }
            }
            else if (keyboardState.IsKeyUp(Keys.Space))
            {
                switch (WhichTexture)
                {
                    case 0:
                        _gunTexture = _content.Load<Texture2D>("lmg/lmg_basic");
                        break;
                    case 1:
                        _gunTexture = _content.Load<Texture2D>("skiny/tyler_nam");
                        break;
                    case 2:
                        _gunTexture = _content.Load<Texture2D>("skiny/tyler_scam");
                        break;
                    case 3:
                        _gunTexture = _content.Load<Texture2D>("skiny/tyler_something");
                        break;
                    case 4:
                        _gunTexture = _content.Load<Texture2D>("skiny/tyler_ter");
                        break;
                }
            }
        }

        public void Aktualizovat(KeyboardState keyboardState, Enemy enemy, bool cheat)
        {
            RectangleIntersects(keyboardState, enemy, cheat);
            ReadyTexture();
            RayCastSize();
        }

        public void Update(KeyboardState keyboardState, Boss boss, Spawner spawner, bool cheat)
        {
            RectangleIntersects(keyboardState, boss, spawner, cheat);
            ReadyTexture();
            RayCastSize();
        }

        public void Vykreslit(SpriteBatch spriteBatch) => spriteBatch.Draw(texture, rectangle, Color.Transparent);

        private void RectangleIntersects(KeyboardState keyboardState, Enemy enemy, bool cheat)
        {
            if (rectangle.Intersects(enemy.Hitbox))
            {
                if (_width > 50) { _width = Convert.ToInt16(enemy.Hitbox.X - _position.X); }
                else { _width = 1; }
                if (_width <= 5) { _width = 5; }
                ShootingEnemy(keyboardState, enemy, cheat);
            }
            else
            {
                if (_width < widthMaximum) { _width++; }
                else { _width = widthMaximum; }
            }
        }

        private void RectangleIntersects(KeyboardState keyboardState, Boss boss, Spawner spawner, bool cheat)
        {
            if (rectangle.Intersects(boss.Hitbox))
            {
                if (_width > 50) { _width = Convert.ToInt16(boss.Hitbox.X - _position.X); }
                else { _width = 1; }
                if (_width <= 5) { _width = 5; }
                ShootingBoss(keyboardState, boss, cheat);
            }
            else
            {
                if (_width < widthMaximum) { _width++; }
                else { _width = widthMaximum; }
                if (spawner.BossSpawned) { _width = widthMaximum; }
            }
        }

        private Rectangle RayCastSize() => new((int)_position.X, (int)_position.Y, _width, _height);

        public void ShootingEnemy(KeyboardState keyboardState, Enemy enemy, bool cheat)
        {
            if (!keyboardState.IsKeyDown(Keys.Space)) { return; }
            if (cheat)
            {
                enemy.HP -= 32767;
            }
            else
            {
                enemy.HP -= _gunDamage;
            }
        }

        public void ShootingBoss(KeyboardState keyboardState, Boss boss, bool cheat)
        {
            if (!keyboardState.IsKeyDown(Keys.Space)) { return; }
            if (cheat)
            {
                boss.HP -= 32767;
            }
            else
            {
                boss.HP -= _gunDamage;
            }
        }

        public void UpdateTexture(KeyboardState keyboardState)
        {
            animation.Update();
            SwappingTextures(keyboardState);
        }
    }
}
