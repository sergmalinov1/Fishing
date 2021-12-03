using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.WindowsService;
using CodeBase.UI.Windows;
using CodeBase.UI.Windows.Achieve;
using CodeBase.UI.Windows.EquipmentsCategory;
using CodeBase.UI.Windows.EquipmentsList;
using CodeBase.UI.Windows.PrepareState;
using CodeBase.UI.Windows.Shop;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public class UIFactory : IUIFactory
  {
    
    private readonly IStaticDataService _staticData;
    private readonly IAssetProvider _assetsProvider;
    private readonly IPersistentProgress _progressService;
    private Transform _uiRoot;

    public UIFactory(IStaticDataService staticData, IAssetProvider assetsProvider, IPersistentProgress progressService)
    {
      _staticData = staticData;
      _assetsProvider = assetsProvider;
      _progressService = progressService;
    }
    
    public async Task CreateUIRoot()
    {
      GameObject root = await _assetsProvider.Instantiate(Constants.UIRootPath);
      _uiRoot = root.transform;
    }

    public BaseWindow CreateResult()
    {
      WindowConfig config = _staticData.ForWindow(WindowId.Result);
      
      BaseWindow window = Object.Instantiate(config.Prefab, _uiRoot);
      FishingResult result = window as FishingResult;
      result.Construct(_progressService.Progress);
      return window;
    }

    public BaseWindow CreateShop()
    {
      WindowConfig config = _staticData.ForWindow(WindowId.ShopWindow);
      BaseWindow window = Object.Instantiate(config.Prefab, _uiRoot);
      ShopWindow shop = window as ShopWindow;
      shop.Construct(_progressService.Progress, _staticData, _assetsProvider);
      return window;
  
    }
    
    public BaseWindow CreateAchievements()
    {
      WindowConfig config = _staticData.ForWindow(WindowId.AchievementsWindow);
      BaseWindow window = Object.Instantiate(config.Prefab, _uiRoot);
      AchievementsWindow achievements = window as AchievementsWindow;
      achievements.Construct(_progressService, _assetsProvider);
      return window;
    }
    
    public BaseWindow CreatePrepareWindow()
    {
      WindowConfig config = _staticData.ForWindow(WindowId.PrepareWindow);
      BaseWindow window = Object.Instantiate(config.Prefab, _uiRoot);
      PrepareWindow prepareWindow = window as PrepareWindow;
      prepareWindow.Construct(_progressService.Progress, _staticData, _assetsProvider);
      prepareWindow.Initialize();
      return window;
    }

        public BaseWindow CreateSettingWindow()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.SettingWindow);
            BaseWindow window = Object.Instantiate(config.Prefab, _uiRoot);

            return window;
        }

        public BaseWindow CreateShopCategory()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.CategoryEquipment);
            BaseWindow window = Object.Instantiate(config.Prefab, _uiRoot);

            EquipmentCategoryWindow categoryWindows = window as EquipmentCategoryWindow;
            categoryWindows.Construct(this, _progressService.Progress, _assetsProvider, _staticData);


            return window;

        }

        public BaseWindow CreateListEquipment()
        {
            WindowConfig config = _staticData.ForWindow(WindowId.ListEquipment);
            BaseWindow window = Object.Instantiate(config.Prefab, _uiRoot);

            ListEquipmentsWindow listEquipments = window as ListEquipmentsWindow;
            listEquipments.Construct(_progressService.Progress);


            return window;

        }
    }
}