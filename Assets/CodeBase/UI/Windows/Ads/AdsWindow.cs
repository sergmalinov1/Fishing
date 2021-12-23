using CodeBase.Infrastructure.Ads;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.UI.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.Windows.Ads
{
    public class AdsWindow : BaseWindow
    {

        public RewardedAdItem AdItem;
        public void Construct(IAdsService adsService, IPersistentProgress progressService)
        {

            AdItem.Construct(adsService, progressService);

        }

        protected override void Initialize()
        {
            AdItem.Initialize();
        }

        protected override void SubscribeUpdate()
        {
            AdItem.Subscribe();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            AdItem.Cleanup();
        }
    }

}