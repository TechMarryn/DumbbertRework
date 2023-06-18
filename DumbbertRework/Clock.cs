namespace DumbbertRework
{
    internal class Clock
    {
        private readonly int basic, second, limit;
        private int number, time, _secondsUntilStronger = 30, _secondsUntilWave = 30;
        private bool _done;

        public bool Done { get => _done; }
        
        public int SecondsUntilWave
        {
            get => _secondsUntilWave;
            set => _secondsUntilWave = value;
        }
        
        public int SecondsUntilStronger
        {
            get => _secondsUntilStronger;
            set => _secondsUntilStronger = value;
        }

        public Clock(int seconds, int decreaseValue, int limit)
        {
            basic = time = seconds;
            second = decreaseValue;
            this.limit = limit;
        }

        public void Countdown()
        {
            _done = false;
            if (number <= limit) { number++; }
            else if (number > limit)
            {
                number = 0;
                time -= second;
            }

            if (time <= 0)
            {
                _done = true;
                time = basic;
            }
        }

        public void WaveCountdown()
        {
            Countdown();
            if (_done)
            {
                _secondsUntilWave--;
                _secondsUntilStronger--;
            }
        }
    }
}
