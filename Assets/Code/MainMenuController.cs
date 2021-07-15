using UnityEngine;

namespace Car
{
    internal sealed class MainMenuController : ControllerBase
    {
        private MainMenuView mainMenuView;
        private PlayerProfile playerProfile;

        private readonly ResourcePath mainMenuViewPath = new ResourcePath("MainMenuView");

        public MainMenuController(PlayerProfile _playerProfile, Transform menuRoot)
        {
            playerProfile = _playerProfile;
            mainMenuView = LoadView(menuRoot);
            mainMenuView.StartButton.onClick.AddListener(StartGame);
        }

        private MainMenuView LoadView(Transform menuRoot)
        {
            var mainMenuViewObject = Object.Instantiate(ResourcesLoader.Load(mainMenuViewPath), menuRoot, false);

            AddObject(mainMenuViewObject);

            return mainMenuViewObject.GetComponent<MainMenuView>();
        }

        private void StartGame()
        {
            playerProfile.GameState.Value = GameState.Game;
        }

        protected override void OnDispose()
        {
            mainMenuView.StartButton.onClick.RemoveAllListeners();
        }
    }
}
