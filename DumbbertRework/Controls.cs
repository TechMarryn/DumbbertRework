using Microsoft.Xna.Framework.Input;

namespace DumbbertRework
{
    internal class ControlKeys
    {
        #region Properties
        public Keys RightDirection { get; private set; }
        public Keys LeftDirection { get; private set; }
        public Keys UpDirection { get; private set; }
        public Keys DownDirection { get; private set; }
        #endregion

        #region Constructor
        public ControlKeys(Keys leftDirection, Keys rightDirection, Keys upDirection, Keys downDirection)
        {
            RightDirection = rightDirection;
            LeftDirection = leftDirection;
            UpDirection = upDirection;
            DownDirection = downDirection;
        }
        #endregion
    }
}
