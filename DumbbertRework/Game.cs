using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DumbbertRework
{
    internal class Game : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Enemy[] enemies = new(41);
        Barricade barricade;
        Gun gun;
        Spawner spawner;
        Clock timerBasic, timerEnemy, timerEnemySpawn;
        Texture2D background, money, killCount, skin1, skin2, skin3, skin4;
        SpriteFont font;
        Shop shop;
        Boss boss;
        Cheats cheats;
        Color backgroundColor;
        GameState gameState;
        List<Button> menuButtons;
        List<Button> gameButtons;
        List<Button> skinButtons;
        List<int> enemyCount = new() {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
        int windowHeight = 900;
        int windowWidth = 1800;
        int debug;

        public Game()
        {
            graphics = new(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            //enemy
            float enemyDamage = 10;
            float enemySpeed = 1f;
            double enemyHealth = 100;
            int enemySizeX = 50;
            int enemySizeY = 50;
            Color enemyColor = Color.White;
            Vector2 enemyPosition = new(1800, 800);
            Vector2 spawnPosition = new(1800, 800);

            //gun
            double gunDamage = 0.75;
            int gunSizeX = 1200;
            int gunSizeY = 50;
            Vector2 gunPostion = new(400, 750);

            //barricade
            float barricadeHealth = 200;
            int barricadeSizeX = 20;
            Vector2 barricadePosition = new(600, 800);

            //timer
            double timerBasicSeconds = 2;
            double timerEnemySeconds = 2;
            int timerBasicRemove = 1;
            int timerEnemyRemove = 1;
            int timerBasicLimit = 60;
            int timerEnemyLimit = 60;

            //shop
            double upgradeDamageValue = 0.5;
            int upgradeHealthValue = 200;
            int upgradeDamageCost = 300;
            int upgradeHealthCost = 300;
            int restoreHealthCost = 600;
            int moneyPerKill = 30;

            //boss
            double bossHealth = 6000;
            float bossSpeed = 0.25f;
            int bossSizeX = 1000;
            int bossSizeY = 500;
            Vector2 bossSpawnPosition = new(1800, 800 - bossSizeY + 30);

            //classes
            spriteBatch = new(GraphicsDevice);
            gun = new(gunDamage, graphics.GraphicsDevice, gunPostion, gunSizeX, gunSizeY, Content);
            barricade = new(barricadeHealth, barricadePosition, barricadeSizeX, Content);
            timerBasic = new(timerBasicSeconds, timerBasicRemove, timerBasicLimit);
            timerEnemy = new(timerEnemySeconds, timerEnemyRemove, timerEnemyLimit);
            timerEnemySpawn = new(2, 1, 60);
            font = Content.Load<SpriteFont>("font");
            background = Content.Load<Texture2D>("background");
            money = Content.Load<Texture2D>("money");
            killCount = Content.Load<Texture2D>("killCount");
            skin1 = Content.Load<Texture2D>("skins/skin_nam");
            skin2 = Content.Load<Texture2D>("skins/skin_ter");
            skin3 = Content.Load<Texture2D>("skins/skin_scam");
            skin4 = Content.Load<Texture2D>("skin_something");
            spawner = new();
            shop = new(upgradeDamageValue, upgradeHealthValue, upgradeDamageCost, upgradeHealthCost, restoreHealthCost, moneyPerKill);
            boss = new(bossHealth, bossSpeed, bossSizeX, bossSizeY, bossSpawnPosition, graphics.GraphicsDevice, Content);
            foreach (var e in enemyCount)
            {
                enemies[e] = new(enemyDamage, enemyHealth, enemySpeed, enemySizeX, enemySizeY, enemyPosition, graphics.GraphicsDevice, enemyColor, windowWidth, windowHeight);
            }
            backgroundColor = Color.CornflowerBlue;
            base.LoadContent();
        }
    }
}
