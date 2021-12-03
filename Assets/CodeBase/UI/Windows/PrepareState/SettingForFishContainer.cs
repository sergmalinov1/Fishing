using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.UI.Windows.PrepareState
{
  public class SettingForFishContainer : MonoBehaviour
  {
    public Transform Parent;
    
    private IAssetProvider _assetsProvider;
    private IStaticDataService _staticData;
    private PlayerProgress _progress;
    private ISaveLoadService _saveLoadService;
    private PrepareWindow _prepareWindow;

  //  private readonly List<GameObject> _lure = new List<GameObject>();

    public void Construct(
      PlayerProgress progressServiceProgress, 
      IStaticDataService staticData, 
      IAssetProvider assetsProvider, 
      PrepareWindow prepareWindow)
    {
      _progress = progressServiceProgress;
      _assetsProvider = assetsProvider;
      _staticData = staticData;
      _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
      _prepareWindow = prepareWindow;

    }


    public void Initialize() => 
      RefreshAvailableItems();

    private async void RefreshAvailableItems()
    {
    /*  foreach (InventoryLure item in _progress.Inventory.InventoryLures)
      {
        GameObject itemObject = await _assetsProvider.Instantiate(Constants.LureItemPreparePath, Parent);
        LureItemPrepare lureItem = itemObject.GetComponent<LureItemPrepare>();
        lureItem.Construct(_progress, _saveLoadService, _assetsProvider, _staticData, _prepareWindow);
        lureItem.Initialize(item);
        
      }*/
    }
  }
}