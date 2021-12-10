using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData;
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
        private const string StaticDataLakePath = "StaticData/Lake";

        private const string StaticDataWindowsPath = "StaticData/Windows/WindowStaticData";


        private Dictionary<int, Equipment> _equipments = new Dictionary<int, Equipment>();


        private Dictionary<WindowId, WindowConfig> _windows;

        private Dictionary<FishTypeId, FishStaticData> _fishes;

        public Dictionary<FishTypeId, FishStaticData> Fishes() => _fishes;


        public void Load()
        {         
            _windows = Resources
                .Load<WindowsStaticData>(StaticDataWindowsPath)
                .Configs
                .ToDictionary(x => x.WindowId, x => x);

            _fishes = Resources
                .LoadAll<FishStaticData>(StaticDataFishesPath)
                .ToDictionary(x => x.FishTypeId, x => x);

            SetEquipments<LureStaticData>(StaticDataLurePath);
            SetEquipments<HookStaticData>(StaticDataHookPath);
            SetEquipments<BobberStaticData>(StaticDataBobberPath);
            SetEquipments<FishingLineStaticData>(StaticDataFishingLinePath);
            SetEquipments<FishingRodStaticData>(StaticDataFishingRodPath);
            SetEquipments<LakeStaticData>(StaticDataLakePath);



            /*(ShowType showType = new ShowType();


            foreach (KeyValuePair<int, Equipment> item in _equipments)
            {
                item.Value.Accept(showType);
            }*/
        }
    
        public FishStaticData ForFish(FishTypeId typeId) =>
          _fishes.TryGetValue(typeId,out FishStaticData staticData) 
            ? staticData 
            : null;
    
        public WindowConfig ForWindow(WindowId windowId) =>
          _windows.TryGetValue(windowId, out WindowConfig config)
            ? config
            : null;
  
        public List<Equipment> GetListByKindNew(KindEquipmentId kindEquipmentId)
        {
            List<Equipment> equipments = new List<Equipment>();

            foreach(KeyValuePair<int, Equipment> item in _equipments)
            {
                if(item.Value.GetKindEquipment() == kindEquipmentId)
                {
                    equipments.Add(item.Value);
                }
            }
            return equipments;
        }

        public Equipment GetEquipment(KindEquipmentId kindEquipmentId, int typeId)
        {
            int key = 100 * (int)kindEquipmentId + typeId;
            return _equipments[key];
        }

        private void SetEquipments<TStaticData>(string path) where TStaticData : Equipment
        {
            Dictionary<int, Equipment> tempEquipment = new Dictionary<int, Equipment>();
            tempEquipment = Resources
              .LoadAll<TStaticData>(path)
              .ToDictionary(x => (100 * (int)x._kindEquipmentId + x.GetTypeId()), x => (Equipment)x);

            _equipments = _equipments.Union(tempEquipment).ToDictionary(x => x.Key, x => x.Value);
        }


    }
}
