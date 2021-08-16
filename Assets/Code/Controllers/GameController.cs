using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using Car.Utilities;
using Car.Rewards;
using Car.Fight;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Car
{
    internal sealed class GameController : ControllerBase
    {
        private Game _game;
        private Property<GameState> gameState;
        private GameState previousGameState;
        private Property<float> moveProperty;
        private Property garageProperty;
        private Property abilitiesProperty;
        private RootController _currentController;
        private PlayerProfile _playerProfile;
        private IUserData _userData;

        private Dictionary<string, AssetHandle> assetHandles = new Dictionary<string, AssetHandle>();

        private const float carSpeed = 5.0f;
        private const string rewardsFileName = "data";
        private readonly ResourcePath rewards = new ResourcePath("Rewards");
        private readonly ResourcePath amountsInformation = new ResourcePath("AmountsInformation");
        private readonly ResourcePath fight = new ResourcePath("Fight");

        public GameController(Game game)
        {
            _game = game;
            gameState = new Property<GameState>();
            moveProperty = new Property<float>();
            garageProperty = new Property();
            abilitiesProperty = new Property();
            _playerProfile = new PlayerProfile(new Car(carSpeed), gameState, new AnalyticsUtility(), new AdsUtility());
            _userData = GetUserData();
            gameState.Subscribe(StateChanged);

            gameState.Value = GameState.MainMenu;
            _playerProfile.AnalyticsUtility.GameStartTime();
        }

        protected override void OnDispose()
        {
            gameState.Unsubscribe(StateChanged);
            Application.quitting -= SaveUserData;
        }

        private void StateChanged(GameState gameState)
        {
            if(gameState == previousGameState)
                return;

            if(!_game.loadedViews)
                DeleteObjects();

            if(gameState != GameState.Rewards)
            {
                _currentController?.Dispose();
                _currentController = new RootController();
            }
            switch(gameState)
            {
                case GameState.MainMenu:
                    _currentController.AddController(new MainMenuController(_playerProfile, _game.menuRoot));
                    break;
                case GameState.Game:
                    var carController = new CarController(_playerProfile);
                    _currentController.AddController(carController);
                    _currentController.AddController(new BackgroundController(_playerProfile, moveProperty));
                    _currentController.AddController(new InputController(moveProperty, garageProperty, abilitiesProperty));
                    _currentController.CreateGameControllers(_game.upgradeConfigs, _game.abilityConfigs, carController,  _game.menuRoot, garageProperty, abilitiesProperty, _playerProfile.Car);
                    break;
                case GameState.Rewards:
                    if(!_game.loadedViews)
                    {
                        assetHandles.Add("rewards", new AssetHandle{
                            name = "RewardsView"
                        });
                        assetHandles.Add("amountsInformation", new AssetHandle{
                            name = "AmountsInformationView"
                        });
                        _game.StartLoadViews(assetHandles, gameState, _playerProfile);
                        return;
                    }
                    _currentController?.Dispose();
                    _currentController = new RootController();
                    ShowRewards();
                    break;
                case GameState.Fight:
                    ShowFight();
                    break;
            }
            _game.loadedViews = false;
        }

        private void ShowRewards()
        {
            var amountsInformationController = new AmountsInformationController(assetHandles["amountsInformation"].assetObject.GetComponent<IAmountsInformationView>(), _userData);
            var rewardsController = new RewardsController(assetHandles["rewards"].assetObject.GetComponent<IRewardsView>(), amountsInformationController, _game.Rewards, _userData, _game.Item, _game.TimeToNext, _game.TimeToReset, _playerProfile);

            _currentController.AddController(amountsInformationController);
            _currentController.AddController(rewardsController);
        }

        private void ShowFight()
        {
            var fightView = _currentController.LoadAndAdd<FightView>(fight, _game.menuRoot);
            var fightController = new FightController(fightView, _playerProfile);

            _currentController.AddController(fightController);
        }

        private IUserData GetUserData()
        {
            UserData userData = null;

            if(File.Exists(rewardsFileName))
            {
                using(var inputStream = File.Open(rewardsFileName, FileMode.Open, FileAccess.Read))
                {
                    var xmlSerializer = new XmlSerializer(typeof(UserData));
                    userData = xmlSerializer.Deserialize(inputStream) as UserData;
                }
            }

            if(userData == null)
                userData = new UserData();
            userData.SetMaxRewardIndex(_game.Rewards.Length - 1);

            Application.quitting += SaveUserData;

            return userData;
        }

        private void SaveUserData()
        {
            using(var outputStream = File.Open(rewardsFileName, FileMode.Create, FileAccess.Write))
            {
                var xmlSerializer = new XmlSerializer(typeof(UserData));
                xmlSerializer.Serialize(outputStream, _userData);
            }
        }

        private void DeleteObjects()
        {
            foreach(var assetHandle in assetHandles.Values)
            {
                if(assetHandle.assetObject != null)
                    Addressables.Release<GameObject>(assetHandle.assetObject);
            }
            assetHandles.Clear();
        }
    }
}
