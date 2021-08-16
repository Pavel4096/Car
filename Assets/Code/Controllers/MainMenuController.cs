using Car.Utilities;
using UnityEngine;

namespace Car
{
    internal sealed class MainMenuController : ControllerBase
    {
        private MainMenuView _mainMenuView;
        private PlayerProfile _playerProfile;

        private readonly ResourcePath mainMenuViewPath = new ResourcePath("MainMenu");

        public MainMenuController(PlayerProfile playerProfile, Transform menuRoot)
        {
            _playerProfile = playerProfile;
            _mainMenuView = LoadView(menuRoot);
            _mainMenuView.StartButton.onClick.AddListener(StartGame);
            _mainMenuView.RewardsButton.onClick.AddListener(ShowRewards);
            _mainMenuView.FightButton.onClick.AddListener(ShowFight);
            _mainMenuView.ExitButton.onClick.AddListener(Quit);
        }

        protected override void OnDispose()
        {
            _mainMenuView.StartButton.onClick.RemoveAllListeners();
            _mainMenuView.RewardsButton.onClick.RemoveAllListeners();
            _mainMenuView.FightButton.onClick.RemoveAllListeners();
            _mainMenuView.ExitButton.onClick.RemoveAllListeners();
        }

        private MainMenuView LoadView(Transform menuRoot)
        {
            var mainMenuViewObject = Object.Instantiate(ResourceLoader.Load(mainMenuViewPath), menuRoot, false);

            AddObject(mainMenuViewObject);
            _playerProfile.AnalyticsUtility.MenuEntered("MainMenu");

            return mainMenuViewObject.GetComponent<MainMenuView>();
        }

        private void StartGame()
        {
            _playerProfile.AdsUtility.ShowAd(() => _playerProfile.GameState.Value = GameState.Game);
        }

        private void ShowRewards()
        {
            _playerProfile.GameState.Value = GameState.Rewards;
        }

        private void ShowFight()
        {
            _playerProfile.GameState.Value = GameState.Fight;
        }

        private void Quit()
        {
            Application.Quit();
        }
    }
}
