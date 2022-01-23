using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.IAP;
using CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;


namespace CodeBase.UI.Windows.IAPPopup
{
    public class IAPWindow : BaseWindow
    {
        public ShopItemsContainer ShopItemsContainer;

        public void Construct(IPersistentProgress progressService, IIAPService iapService, IAssetProvider assetProvider)
        {
            ShopItemsContainer.Construct(iapService, progressService, assetProvider);
        }

        protected override void Initialize()
        {
            ShopItemsContainer.Initialize();
        }

        protected override void SubscribeUpdate()
        {
            ShopItemsContainer.Subscribe();
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            ShopItemsContainer.Cleanup();
        }
    }  
}
