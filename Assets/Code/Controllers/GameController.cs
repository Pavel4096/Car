﻿using Car.Utilities;
using UnityEngine;

namespace Car
{
    internal sealed class GameController : ControllerBase
    {
        private Game game;
        private PlayerProfile playerProfile;
        private Property<GameState> gameState;
        private GameState previousGameState;
        private Property<float> moveProperty;
        private Property garageProperty;
        private Property abilitiesProperty;
        private RootController currentController;

        private const float carSpeed = 5.0f;

        public GameController(Game _game)
        {
            game = _game;
            gameState = new Property<GameState>();
            moveProperty = new Property<float>();
            garageProperty = new Property();
            abilitiesProperty = new Property();
            playerProfile = new PlayerProfile(new Car(carSpeed), gameState, new AnalyticsUtility(), new AdsUtility());
            gameState.Subscribe(StateChanged);

            gameState.Value = GameState.MainMenu;
            playerProfile.AnalyticsUtility.GameStartTime();
        }

        protected override void OnDispose()
        {
            gameState.Unsubscribe(StateChanged);
        }

        private void StateChanged(GameState gameState)
        {
            if(gameState == previousGameState)
                return;

            currentController?.Dispose();
            currentController = new RootController();
            switch(gameState)
            {
                case GameState.MainMenu:
                    currentController.AddController(new MainMenuController(playerProfile, game.menuRoot));
                    break;
                case GameState.Game:
                    var carController = new CarController(playerProfile);
                    currentController.AddController(carController);
                    currentController.AddController(new BackgroundController(playerProfile, moveProperty));
                    currentController.AddController(new InputController(moveProperty, garageProperty, abilitiesProperty));
                    currentController.CreateGameControllers(game.upgradeConfigs, game.abilityConfigs, carController,  game.menuRoot, garageProperty, abilitiesProperty, playerProfile.Car);
                    break;
            }
        }
    }
}
