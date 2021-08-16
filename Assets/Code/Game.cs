using System;
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
        private AssetBundle assetBundle;

        private void Awake()
        {
            gameController = new GameController(this);
            //StartCoroutine(LoadAssetBundle("http://localhost:8080/test"));
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
                handle = Addressables.InstantiateAsync(assetHandle.name, menuRoot, false);
                yield return handle;
                assetHandle.assetObject = handle.Result;
            }

            loadedViews = true;
            playerProfile.GameState.Value = newGameState;
            if(loadingCoroutine != null)
                StopCoroutine(loadingCoroutine);
            loadingCoroutine = null;
        }

        private IEnumerator LoadAssetBundle(string name)
        {
            var startTime = DateTime.Now;
            var request = UnityWebRequestAssetBundle.GetAssetBundle(name, 46, 0);
            //var request = UnityWebRequestAssetBundle.GetAssetBundle(name);
            yield return request.SendWebRequest();

            if(request.isDone && request.error == null)
            {
                Debug.Log(request.downloadedBytes + " bytes.");
                assetBundle = DownloadHandlerAssetBundle.GetContent(request);
            }
            else
                Debug.Log($"Error while loading asset bundle {name}.");

            var endTime = DateTime.Now;
            Debug.Log($"Loaded AssetBundle in: {(endTime - startTime).TotalMilliseconds} milliseconds.");
            
            startTime = endTime;
            var loadedObject = assetBundle.LoadAsset("assets/textures/image2.png");
            if(loadedObject == null)
                Debug.Log("Failed to load image.");
            endTime = DateTime.Now;
            Debug.Log($"Loaded image in: {(endTime - startTime).TotalMilliseconds} milliseconds.");
        }
    }
}
