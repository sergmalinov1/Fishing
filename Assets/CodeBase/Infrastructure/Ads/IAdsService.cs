using CodeBase.Infrastructure.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Ads
{
    public interface IAdsService : IService
    {
        event Action RewardedVideoReady;
        bool IsRewardedVideoReady { get; }
        int Reward { get; }
        void Initialize();
        void ShowRewardedVideo(Action onVideoFinished);
    }
}
