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

        public void StartLoadViews(Dictionary<string, AssetHandle> assetHandles, GameState newGameState, PlayerProfile playerProfile)
        {
            loadedViews = false;
            loadingCoroutine = StartCoroutine(LoadViews(assetHandles, newGameState, playerProfile));
        }

        private IEnumerator LoadViews(Dictionary<string, AssetHandle> assetHandles, GameState newGameState, PlayerProfile playerProfile)
        {
            AsyncOperationHandle<GameObject> handle;

            foreach(var assetHandleEntry in assetHandles)
            {
                var assetHandle = assetHandleEntry.Value;
                handle = Addressables.LoadAssetAsync<GameObject>(assetHandle.name);
                yield return handle;
                if(handle.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.Log($"Failed to load asset '{assetHandle.name}'");
                }
                handle = Addressables.InstantiateAsync(assetHandle.name, menuRoot, false);
                yield return handle;
                if(handle.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.Log($"Failed to create object from asset '{assetHandle.name}'");
                }
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
/*
LZ4 - Размер файла: 1.37 МиБ - загрузка на диск
    Время загрузки AssetBundle: 800 миллисекунд, 222, 800, 206, 137
    Время загрузки изображения: 50 миллисекунд, 34, 41, 44, 33

LZMA - Размер файла: 705 КиБ - загрузка на диск
    Время загрузки AssetBundle: 756, 796, 840, 850, 813
    Время загрузки изображения: 63, 38, 46, 41, 40

LZMA - Размер файла: 705 КиБ - загрузка в оперативную память
    Время загрузки AssetBundle: 876, 210, 210, 227, 270
    Время загрузки изображения: 32, 25, 36, 32, 56

Несжатый - Размер файла: 32 МиБ - загрузка на диск
    Время загрузки AssetBundle: 801, 821, 881, 853, 824    Повторная загрузка с диска: 52, 55, 61, 67, 64
    Время загрузки изображения: 37, 54, 37, 39, 46                                     41, 43, 41, 39, 49

Несжатый - Размер файла: 32 МиБ - загрузка в оперативную память
    Время загрузки AssetBundle: 249, 288, 251, 235, 234
    Время загрузки изображения: 36, 33, 39, 41, 41



До этого места, при записи на диск исользовалось сжатие.
Запись на диск без сжатия:

LZMA - Размер файла: 705 КиБ - загрузка на диск
    Время загрузки AssetBundle: 929, 782, 802, 788, 847
    Время загрузки изображения: 31, 34, 43, 41, 42



Разница не очень заметна. Скорее всего, на более сложных данных - будет заметнее.
При сохранении на диск без сжатия - немного более быстрая загрузка. Так же немного быстрее загружается из оперативной памяти.
Но хранить много всего в оперативной памяти - не хватит памяти. На диске со сжатием загрузка не намного дольше, а места занимает меньше.
Но некоторые данные могут плохо сжиматься.
Так же, разница в размере файла будет заметнее при низкой скорости соединения.
*/
}
