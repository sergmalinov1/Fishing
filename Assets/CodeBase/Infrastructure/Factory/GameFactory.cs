using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.GameLogic;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Inventory;
using CodeBase.Infrastructure.RandomService;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Logic;
using CodeBase.StaticData;
using CodeBase.StaticData.Fish;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.WindowsService;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assets;
    private readonly IWindowService _windowService;
    private readonly IPersistentProgress _progressService;
    private readonly IStaticDataService _staticData;
    private readonly ISaveLoadService _saveLoadService;
    private readonly IRandomService _randomService;

    public GameFactory(
      IAssetProvider assets, 
      IWindowService windowService, 
      IPersistentProgress progressService, 
      IStaticDataService staticData, 
      ISaveLoadService saveLoadService, 
      IRandomService randomService,
      IInventoryService inventoryService)
    {
      _assets = assets;
      _windowService = windowService;
      _progressService = progressService;
      _staticData = staticData;
      _saveLoadService = saveLoadService;
      _randomService = randomService;
    }


        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public async Task<GameObject> CreateHud()
        {
            GameObject hud = await InstantiateRegistredAsync(AssetsAddress.HudPath);


            foreach (OpenWindowButton openWindowButton in hud.GetComponentsInChildren<OpenWindowButton>())
                openWindowButton.Construct(_windowService);

            HudManager hudManager = hud.GetComponentInChildren<HudManager>();
            hudManager.Construct(_progressService);
            hudManager.Initialize();

            MoneyCounter moneyObj = hud.GetComponentInChildren<MoneyCounter>();
            moneyObj.Construct(_progressService.Progress); ;

            LureCounter lureCounter = hud.GetComponentInChildren<LureCounter>();
            lureCounter.Construct(_progressService.Progress); ;


            return hud;
        }

        public async Task<GameObject> CreareFishingLogic()
        {
            GameObject fishing = await InstantiateRegistredAsync(AssetsAddress.FishingLogic);

            LogicStateMachine bobberSpawner = fishing.GetComponent<LogicStateMachine>();
            bobberSpawner.Construct(this, _windowService, _progressService.Progress, _staticData, _saveLoadService, _randomService);
            bobberSpawner.Initialize();
            //  bobberSpawner.Enter<StartState>();


            return fishing;
        }

        public async Task<GameObject> CreateFish(FishTypeId fishTypeId, Vector3 at)
        {

            FishStaticData fishData = _staticData.ForFish(fishTypeId);

            GameObject prefab = await _assets.Load<GameObject>(fishData.PrefabReference);

            GameObject fish = UnityEngine.Object.Instantiate(prefab);
            fish.transform.position = at;
            return fish;
        }


        public async Task<GameObject> CreateBobber(Vector3 at)
        {
            GameObject bobber = await InstantiateRegistredAsync(AssetsAddress.BobberPrefab, at);
            return bobber;
        }

        public async Task<GameObject> CreateSplash(Vector3 at)
        {
            GameObject splash = await InstantiateRegistredAsync(AssetsAddress.SplashPrefab, at);
            return splash;
        }

        public async Task<GameObject> CreateFish(FishTypeId typeId, Transform parent)
        {
            FishStaticData fishData = _staticData.ForFish(typeId);

            GameObject prefab = await _assets.Load<GameObject>(fishData.PrefabReference);
            GameObject fish = UnityEngine.Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);

            return fish;
        }

        public async Task CreateBackground()
        {
            Vector3 position = new Vector3(0, 0, 25.0f);
            GameObject background = await InstantiateRegistredAsync(AssetsAddress.Background, position);
            BackgroundManager backgroundManager = background.GetComponent<BackgroundManager>();
            backgroundManager.Construct(_progressService);
            backgroundManager.Inizialize();
        }


        public async Task<GameObject> CreateTackleContainer(Vector3 position)
        {
            GameObject containerPrafab = await InstantiateRegistredAsync(AssetsAddress.TackleContainer, position);
            return containerPrafab;
        }

        public async Task<GameObject> CreateFishInContainer(TackleContainer tackleContainer, FishTypeId fishId)
        {
            FishStaticData fishData = _staticData.ForFish(fishId);
            GameObject prefab = await _assets.Load<GameObject>(fishData.PrefabReference);

          //  Equipment bobberEquipmentId = _staticData.GetEquipment(KindEquipmentId.Lure, lureId);
           // GameObject prefab = await _assets.Load<GameObject>(bobberEquipmentId.GetReference());

            Transform container = tackleContainer.OnHookContainer;
          //  GameObject fish = UnityEngine.Object.Instantiate(prefab, container.position, Quaternion.identity, container.transform);
            GameObject fish = UnityEngine.Object.Instantiate(prefab, container.transform);
            return fish;

        }

        public async Task<GameObject> CreateLureInContainer(TackleContainer tackleContainer, int lureId)
        {
            //TackleContainer equipmentContainer = containerPrafab.GetComponent<TackleContainer>();

            Equipment bobberEquipmentId = _staticData.GetEquipment(KindEquipmentId.Lure, lureId);
            GameObject prefab = await _assets.Load<GameObject>(bobberEquipmentId.GetReference());

            Transform container = tackleContainer.OnHookContainer;
            GameObject lure = UnityEngine.Object.Instantiate(prefab, container.position, Quaternion.identity, container.transform);
            return lure;

        }

        public async Task<GameObject> CreateBobberInContainer(TackleContainer tackleContainer, int bobberId)
        {
           // Debug.Log("CreateBobberInContainer");

           // TackleContainer equipmentContainer = containerPrafab.GetComponent<TackleContainer>();

            Equipment bobberEquipmentId = _staticData.GetEquipment(KindEquipmentId.Bobber, bobberId);
            GameObject prefab = await _assets.Load<GameObject>(bobberEquipmentId.GetReference());

            Transform container = tackleContainer.BobberContainer;
            //  tackleContainer.Bobber =
            GameObject bobber = UnityEngine.Object.Instantiate(prefab, container.position, Quaternion.identity, container.transform);
            return bobber;
        }


        private async Task<GameObject> InstantiateRegistredAsync(string prefabPath)
        {
            GameObject gameObject = await _assets.Instantiate(prefabPath);
            return gameObject;
        }

        private async Task<GameObject> InstantiateRegistredAsync(string prefabPath, Vector3 position)
        {
            GameObject gameObject = await _assets.Instantiate(prefabPath, position);
            return gameObject;
        }

 
    }
}