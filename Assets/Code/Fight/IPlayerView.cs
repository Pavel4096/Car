using System;

namespace Car.Fight
{
    public interface IPlayerView
    {
        event Action<bool> HealthChanged;
        event Action<bool> MoneyChanged;
        event Action<bool> PowerChanged;
        event Action<bool> ViolenceChanged;
        event Action<bool> GunChanged;
        event Action<bool> KnifeChanged;
        event Action<IEnemy> GunAttack;
        event Action<IEnemy> KnifeAttack;
        event Action<IEnemy> PassBy;

        int PlayerHealth { set; }
        int PlayerMoney { set; }
        int PlayerPower { set; }
        int PlayerViolence { set; }
        int PlayerGun { set; }
        int PlayerKnife { set; }

        void ShowPassByButton(bool state);
    }
}
