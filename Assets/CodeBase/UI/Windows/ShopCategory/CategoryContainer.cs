using System.Collections.Generic;
using CodeBase;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using CodeBase.StaticData.Hook;
using CodeBase.StaticData.Lure;
using CodeBase.UI.Windows.Shop;
using UnityEngine;


namespace CodeBase.UI.Windows.ShopCategory
{
    public class CategoryContainer : MonoBehaviour
    {
        public Transform Parent;

        private IAssetProvider _assetsProvider;
        private IStaticDataService _staticData;
        private PlayerProgress _progress;
        private ISaveLoadService _saveLoadService;

        private readonly List<GameObject> _hooks = new List<GameObject>();

        public void Construct(PlayerProgress progressServiceProgress, IStaticDataService staticData, IAssetProvider assetsProvider)
        {
            _progress = progressServiceProgress;
            _assetsProvider = assetsProvider;
            _staticData = staticData;
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>(); 

        }

        public void Initialize() =>  RefreshAvailableItems();

        private async void RefreshAvailableItems()
        {
            foreach (KeyValuePair<HookTypeId, HookStaticData> hook in _staticData.Hooks())
            {
                HookStaticData value = hook.Value;

                GameObject productObject=  await _assetsProvider.Instantiate(Constants.ShopCardPath, Parent);
               
                CategoryItem productItem = productObject.GetComponent<CategoryItem>();
                productItem.Construct((int)ItemTypeId.Hook);
                productItem.Initialize(value.HookName, (int)value.HookTypeId);

                //  _hooks.Add(productObject);
            }
        }
    }
}
