using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DumbbertRework
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        #region Variables
        private readonly GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch spriteBatch;
        private Enemy enemy;
        private Barricade barricade;
        private Gun gun;
        private Clock clock;
        private SpriteFont font;
        private Animation animation;
        private int debug;
        private readonly int windowWidth = 900;
        private readonly int windowHeight = 1800;
        #endregion

        #region Constructor
        public Game()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        #endregion

        #region Functions
        protected override void Initialize()
        {
            graphicsDeviceManager.PreferredBackBufferHeight = windowWidth;
            graphicsDeviceManager.PreferredBackBufferWidth = windowHeight;
            graphicsDeviceManager.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new(GraphicsDevice);
            gun = new(.5);
            barricade = new(200, new(600,800), 20);
            enemy = new(10,
                        100,
                        3,
                        30,
                        50,
                        new(1700, 800),
                        graphicsDeviceManager.GraphicsDevice,
                        Color.White,
                        windowHeight,
                        windowWidth);
            font = Content.Load<SpriteFont>("font");
            clock = new(1);
            animation = new(1);
        }

        protected override void UnloadContent() {}

        #region Texty
        private string EnemyHp() => "enemy HP: " + enemy.HP;
        
        private string EnemyPositionX() => "poziceX: " + enemy.Position.X;

        private string EnemyPositionY() => "poziceY: " + enemy.Position.Y;

        private string BarricadeHp() => "barricade HP: " + barricade.Hp;
        #endregion

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape)) { Exit(); }
            if (keyboardState.IsKeyDown(Keys.F3)) { debug = 1; }
            if (keyboardState.IsKeyDown(Keys.F4)) { debug = 0; }
            gun.Fire(keyboardState, enemy);
            enemy.Update(graphicsDeviceManager, barricade);
            base.Update(gameTime);
            clock.Update();
            animation.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            enemy.Draw(spriteBatch);

            #region Text
            spriteBatch.DrawString(font, EnemyHp(), new Vector2(100, 100), Color.White);
            spriteBatch.DrawString(font, EnemyPositionX(), new Vector2(100, 115), Color.White);
            spriteBatch.DrawString(font, EnemyPositionY(), new Vector2(100, 130), Color.White);
            spriteBatch.DrawString(font, BarricadeHp(), new Vector2(100, 145), Color.White);
            #endregion

            #region Debug
            if (debug == 1)
            {
                spriteBatch.DrawString(font, clock.TextTime(), new(100, 160), Color.White);
                spriteBatch.DrawString(font, clock.TextDone(), new(100, 175), Color.White);
                spriteBatch.DrawString(font, animation.TextureNumber(), new(100, 190), Color.White);
                spriteBatch.DrawString(font, animation.SwappingDone(), new(100, 205), Color.White);
            }
            #endregion

            spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
    }
}