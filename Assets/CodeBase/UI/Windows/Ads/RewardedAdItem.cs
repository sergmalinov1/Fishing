﻿using CodeBase.Infrastructure.Ads;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Ads
{
    public class RewardedAdItem : MonoBehaviour
    {
        public Button ShowAdButton;
        public GameObject[] AdActiveObjects;
        public GameObject[] AdInactiveObjects;
        private IAdsService _adsService;
        private IPersistentProgress _progressService;

        public void Construct(IAdsService adsService, IPersistentProgress progressService)
        {
            _adsService = adsService;
            _progressService = progressService;
        }

        public void Initialize()
        {
            ShowAdButton.onClick.AddListener(OnShowAdClicked);

            RefreshAvailableAd();
        }

        public void Subscribe() =>
          _adsService.RewardedVideoReady += RefreshAvailableAd;

        public void Cleanup() =>
          _adsService.RewardedVideoReady -= RefreshAvailableAd;

        private void OnShowAdClicked() =>
          _adsService.ShowRewardedVideo(OnVideoFinished);

        private void OnVideoFinished()
        {
            _progressService.Progress.MoneyData.Add(_adsService.Reward);
        }
          

        private void RefreshAvailableAd()
        {
            bool videoReady = _adsService.IsRewardedVideoReady;

            foreach (GameObject adActiveObject in AdActiveObjects)
                adActiveObject.SetActive(videoReady);

            foreach (GameObject adInactiveObject in AdInactiveObjects)
                adInactiveObject.SetActive(!videoReady);

        }
    }
}
