using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DumbbertRework
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private readonly GraphicsDeviceManager graphicsDevice;
        private SpriteBatch spriteBatch;
        private readonly Enemy[] enemy = new Enemy[41];
        private Barricade barricade;
        private Gun gun;
        private Spawner spawner;
        private Clock basicClock, enemyClock, enemySpawnClock;
        private Texture2D background, money, killCount, skin1, skin2, skin3, skin4;
        private SpriteFont font;
        private Shop shop;
        private Boss boss;
        private Cheats cheats;
        private Color backgroundColor;
        private GameState gameState;
        private List<Button> menuButtons, skinButtons, gameButtons;
        private readonly List<int> enemyCount = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        private readonly int VyskaOkna = 900, SirkaOkna = 1800;
        private bool debug;

        public Game()
        {
            graphicsDevice = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphicsDevice.PreferredBackBufferHeight = VyskaOkna;
            graphicsDevice.PreferredBackBufferWidth = SirkaOkna;
            graphicsDevice.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new(GraphicsDevice);
            gun = new(1, graphicsDevice.GraphicsDevice, new(400, 750), 1200, 50, Content);
            barricade = new(200, new(600, 800), Content);
            basicClock = new(2, 1, 60);
            enemyClock = new(2, 1, 40);
            enemySpawnClock = new(2, 1, 60);
            font = Content.Load<SpriteFont>("font");
            background = Content.Load<Texture2D>("Background");
            money = Content.Load<Texture2D>("money");
            killCount = Content.Load<Texture2D>("killcount");
            skin1 = Content.Load<Texture2D>("skiny/tyler_nam");
            skin2 = Content.Load<Texture2D>("skiny/tyler_ter");
            skin3 = Content.Load<Texture2D>("skiny/tyler_scam");
            skin4 = Content.Load<Texture2D>("skiny/tyler_something");
            spawner = new();
            shop = new(1, 200, 300, 300, 600, 50);
            boss = new(6000, 0.25f, 1000, 500, new(1800, 330), Content);
            backgroundColor = Color.CornflowerBlue;
            cheats = new();

            for (int i = 0; i <= enemyCount.Capacity; i++)
            {
                enemy[i] = new(10, 100, 1f, 50, 80, new(1800, 700), Content);
            }

            var skinsButton = new Button(Content.Load<Texture2D>("tlacitka/tlacitko_menu"), font)
            {
                Position = new Vector2(1370, 325),
                Text = "Skins"
            };
            skinsButton.Click += SkinsButton_Clicked;

            var randomButton = new Button(Content.Load<Texture2D>("tlacitka/tlacitko_menu"), font)
            {
                Position = new Vector2(920, 325),
                Text = "Random"
            };
            randomButton.Click += RandomButton_Clicked;

            var quitButton = new Button(Content.Load<Texture2D>("tlacitka/tlacitko_menu"), font)
            {
                Position = new Vector2(470, 325),
                Text = "Quit"
            };
            quitButton.Click += QuitButton_Clicked;

            var playButton = new Button(Content.Load<Texture2D>("tlacitka/tlacitko_menu"), font)
            {
                Position = new Vector2(20, 325),
                Text = "New Game"
            };
            playButton.Click += PlayButton_Clicked;

            var upgradeDamageButton = new Button(Content.Load<Texture2D>("tlacitka/tlacitko_male"), font)
            {
                Position = new Vector2(400, 100),
                Text = "Upgrade Damage"
            };
            upgradeDamageButton.Click += UpgradeDamageButton_Clicked;

            var upgradeHealthButton = new Button(Content.Load<Texture2D>("tlacitka/tlacitko_male"), font)
            {
                Position = new Vector2(1000, 100),
                Text = "Upgrade Health"
            };
            upgradeHealthButton.Click += UpgradeHealthButton_Clicked;

            var skin1Button = new Button(Content.Load<Texture2D>("tlacitka/tlacitko_male"), font)
            {
                Position = new Vector2(1000, 40),
                Text = "Skin 1"
            };
            skin1Button.Click += Skin1Button_Clicked;

            var skin2Button = new Button(Content.Load<Texture2D>("tlacitka/tlacitko_male"), font)
            {
                Position = new Vector2(1000, 260),
                Text = "Skin 2"
            };
            skin2Button.Click += Skin2Button_Clicked;

            var skin3Button = new Button(Content.Load<Texture2D>("tlacitka/tlacitko_male"), font)
            {
                Position = new Vector2(1000, 480),
                Text = "Skin 3"
            };
            skin3Button.Click += Skin3Button_Clicked;

            var skin4Button = new Button(Content.Load<Texture2D>("tlacitka/tlacitko_male"), font)
            {
                Position = new Vector2(1000, 260 + 220 * 2),
                Text = "Skin 4"
            };
            skin4Button.Click += Skin4Button_Clicked;

            menuButtons = new()
            {
                randomButton,
                quitButton,
                playButton,
                skinsButton
            };

            gameButtons = new()
            {
                upgradeDamageButton,
                upgradeHealthButton
            };

            skinButtons = new()
            {
                skin1Button,
                skin2Button,
                skin3Button,
                skin4Button
            };
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var button in gameButtons) { button.Update(); }
            foreach (var button in menuButtons) { button.Update(); }
            foreach (var button in skinButtons) { button.Update(); }

            KeyboardState keyboardState = Keyboard.GetState();

            switch (gameState)
            {
                case GameState.Menu:
                    break;
                case GameState.Game:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { Exit(); }
                    if (Keyboard.GetState().IsKeyDown(Keys.F3)) { debug = !debug; }
                    gun.UpdateTexture(keyboardState);
                    cheats.Update();

                    if (spawner.EnemySpawned)
                    {
                        basicClock.Countdown();
                        basicClock.WaveCountdown();
                        for (int index = 1; index <= spawner.WhichEnemiesAreMoving.Capacity; index++)
                        {
                            enemy[index].Update(barricade, enemyClock, spawner, shop, cheats.BarricadeHealthOut());
                        }
                        foreach (int e in enemyCount)
                        {
                            shop.Update(gun, barricade, cheats.MoneyOut());
                            gun.Aktualizovat(keyboardState, enemy[e], cheats.GunDamageOut());
                            if (basicClock.SecondsUntilStronger == 0)
                            {
                                for (int index = 1; index <= enemyCount.Capacity; index++) { Spawner.MakeEnemiesStronger(enemy[index], basicClock); }
                                basicClock.SecondsUntilStronger = 30;
                            }
                        }
                    }
                    else
                    {
                        foreach (int e in enemyCount)
                        {
                            enemy[e].Position = enemy[e].ZakladniPozice;
                            enemy[e].HP = enemy[e].BaseHealth;
                            spawner.WhichEnemiesAreMoving.Clear();
                        }
                    }

                    if (spawner.BossSpawned) { gun.Update(keyboardState, boss, spawner, cheats.GunDamageOut()); }

                    shop.Update(gun, barricade, cheats.MoneyOut());
                    boss.Update(barricade, cheats.BarricadeHealthOut());
                    barricade.TextureSwap();
                    spawner.Update(basicClock, enemyCount, boss);
                    base.Update(gameTime);
                    break;

                case GameState.Skins:
                    break;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            IsMouseVisible = true;

            switch (gameState)
            {
                case GameState.Menu:
                    GraphicsDevice.Clear(backgroundColor);
                    spriteBatch.Begin();
                    foreach (var button in menuButtons) { button.Draw(spriteBatch); }
                    spriteBatch.End();
                    base.Draw(gameTime);
                    break;

                case GameState.Game:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    spriteBatch.Draw(background, new Vector2(0, 330), Color.White);
                    spriteBatch.Draw(gun.GunTexture, new Vector2(80, 632), Color.White);
                    spriteBatch.Draw(money, new Vector2(0, 800), Color.White);
                    spriteBatch.Draw(barricade.Texture, new Vector2(500, 578), Color.White);
                    spriteBatch.Draw(killCount, new Vector2(5, 840), Color.White);

                    foreach (var button in gameButtons) { button.Draw(spriteBatch); }

                    if (spawner.EnemySpawned)
                    {
                        foreach (int e in enemyCount) { enemy[e].Draw(spriteBatch); }
                    }
                    if (spawner.BossSpawned) { boss.Draw(spriteBatch); }
                    gun.Vykreslit(spriteBatch);
                    if (spawner.EnemySpawned)
                    {
                        foreach (int e in enemyCount)
                        {
                            spriteBatch.DrawString(font, "HP: " + enemy[e].HP, enemy[e].Position - new Vector2(10, 75), Color.Red);
                        }
                    }
                    if (spawner.BossSpawned) { spriteBatch.DrawString(font, "HP: " + boss.HP, boss.Pozice + new Vector2(200, 200), Color.Red); }
                    spriteBatch.DrawString(font, ": " + shop.Money, new Vector2(50, 820), Color.Gold);
                    spriteBatch.DrawString(font, ": " + spawner.DeathCount, new Vector2(50, 860), Color.White);
                    spriteBatch.DrawString(font, "Damage: " + gun.Dmg, gun.Position - new Vector2(180, 70), Color.Black);
                    spriteBatch.DrawString(font, barricade.Health.ToString(), new Vector2(540, 630), Color.White);

                    #region Debug
                    if (debug)
                    {
                        spriteBatch.DrawString(font, "All Cheats: " + cheats.StatusOut(), new Vector2(1500, 100), Color.Red);
                        spriteBatch.DrawString(font, "GunDMG Cheat: " + cheats.GunDamageOut(), new Vector2(1500, 115), Color.Red);
                        spriteBatch.DrawString(font, "Money Cheat: " + cheats.MoneyOut(), new Vector2(1500, 130), Color.Red);
                        spriteBatch.DrawString(font, "BarricadeHP Cheat: " + cheats.BarricadeHealthOut(), new Vector2(1500, 145), Color.Red);
                    }
                    #endregion

                    spriteBatch.End();
                    base.Draw(gameTime);
                    break;

                case GameState.Skins:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin();
                    foreach (var button in skinButtons) { button.Draw(spriteBatch); }
                    spriteBatch.Draw(skin1, new Vector2(50, 50), Color.White);
                    spriteBatch.Draw(skin3, new Vector2(50, 50 + 200), Color.White);
                    spriteBatch.Draw(skin4, new Vector2(50, 50 + 200 * 2), Color.White);
                    spriteBatch.Draw(skin2, new Vector2(50, 50 + 200 * 3), Color.White);
                    spriteBatch.End();
                    base.Draw(gameTime);
                    break;
            }
        }

        #region Buttons
        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    gameState = GameState.Game;
                    break;
            }
        }

        private void QuitButton_Clicked(object sender, EventArgs e)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    Exit();
                    break;
            }
        }

        private void RandomButton_Clicked(object sender, EventArgs e)
        {
            switch (gameState)
            {
                case GameState.Menu:
                    var random = new Random();
                    backgroundColor = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                    break;
            }
        }

        private void SkinsButton_Clicked(object sender, EventArgs e)
        {
            switch (gameState)
            {
                case GameState.Game:
                    break;
            }
        }

        private void UpgradeHealthButton_Clicked(object sender, EventArgs e)
        {
            switch (gameState)
            {
                case GameState.Game:
                    shop.UpgradeHealth(barricade);
                    break;
            }
        }

        private void UpgradeDamageButton_Clicked(object sender, EventArgs e)
        {
            switch (gameState)
            {
                case GameState.Game:
                    shop.UpgradeDamage(gun);
                    break;
            }
        }

        private void Skin1Button_Clicked(object sender, EventArgs e)
        {
            switch (gameState)
            {
                case GameState.Skins:
                    gun.WhichTexture = 1;
                    gameState = GameState.Menu;
                    break;
            }
        }

        private void Skin2Button_Clicked(object sender, EventArgs e)
        {
            switch (gameState)
            {
                case GameState.Skins:
                    gun.WhichTexture = 2;
                    gameState = GameState.Menu;
                    break;
            }
        }

        private void Skin3Button_Clicked(object sender, EventArgs e)
        {
            switch (gameState)
            {
                case GameState.Skins:
                    gun.WhichTexture = 3;
                    gameState = GameState.Menu;
                    break;
            }
        }

        private void Skin4Button_Clicked(object sender, EventArgs e)
        {
            switch (gameState)
            {
                case GameState.Skins:
                    gun.WhichTexture = 4;
                    gameState = GameState.Menu;
                    break;
            }
        }
        #endregion
    }
}
