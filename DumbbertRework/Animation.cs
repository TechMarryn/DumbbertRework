namespace DumbbertRework
{
    internal class Animation
    {
        private readonly Clock swapping;
        private int _attackTexture, _enemyTexture;
        
        public int AttackTexture
        {
            get => _attackTexture;
            set => _attackTexture = value;
        }

        public int EnemyTexture
        {
            get => _enemyTexture;
            set => _enemyTexture = value;
        }

        public Animation(int seconds, int decreaseValue, int limit)
        {
            swapping = new(seconds, decreaseValue, limit);
        }

        public void Update()
        {
            swapping.Countdown();
            if (swapping.Done) { EnemyTexture = (int)(_enemyTexture >= 10 ? 1 : ++_enemyTexture); }
            if (swapping.Done) { AttackTexture = (int)(_attackTexture == 1 ? 2 : 1); }
        }
    }
}
