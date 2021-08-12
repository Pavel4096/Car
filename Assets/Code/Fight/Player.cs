using System;

namespace Car.Fight
{
    public class Player : IPlayer
    {
        private IPlayerView _playerView;
        private PlayerProperty _healthProperty;
        private PlayerProperty _moneyProperty;
        private PlayerProperty _powerProperty;
        private PlayerProperty _violenceProperty;
        private PlayerProperty _gunProperty;
        private PlayerProperty _knifeProperty;

        public Player(IPlayerView playerView)
        {
            _playerView = playerView;
            _healthProperty = new PlayerProperty(DataType.Health);
            _moneyProperty = new PlayerProperty(DataType.Money);
            _powerProperty =  new PlayerProperty(DataType.Power);
            _violenceProperty = new PlayerProperty(DataType.Violence);
            _gunProperty = new PlayerProperty(DataType.Gun);
            _knifeProperty = new PlayerProperty(DataType.Knife);

            _playerView.HealthChanged += _healthProperty.Change;
            _playerView.MoneyChanged += _moneyProperty.Change;
            _playerView.PowerChanged += _powerProperty.Change;
            _playerView.ViolenceChanged += _violenceProperty.Change;
            _playerView.GunChanged += _gunProperty.Change;
            _playerView.KnifeChanged += _knifeProperty.Change;

            _playerView.GunAttack += ProcessGunAttack;
            _playerView.KnifeAttack += ProcessKnifeAttack;
            _playerView.PassBy += ProcessPassBy;
        }

        public void Update()
        {
            _playerView.PlayerHealth = _healthProperty.Value;
            _playerView.PlayerMoney = _moneyProperty.Value;
            _playerView.PlayerPower = _powerProperty.Value;
            _playerView.PlayerViolence = _violenceProperty.Value;
            _playerView.PlayerGun = _gunProperty.Value;
            _playerView.PlayerKnife = _knifeProperty.Value;

            if(_violenceProperty.Value <= 2)
                _playerView.ShowPassByButton(true);
            else
                _playerView.ShowPassByButton(false);
        }

        public void AddEnemy(IEnemy enemy)
        {
            _healthProperty.Add(enemy);
            _moneyProperty.Add(enemy);
            _powerProperty.Add(enemy);
            _violenceProperty.Add(enemy);
        }

        public void RemoveEnemy(IEnemy enemy)
        {
            _healthProperty.Remove(enemy);
            _moneyProperty.Remove(enemy);
            _powerProperty.Remove(enemy);
            _violenceProperty.Remove(enemy);
        }

        public void Clear()
        {
            _playerView.HealthChanged -= _healthProperty.Change;
            _playerView.MoneyChanged -= _moneyProperty.Change;
            _playerView.PowerChanged -= _powerProperty.Change;
            _playerView.ViolenceChanged -= _violenceProperty.Change;
            _playerView.GunChanged -= _gunProperty.Change;
            _playerView.KnifeChanged -= _knifeProperty.Change;

            _playerView.GunAttack -= ProcessGunAttack;
            _playerView.KnifeAttack -= ProcessKnifeAttack;
            _playerView.PassBy -= ProcessPassBy;

            _healthProperty.RemoveAllEnemies();
            _moneyProperty.RemoveAllEnemies();
            _powerProperty.RemoveAllEnemies();
            _violenceProperty.RemoveAllEnemies();
            _gunProperty.RemoveAllEnemies();
            _knifeProperty.RemoveAllEnemies();
        }

        private void ProcessGunAttack(IEnemy enemy)
        {
            enemy.UpdateGun(_gunProperty.Value - enemy.Gun, !PlayerWon(_powerProperty.Value * _gunProperty.Value, enemy.Power * enemy.Gun));
        }

        private void ProcessKnifeAttack(IEnemy enemy)
        {
            enemy.UpdateKnife(_knifeProperty.Value - enemy.Knife, !PlayerWon(_powerProperty.Value * _knifeProperty.Value, enemy.Power * enemy.Knife));
        }

        private void ProcessPassBy(IEnemy enemy)
        {
            enemy.UpdateGun(_gunProperty.Value - enemy.Gun, true);
            enemy.UpdateKnife(_knifeProperty.Value - enemy.Knife, true);
        }

        private bool PlayerWon(float playerValue, float enemyValue)
        {
            if(playerValue >= enemyValue)
            {
                UnityEngine.Debug.Log("You won!");
                return true;
            }
            else
            {
                UnityEngine.Debug.Log("You lost!");
                return false;
            }
        }
    }
}
