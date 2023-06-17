namespace DumbbertRework
{
    internal class Animation
    {
        #region Variables
        private int _number;
        public string _textureNumber;
        private readonly int _seconds;
        private readonly Clock _swapping;
        #endregion

        #region Constructor
        public Animation(int seconds)
        {
            _seconds = seconds;
            _swapping = new Clock(_seconds);
        }
        #endregion

        #region Functions
        public void SwapTexture()
        {
            if (SwappingDone() == "True" && _number == 2) { _number = 1; }
            if (SwappingDone() == "True" && _number == 1) { _number = 2; }
        }

        public void Update()
        {
            _swapping.Update();
            SwapTexture();
            TextureNumber();
            SwappingDone();
        }

        public string TextureNumber() => _number.ToString();

        public string SwappingDone() => _swapping.Done().ToString();
        #endregion
    }
}
