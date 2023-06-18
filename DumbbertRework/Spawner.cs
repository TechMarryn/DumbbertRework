using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace DumbbertRework
{
    class Spawner
    {
        private bool _enemySpawned = true;
        private bool _bossSpawned;
        private List<int> whoIsMoving = new(1);

        public bool EnemySpawned => _enemySpawned;

        public bool BossSpawned => _bossSpawned;

        public List<int> WhichEnemiesAreMoving
        {
            get => whoIsMoving;
            set => whoIsMoving = value;
        }
        
        public int DeathCount { get; set; }

        public void Update(Clock clock, List<int> enemyCount, Boss boss)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S)) { _enemySpawned = true; }
            if (Keyboard.GetState().IsKeyDown(Keys.A)) { _enemySpawned = false; }
            SpawnBoss(clock, boss);
            WhoIsMoving(clock, enemyCount);
        }

        public void WhoIsMoving(Clock clock, List<int> enemyCount)
        {
            if (whoIsMoving.Count >= enemyCount.Count) { return; }
            if (!clock.Done) { return; }
            whoIsMoving.Add(1);
        }

        private void SpawnBoss(Clock clock, Boss boss)
        {
            if (clock.SecondsUntilWave > 0) { return; }
            _enemySpawned = false;
            _bossSpawned = true;
            boss.Exists = true;
            if (boss.Died) { return; }
            _enemySpawned = true;
            clock.SecondsUntilWave = 60;
        }

        public static void MakeEnemiesStronger(Enemy enemy, Clock clock)
        {
            if (clock.SecondsUntilStronger > 0) { return; }
            enemy.HP += 20;
            enemy.BaseHealth += 20;
            enemy.Speed += enemy.Speed / 100 * 10;
        }
    }
}
