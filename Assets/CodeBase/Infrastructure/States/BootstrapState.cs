using CodeBase.GameLogic;
using CodeBase.Infrastructure.Ads;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Inventory;
using CodeBase.Infrastructure.RandomService;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.IAP;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.WindowsService;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
  public class BootstrapState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;
    
    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;

      RegisterServices();
    }

    public void Enter()
    {
      _sceneLoader.Load(Constants.InitialScene, onLoaded: EnterMenuScene);
    }

    public void Exit()
    {
      
    }

    private void EnterMenuScene() => 
      _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            RegisterStaticData();
            RegisterAdsService();

            _services.RegisterSingle<IGameStateMachine>(_stateMachine);
            _services.RegisterSingle<IInputService>(RegisterInputService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());

            _services.RegisterSingle<IPersistentProgress>(new PersistentProgress());

            RegisterIAPService(new IAPProvider(), _services.Single<IPersistentProgress>());

            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(
                _services.Single<IPersistentProgress>()));

            _services.RegisterSingle<IRandomService>(new UnityRandomService(
                _services.Single<IAssetProvider>(),
                _services.Single<IPersistentProgress>(),
                _services.Single<IStaticDataService>()));

            _services.RegisterSingle<IInventoryService>(new InventoryService(
                _services.Single<IAssetProvider>(),
                _services.Single<IPersistentProgress>(),
                _services.Single<IStaticDataService>(),
                _services.Single<ISaveLoadService>(),
                _services.Single<IRandomService>()));

            _services.RegisterSingle<IUIFactory>(new UIFactory(
                _services.Single<IStaticDataService>(),
                _services.Single<IAssetProvider>(),
                _services.Single<IPersistentProgress>(),
                _services.Single<IInventoryService>(),
                _services.Single<IAdsService>()));

            _services.RegisterSingle<IWindowService>(new WindowService(_services.Single<IUIFactory>()));

            _services.RegisterSingle<IGameFactory>(new GameFactory(
                _services.Single<IAssetProvider>(),
                _services.Single<IWindowService>(),
                _services.Single<IPersistentProgress>(),
                _services.Single<IStaticDataService>(),
                _services.Single<ISaveLoadService>(),
                _services.Single<IRandomService>(),
                _services.Single<IInventoryService>()));
        }

        private void RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.Load();
            _services.RegisterSingle<IStaticDataService>(staticData);
        }

        private void RegisterAdsService()
        {
            AdsService adsService = new AdsService();
            adsService.Initialize();
            _services.RegisterSingle<IAdsService>(adsService);
        }

        private static IInputService RegisterInputService()
        {
            if (Application.isEditor)
                return new StandaloneInputService();
            else
                return new MobileInputService();
        }

        private void RegisterIAPService(IAPProvider iapProvider, IPersistentProgress progressService)
        {
            IAPService iapService = new IAPService(iapProvider, progressService);
            iapService.Initialize();
            _services.RegisterSingle<IIAPService>(iapService);
        }
    }
}