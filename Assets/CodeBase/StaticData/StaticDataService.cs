using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Fish;
using CodeBase.StaticData.Lure;
using CodeBase.StaticData.ShopTemp;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.WindowsService;
using UnityEngine;

namespace CodeBase.StaticData
{
  public class StaticDataService : IStaticDataService
  {
    private const string StaticDataFishesPath = "StaticData/Fish";
    private const string StaticDataLurePath = "StaticData/Lure";
    private const string StaticDataWindowsPath = "StaticData/Windows/WindowStaticData";
  //  private const string StaticDataShopPath = "StaticData/ShopTemp";
    
    private Dictionary<FishTypeId, FishStaticData> _fishes;
    private Dictionary<LureTypeId, LureStaticData> _lure;
    private Dictionary<WindowId, WindowConfig> _windows;
  //  private Dictionary<ShopId, ShopStaticData> _products;

    public Dictionary<FishTypeId, FishStaticData> Fishes() => _fishes;      
    public Dictionary<LureTypeId, LureStaticData> Lures() => _lure;      
  //  public Dictionary<ShopId, ShopStaticData> Products() => _products;      
      
    public void Load()
    {
      _fishes = Resources
        .LoadAll<FishStaticData>(StaticDataFishesPath)
        .ToDictionary(x => x.FishTypeId, x => x);
      
      _lure = Resources
        .LoadAll<LureStaticData>(StaticDataLurePath)
        .ToDictionary(x => x.LureTypeId, x => x);
      
      _windows = Resources
        .Load<WindowsStaticData>(StaticDataWindowsPath)
        .Configs
        .ToDictionary(x => x.WindowId, x => x);
      
     /* _products = Resources
        .LoadAll<ShopStaticData>(StaticDataShopPath)
        .ToDictionary(x => x.ShopId, x => x);*/
    }

    
    public FishStaticData ForFish(FishTypeId typeId) =>
      _fishes.TryGetValue(typeId,out FishStaticData staticData) 
        ? staticData 
        : null;
    
    public WindowConfig ForWindow(WindowId windowId) =>
      _windows.TryGetValue(windowId, out WindowConfig config)
        ? config
        : null;
    
    
    public LureStaticData ForLure(LureTypeId lureTypeId) =>
      _lure.TryGetValue(lureTypeId, out LureStaticData config)
        ? config
        : null;
    
  /*  public ShopStaticData ForProduct(ShopId productId) =>
      _products.TryGetValue(productId, out ShopStaticData config)
        ? config
        : null;*/
  }
}
