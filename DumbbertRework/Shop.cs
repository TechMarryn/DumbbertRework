using System;
using Microsoft.Xna.Framework.Input;

namespace DumbbertRework
{
    class Shop
    {
        private int _UpgradeHealthCost, _upgradeDamageCost, _money = 500;
        private readonly int _upgradeHealthValue, _upgradeDamageValue, _restoreHealthCost, _moneyPerKill;

        public int Money
        {
            get => _money;
            set => _money = value;
        }

        public int MoneyPerEnemy => _moneyPerKill;

        public Shop(int upgradeDamageValue, int upgradeHealthValue, int upgradeDamageCost, int upgradeHealthCost, int restoreHealthCost, int moneyPerKill)
        {
            _upgradeDamageValue = upgradeDamageValue;
            _upgradeHealthValue = upgradeHealthValue;
            _upgradeDamageCost = upgradeDamageCost;
            _UpgradeHealthCost = upgradeHealthCost;
            _restoreHealthCost = restoreHealthCost;
            _moneyPerKill = moneyPerKill;
        }

        public void Update(Gun gun, Barricade barricade, bool cheat)
        {
            if (cheat) { _money = 32767; }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad1)) { UpgradeDamage(gun); }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2)) { UpgradeHealth(barricade); }
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad3)) { RestoreHealth(barricade); }
        }

        public void UpgradeDamage(Gun gun)
        {
            if (_money < _upgradeDamageCost) { return; }
            _money -= _upgradeDamageCost;
            gun.Dmg += _upgradeDamageValue;
            _upgradeDamageCost += Convert.ToInt16(_upgradeDamageCost * 0.2);
        }

        public void UpgradeHealth(Barricade barricade)
        {
            if (_money < _UpgradeHealthCost) { return; }
            _money -= _UpgradeHealthCost;
            barricade.MaximumHealth += _upgradeHealthValue;
            barricade.Health += _upgradeHealthValue;
            _UpgradeHealthCost += Convert.ToInt16(_UpgradeHealthCost * 0.2);
        }

        private void RestoreHealth(Barricade barricade)
        {
            if (_money < _restoreHealthCost) { return; }
            if (barricade.Health >= barricade.MaximumHealth) { return; }
            _money -= _restoreHealthCost;
            barricade.Health = barricade.MaximumHealth;
        }
    }
}
