using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Car.Fight
{
    public class FightController : ControllerBase
    {
        private IPlayer _player;
        private IEnemy _enemy;
        private FightView _fightView;
        private PlayerProfile _playerProfile;

        public FightController(FightView fightView, PlayerProfile playerProfile)
        {
            _fightView = fightView;
            _playerProfile = playerProfile;
            _player = new Player(_fightView);
            _enemy = new Enemy("Enemy", _fightView);
            SetupView();
            _player.AddEnemy(_enemy);
            _player.Update();
        }

        protected override void OnDispose()
        {
            _fightView.PlayerDataChanged -= UpdatePlayer;
            _fightView.ExitButton.onClick.RemoveAllListeners();
            _player.Clear();
        }

        private void SetupView()
        {
            _fightView.SetEnemy(_enemy);
            _fightView.PlayerDataChanged += UpdatePlayer;
            _fightView.ExitButton.onClick.AddListener(Quit);
        }

        private void UpdatePlayer()
        {
            _player.Update();
        }

        private void Quit()
        {
            _playerProfile.GameState.Value = GameState.MainMenu;
        }
    }
}
