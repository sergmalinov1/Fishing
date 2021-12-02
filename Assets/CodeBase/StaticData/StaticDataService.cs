using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Fish;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Services.WindowsService;
using UnityEngine;

namespace CodeBase.StaticData
{
  public class StaticDataService : IStaticDataService
  {
        private const string StaticDataFishesPath = "StaticData/Fish";

        private const string StaticDataLurePath = "StaticData/Lure";
        private const string StaticDataHookPath = "StaticData/Hook";
        private const string StaticDataBobberPath = "StaticData/Bobber";
        private const string StaticDataFishingLinePath = "StaticData/FishingLine";
        private const string StaticDataFishingRodPath = "StaticData/FishingRod";


        private const string StaticDataWindowsPath = "StaticData/Windows/WindowStaticData";


        private Dictionary<WindowId, WindowConfig> _windows;

        private Dictionary<FishTypeId, FishStaticData> _fishes;

        private Dictionary<BobberTypeId, BobberStaticData> _bobber;
        private Dictionary<LureTypeId, LureStaticData> _lure;
  
        private Dictionary<HookTypeId, HookStaticData> _hooks;

        private Dictionary<FishingLineId, FishingLineStaticData> _fishingLine;

        private Dictionary<FishingRodId, FishingRodStaticData> _fishingRod;


        public Dictionary<FishTypeId, FishStaticData> Fishes() => _fishes;

        public Dictionary<BobberTypeId, BobberStaticData> Bobber() => _bobber;
        public Dictionary<LureTypeId, LureStaticData> Lures() => _lure;
        public Dictionary<HookTypeId, HookStaticData> Hooks() => _hooks;

        public Dictionary<FishingLineId, FishingLineStaticData> FishingLine() => _fishingLine;

        public Dictionary<FishingRodId, FishingRodStaticData> FishingRod() => _fishingRod;

        public void Load()
        {
            _windows = Resources
                .Load<WindowsStaticData>(StaticDataWindowsPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);

            _fishes = Resources
                .LoadAll<FishStaticData>(StaticDataFishesPath)
                .ToDictionary(x => x.FishTypeId, x => x);

            _lure = Resources
              .LoadAll<LureStaticData>(StaticDataLurePath)
              .ToDictionary(x => x.LureTypeId, x => x);



            _hooks = Resources
                .LoadAll<HookStaticData>(StaticDataHookPath)
                .ToDictionary(x => x.HookTypeId, x => x);

            _bobber = Resources
                .LoadAll<BobberStaticData>(StaticDataBobberPath)
                .ToDictionary(x => x.BobberTypeId, x => x);

            _fishingLine = Resources
                .LoadAll<FishingLineStaticData>(StaticDataFishingLinePath)
                .ToDictionary(x => x.FishingLineId, x => x);

            _fishingRod = Resources
                .LoadAll<FishingRodStaticData>(StaticDataFishingRodPath)
                .ToDictionary(x => x.FishingRodId, x => x);

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

        public HookStaticData ForHook(HookTypeId hookTypeId) =>
         _hooks.TryGetValue(hookTypeId, out HookStaticData config)
           ? config
           : null;

        public BobberStaticData ForBobber(BobberTypeId bobberTypeId) =>
         _bobber.TryGetValue(bobberTypeId, out BobberStaticData config)
          ? config
          : null;

        public FishingLineStaticData ForFishingLine(FishingLineId fishingLineTypeId) =>
         _fishingLine.TryGetValue(fishingLineTypeId, out FishingLineStaticData config)
          ? config
          : null;

        public FishingRodStaticData ForFishingRod(FishingRodId fishingRodTypeId) =>
         _fishingRod.TryGetValue(fishingRodTypeId, out FishingRodStaticData config)
          ? config
          : null;


    }
}
