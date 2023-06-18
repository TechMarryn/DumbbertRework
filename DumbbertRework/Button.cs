using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DumbbertRework
{
    internal class Button
    {
        private MouseState _mouseStateNow, _mouseStateBefore;
        private readonly SpriteFont _font;
        private bool _aboveButton;
        private readonly Texture2D _texture;
        private Color penColor;
        public event EventHandler Click;
        private Vector2 _position;
        private string _text;

        public Vector2 Position
        {
            get => _position;
            set => _position = value;
        }

        public Rectangle ButtonRectangle => new((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);

        public string Text
        {
            get => _text;
            set => _text = value;
        }

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            penColor = Color.Black;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var color = Color.White;

            if (_aboveButton) { color = Color.LightGray; }

            spriteBatch.Draw(_texture, ButtonRectangle, color);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (ButtonRectangle.X + (ButtonRectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (ButtonRectangle.Y + (ButtonRectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), penColor);
            }
        }

        public void Update()
        {
            _mouseStateBefore = _mouseStateNow;
            _mouseStateNow = Mouse.GetState();
            var mouseRectangle = new Rectangle(_mouseStateNow.X, _mouseStateNow.Y, 1, 1);
            _aboveButton = false;

            if (mouseRectangle.Intersects(ButtonRectangle))
            {
                _aboveButton = true;

                if (_mouseStateNow.LeftButton == ButtonState.Released && _mouseStateBefore.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
