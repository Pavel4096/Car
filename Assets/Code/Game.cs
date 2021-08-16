using Car.Utilities;
using Car.Rewards;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;


using Car.Inventory;

namespace Car
{
    public class Game : MonoBehaviour
    {
        public Transform menuRoot;
        public ItemUpgradeConfig[] upgradeConfigs;
        public AbilityConfig[] abilityConfigs;

        public ItemConfig[] configs;

        [SerializeField]
        private GameObject _item;
        [SerializeField]
        private Reward[] _rewards;
        [SerializeField]
        private int _timeToNext = 1*60;
        [SerializeField]
        private int _timeToReset = 1*60;

        public GameObject Item => _item;
        public Reward[] Rewards => _rewards;
        public int TimeToNext => _timeToNext;
        public int TimeToReset => _timeToReset;
        
        private GameController gameController;

        private void Awake()
        {
            gameController = new GameController(this);
        }

        private void Update()
        {
            UpdateUtility.GameUpdate();
        }

        private void StartLoadViews(string name1, string name2, ref GameObject view1, ref GameObject view2, GameState newGameState, PlayerProfile playerProfile)
        {
            StartCoroutine(name1, name2, ref view1, ref view2, newGameState, playerProfile);
        }

        private IEnumerator LoadViews(string name1, string name2, ref GameObject view1, ref GameObject view2, GameState newGameState, PlayerProfile playerProfile)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(name1);

            yield return handle;
            handle = Addressables.Instantiate(handle.Result, menuRoot, false);
            yield return handle;
            view1 = handle.Result;
            handle = Addressables.LoadAssetAsync<GameObject>(name2);
            yield return handle;
            handle = Addressables.Instantiate(handle.Result, menuRoot, false);
            yield return handle;
            view2 = handle.Result;

            playerProfile.GameState.Value = newGameState;
        }
    }
}
