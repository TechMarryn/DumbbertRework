using Microsoft.Xna.Framework.Input;

namespace DumbbertRework
{
    class Cheats
    {
        private bool A,B,E,H,H1,L,M,N,O,T,Y, status, statusGunDamage, statusMoney, statusBarricadeHealth = false;

        public void Update()
        {
            Status();
            GunDamage();
            Money();
            BarricadeHealth();
        }

        public void Status()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F9))
            {
                status = !status;
                statusGunDamage = !statusGunDamage;
                statusMoney = !statusMoney;
                statusBarricadeHealth = !statusBarricadeHealth;
            }
        }

        public void GunDamage()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.B)) { B = !B; }
            if (Keyboard.GetState().IsKeyDown(Keys.A) && B){ A = !A; }
            if (Keyboard.GetState().IsKeyDown(Keys.T) && A) { T = !T; }
            if (Keyboard.GetState().IsKeyDown(Keys.O) && T) { O = !O; }
            if (Keyboard.GetState().IsKeyDown(Keys.N) && O) { N = !N; }

            if (B && A && T && O && N)
            {
                statusGunDamage = !statusGunDamage;
                B = !B;
                A = !A;
                T = !T;
                O = !O;
                N = !N;
            }
        }

        public void Money()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.M)) { M = !M; }
            if (Keyboard.GetState().IsKeyDown(Keys.O) & M) { O = !O; }
            if (Keyboard.GetState().IsKeyDown(Keys.N) & O) { N = !N; }
            if (Keyboard.GetState().IsKeyDown(Keys.E) & N) { E = !E; }
            if (Keyboard.GetState().IsKeyDown(Keys.Y) & E) { Y = !Y; }

            if (M && O && N && E && Y)
            {
                statusMoney = !statusMoney;
                M = !M;
                O = !O;
                N = !N;
                E = !E;
                Y = !Y;
            }
        }

        public void BarricadeHealth()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.H)) { H = !H; }
            if (Keyboard.GetState().IsKeyDown(Keys.E) & H) { E = !E; }
            if (Keyboard.GetState().IsKeyDown(Keys.A) & E) { A = !A; }
            if (Keyboard.GetState().IsKeyDown(Keys.L) & A) { L = !L; }
            if (Keyboard.GetState().IsKeyDown(Keys.T) & L) { T = !T; }
            if (Keyboard.GetState().IsKeyDown(Keys.H) & T) { H1 = !H1; }

            if (H && E && A && L && T && H1)
            {
                statusBarricadeHealth = !statusBarricadeHealth;
                H = !H;
                E = !E;
                A = !A;
                L = !L;
                T = !T;
                H1 = !H1;
            }
        }

        public bool StatusOut() => status;

        public bool GunDamageOut() => statusGunDamage;

        public bool MoneyOut() => statusMoney;

        public bool BarricadeHealthOut() => statusBarricadeHealth;
    }
}
