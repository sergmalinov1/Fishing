using System.Threading.Tasks;
using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.IAP;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.UI.Windows.IAPPopup
{
    public class ShopItemsContainer : MonoBehaviour
    {
        private const string ShopItemPath = "ShopItem";

        public GameObject[] ShopUnavailableObjects;
        public Transform Parent;

        private IIAPService _iapService;
        private IPersistentProgress _progressService;
        private IAssetProvider _assetProvider;
        private readonly List<GameObject> _shopItems = new List<GameObject>();

        public void Construct(IIAPService iapService, IPersistentProgress progressService, IAssetProvider assetProvider)
        {
            _iapService = iapService;
            _progressService = progressService;
            _assetProvider = assetProvider;
        }

        public void Initialize() =>
            RefreshAvailableItems();

        public void Subscribe()
        {
            _iapService.Initialized += RefreshAvailableItems;
            _progressService.Progress.PurchaseData.Changed += RefreshAvailableItems;
        }

        public void Cleanup()
        {
            _iapService.Initialized -= RefreshAvailableItems;
            _progressService.Progress.PurchaseData.Changed -= RefreshAvailableItems;
        }

        private async void RefreshAvailableItems()
        {
            UpdateShopUnavailableItems();

            if (!_iapService.IsInitialized)
                return;

            ClearShopItems();

            await FillShopItems();
        }

        private void ClearShopItems()
        {
            foreach (GameObject shopItem in _shopItems)
                Destroy(shopItem);
        }

        private async Task FillShopItems()
        {
            foreach (ProductDescription productDescription in _iapService.Products())
            {
                GameObject shopItemObject = await _assetProvider.Instantiate(ShopItemPath, Parent);
                ShopItem shopItem = shopItemObject.GetComponent<ShopItem>();

                shopItem.Construct(_iapService, _assetProvider, productDescription);
                shopItem.Initialize();

                _shopItems.Add(shopItemObject);
            }
        }

        private void UpdateShopUnavailableItems()
        {
            foreach (GameObject shopUnavailableObject in ShopUnavailableObjects)
                shopUnavailableObject.SetActive(!_iapService.IsInitialized);
        }
    }
}
