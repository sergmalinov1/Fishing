﻿using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using CodeBase.StaticData.Fish;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Infrastructure.Inventory
{
    public class InventoryService : IInventoryService
    {

        private readonly IPersistentProgress _progressService;
        private readonly IAssetProvider _assetsProvider;
        private readonly IStaticDataService _staticData;
        private readonly ISaveLoadService _saveLoadService;


        public InventoryService(
            IAssetProvider assetsProvider,
            IPersistentProgress persistentService,
            IStaticDataService staticData,
            ISaveLoadService saveLoadService)
        {
            _progressService = persistentService;
            _assetsProvider = assetsProvider;
            _staticData = staticData;
            _saveLoadService = saveLoadService;
        }

        public List<EquipmentConfig> GetEquipmentsConfigByKindNew()
        {
            List<EquipmentConfig> equipmentsList = new List<EquipmentConfig>();

            KindEquipmentId _kindEquipmentId = _progressService.Progress.SettingWindow.KindOpenedWindowList;
            List<Equipment> allEquipments = _staticData.GetListByKindNew(_kindEquipmentId);
            List<EquipmentItem> purchasedEquipment = _progressService.Progress.Inventory.GetListEquipmentByKind(_kindEquipmentId);

            int typePurchaseditem = _progressService.Progress.Inventory.GetSelectedEquipmentByKind(_kindEquipmentId);

            foreach (IEquipment item in allEquipments)
            {
                EquipmentConfig config = new EquipmentConfig(item);

                if (item.GetTypeId() == typePurchaseditem)
                {
                    config.IsSelectetitem = true;
                }

                foreach (EquipmentItem purchasedItem in purchasedEquipment)
                {
                    if (item.GetTypeId() == purchasedItem.TypeId)
                    {
                        config.QtyPurchasedEquipment = purchasedItem.Count;

                    }
                }

                equipmentsList.Add(config);

            }

            return equipmentsList;
        }

        public List<EquipmentConfig> GetSelectedEquipments()
        {
            List<EquipmentConfig> _selectedEquipments = new List<EquipmentConfig>();
            foreach (CategoryEquipment item in _progressService.Progress.Inventory.InstalledEquipments)
            {
                Equipment itemEquipment = _staticData.GetEquipment(item.KindEquipmentId, (int)item.SelectedEquipmentTypeId) ;

                EquipmentConfig config = new EquipmentConfig(itemEquipment);
                EquipmentItem equipmentItem = item.FindPurchasedByTypeId(item.SelectedEquipmentTypeId);
                config.QtyPurchasedEquipment = equipmentItem.Count;
                _selectedEquipments.Add(config);
            }

            return _selectedEquipments;
        }


        public void BuyEquipment(KindEquipmentId kindEquipmentId, int typeId, int price)
        {
            if(!IsCanBuy(price))
            {
                return;
            }
            _progressService.Progress.Inventory.BuyEquipmentItem(kindEquipmentId, typeId);
            _progressService.Progress.MoneyData.Money -= price;
            SetEquipmentState();

            _saveLoadService.SaveProgress();
        }

        public void SelectEquipment(KindEquipmentId kindEquipmentId, int equipmentTypeId)
        {
            _progressService.Progress.Inventory.SelectEquipmentItem(kindEquipmentId, equipmentTypeId);
            SetEquipmentState();
            _saveLoadService.SaveProgress();
        }

        public bool IsCanBuy(int ProductPrice)
        {
            if(ProductPrice <= _progressService.Progress.MoneyData.Money)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void SetEquipmentState()
        {
            Dictionary<FishTypeId, FishStaticData> fishes =_staticData.Fishes();

            int typeLureId = _progressService.Progress.Inventory.GetSelectedEquipmentByKind(KindEquipmentId.Lure);

            Equipment equipmentLure = _staticData.GetEquipment(KindEquipmentId.Lure, typeLureId);
            LureStaticData lure = equipmentLure as LureStaticData; //ПЕРЕДЕЛАТЬ!


            int typeLaceId = _progressService.Progress.Inventory.GetSelectedEquipmentByKind(KindEquipmentId.Lake);
            Equipment equipmentLake = _staticData.GetEquipment(KindEquipmentId.Lake, typeLaceId);
            LakeStaticData lake = equipmentLake as LakeStaticData; //ПЕРЕДЕЛАТЬ!

            Dictionary<FishTypeId, FishStaticData> fishesInLake = new Dictionary<FishTypeId, FishStaticData>();

            foreach (KeyValuePair<FishTypeId, FishStaticData> fish in fishes)
            {
                foreach (FishTypeId FishInLake in lake.TypeFishAreFound)
                {
                    if (fish.Key == FishInLake)
                    {
                        fishesInLake.Add(fish.Key, fish.Value);
                    }
                }
            }


            Dictionary<FishTypeId, FishStaticData> temp = new Dictionary<FishTypeId, FishStaticData>();

            foreach (KeyValuePair<FishTypeId, FishStaticData> fish in fishesInLake)
            {
                foreach (FishTypeId FishEat in lure.TypeFishEat)
                {
                    if (fish.Key == FishEat)
                    {
                        temp.Add(fish.Key, fish.Value);
                    }
                }
            }


          /*  foreach(var item in filteredFishes)
            {
                Debug.Log(item.Value.FishName);
            }
            Debug.Log("============");*/

            _progressService.Progress.EquipmentStats.Fishes = temp.Select(kvp => (int)kvp.Key).ToList();

        }
     
    }
}

