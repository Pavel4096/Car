using System.Collections;
using System.Collections.Generic;
using Car.Utilities;
using Car.Rewards;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using Car.Inventory;

namespace Car
{
    public class Game : MonoBehaviour
    {
        public Transform menuRoot;
        public ItemUpgradeConfig[] upgradeConfigs;
        public AbilityConfig[] abilityConfigs;

        public ItemConfig[] configs;

        public bool loadedViews = false;
        public GameObject view1;
        public GameObject view2;

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

        private Coroutine loadingCoroutine;

        private void Awake()
        {
            gameController = new GameController(this);
        }

        private void Update()
        {
            UpdateUtility.GameUpdate();
        }

        public void StartLoadViews(List<AssetHandle> assetHandles, GameState newGameState, PlayerProfile playerProfile)
        {
            loadedViews = false;
            loadingCoroutine = StartCoroutine(LoadViews(assetHandles, newGameState, playerProfile));
        }

        private IEnumerator LoadViews(List<AssetHandle> assetHandles, GameState newGameState, PlayerProfile playerProfile)
        {
            AsyncOperationHandle<GameObject> handle;

            foreach(var assetHandle in assetHandles)
            {
                handle = Addressables.LoadAssetAsync<GameObject>(assetHandle.name);
                yield return handle;
                handle = Addressables.Instantiate(assetHandle.name, menuRoot, false);
                yield return handle;
                assetHandle.assetObject = handle.Result;
            }

            loadedViews = true;
            playerProfile.GameState.Value = newGameState;
            if(loadingCoroutine != null)
                StopCoroutine(loadingCoroutine);
            loadingCoroutine = null;
        }
    }
}
