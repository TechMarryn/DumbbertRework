namespace DumbbertRework
{
    internal class Clock
    {
        #region Variables
        private float _time;
        private readonly int _base;
        private bool _ran;
        private readonly float _second;
        public string text;
        private int _number;
        private bool _done;
        private string done;
        #endregion

        #region Constructor
        public Clock(int seconds)
        {
            _base = seconds;
            _time = seconds;
            _second = 1;
        }
        #endregion

        #region Functions
        public void Update()
        {
            Done();
            TextTime();
            TextDone();

            _ran = false;
            if (_number <= 60)
            {
                _number += 1;
            }

            if (_number >= 60)
            {
                _number = 0;
                _time -= _second;
            }

            if (_time <= 0)
            {
                _ran = true;
                _time = _base;
            }
        }

        public string TextTime() => _time.ToString();

        public string TextDone() => _done.ToString();

        public bool Done()
        {
            if (_ran == true) { return _done = true; }
            else { return _done = false; }
        }
        #endregion
    }
}
