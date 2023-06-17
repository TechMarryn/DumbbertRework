using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DumbbertRework
{
    internal class Gun
    {
        #region Variables
        private readonly double _gunDamage;
        #endregion

        #region Constructor
        public Gun(double gunDamage)
        {
            _gunDamage = gunDamage;
        }
        #endregion

        #region Functions
        public void Fire(KeyboardState keyboardState, Enemy enemy)
        {
            if (keyboardState.IsKeyDown(Keys.Space)) { enemy.HP -= _gunDamage; }
        }
        #endregion
    }
}
