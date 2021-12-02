using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.UI.Windows.Shop
{
  public class ShopItemsContainer : MonoBehaviour
  {
    public Transform Parent;
    
    private IAssetProvider _assetsProvider;
    private IStaticDataService _staticData;
    private PlayerProgress _progress;
    private ISaveLoadService _saveLoadService;

    private readonly List<GameObject> _products = new List<GameObject>();

    public void Construct(PlayerProgress progressServiceProgress, IStaticDataService staticData, IAssetProvider assetsProvider)
    {
      _progress = progressServiceProgress;
      _assetsProvider = assetsProvider;
      _staticData = staticData;
      _saveLoadService = AllServices.Container.Single<ISaveLoadService>();;

    }
    
    public void Initialize() => 
      RefreshAvailableItems();

    private async void RefreshAvailableItems()
    {
      foreach (KeyValuePair<LureTypeId, LureStaticData> lure in _staticData.Lures())
      {
        GameObject productObject = await _assetsProvider.Instantiate(Constants.ShopItemPath, Parent);
        ShopItem productItem = productObject.GetComponent<ShopItem>();
        productItem.Construct(_progress, lure.Value, _saveLoadService, _assetsProvider);
        productItem.Initialize();
        
        _products.Add(productObject);
      }
    }
  }
}