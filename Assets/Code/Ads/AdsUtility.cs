using System;
using UnityEngine.Advertisements;

namespace Car
{
    public class AdsUtility : IAds, IUnityAdsInitializationListener, IUnityAdsListener
    {
        private bool isInitialized;
        private Action finishHandler;

        private const string gameAndroidID = "4228240";
        private const string gameIOSID = "4228241";
        private const string adUnitAndroidInterstitial = "Interstitial_Android";
        private const string adUnitIOSInterstitial = "Interstitial_iOS";

        public AdsUtility()
        {
            Advertisement.Initialize(GetGameID(), true, false, this);
        }

        public void ShowAd()
        {
            if(isInitialized)
            {
                Advertisement.Show(GetAdUnit());
            }
        }

        public void ShowAd(Action finish_handler)
        {
            finishHandler = finish_handler;
            ShowAd();
        }

        public void OnInitializationComplete()
        {
            isInitialized = true;
            Advertisement.AddListener(this);
            LoadAd();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
        }

        public void OnUnityAdsReady(string adUnitID)
        {}

        public void OnUnityAdsDidError(string message)
        {
            Finish();
        }

        public void OnUnityAdsDidStart(string adUnitID)
        {}

        public void OnUnityAdsDidFinish(string adUnitID, ShowResult result)
        {
            Finish();
        }

        private string GetGameID()
        {
#if UNITY_ANDROID
            return gameAndroidID;
#elif UNITY_IOS
            return gameIOSID;
#elif UNITY_EDITOR
            return gameAndroidID;
#else
            return gameAndroidID;
#endif
        }

        private string GetAdUnit()
        {
#if UNITY_ANDROID
            return adUnitAndroidInterstitial;
#elif UNITY_IOS
            return adUnitIOSInterstitial;
#elif UNITY_EDITOR
            return adUnitAndroidInterstitial;
#else
            return adUnitAndroidInterstitial;
#endif
        }

        private void LoadAd()
        {
            if(isInitialized)
            {
                Advertisement.Load(adUnitAndroidInterstitial);
            }
        }

        private void Finish()
        {
            finishHandler?.Invoke();
            finishHandler = null;
            LoadAd();
        }
    }
}
