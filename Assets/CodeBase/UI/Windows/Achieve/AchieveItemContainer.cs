using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.UI.Windows.Achieve
{
  public class AchieveItemContainer : MonoBehaviour
  {
    
    public Transform Parent;
    
    private IPersistentProgress _progressService;
    private IAssetProvider _assetProvider;
    
    private readonly List<GameObject> _achieveItems = new List<GameObject>();

    public void Construct(IPersistentProgress progressService, IAssetProvider assetProvider)
    {
      _progressService = progressService;
      _assetProvider = assetProvider;
    }

    public void Initialize() => 
      RefreshAvailableItems();

    private async void RefreshAvailableItems()
    {
      foreach (CaughtFish caughtFish in _progressService.Progress.ResultOfFishing.CaughtFish)
      {
        GameObject achieveItemObject = await _assetProvider.Instantiate(Constants.AchieveItemPath, Parent);
        AchieveItem achieveItem = achieveItemObject.GetComponent<AchieveItem>();
        achieveItem.Construct(caughtFish.FishName, caughtFish.CountCaughtFish);
        
        
        _achieveItems.Add(achieveItemObject);
      
      }
    }
  }
}