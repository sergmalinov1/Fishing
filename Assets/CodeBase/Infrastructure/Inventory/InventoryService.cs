using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using CodeBase.StaticData.Fish;
using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.StaticData.Visitor;
using UnityEngine;
using CodeBase.Infrastructure.RandomService;

namespace CodeBase.Infrastructure.Inventory
{
    public class InventoryService : IInventoryService
    {

        private readonly IPersistentProgress _progressService;
        private readonly IAssetProvider _assetsProvider;
        private readonly IStaticDataService _staticData;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IRandomService _randomService;

        public InventoryService(
            IAssetProvider assetsProvider,
            IPersistentProgress persistentService,
            IStaticDataService staticData,
            ISaveLoadService saveLoadService,
            IRandomService randomService)
        {
            _progressService = persistentService;
            _assetsProvider = assetsProvider;
            _staticData = staticData;
            _saveLoadService = saveLoadService;
            _randomService = randomService;
        }

        public List<EquipmentConfig> GetEquipmentsConfigByKindNew()
        {
            List<EquipmentConfig> equipmentsList = new List<EquipmentConfig>();

            KindEquipmentId _kindEquipmentId = _progressService.Progress.SettingWindow.KindOpenedWindowList;
            List<Equipment> allEquipments = _staticData.GetListByKindNew(_kindEquipmentId);
            List<EquipmentItem> purchasedEquipment = _progressService.Progress.Inventory.GetListEquipmentByKind(_kindEquipmentId);

            int typePurchaseditem = _progressService.Progress.Inventory.GetSelectedEquipmentByKind(_kindEquipmentId);

            foreach (Equipment item in allEquipments)
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

                if(equipmentItem == null)
                    config.QtyPurchasedEquipment = 0;
                else
                    config.QtyPurchasedEquipment = equipmentItem.Count;

                _selectedEquipments.Add(config); 

            }

            return _selectedEquipments;
        }


        public void BuyEquipment(KindEquipmentId kindEquipmentId, int typeId, int price)
        {
           // Debug.Log("BuyEquipment");
            if(!IsCanBuy(price))
            {
                return;
            }
            _progressService.Progress.Inventory.BuyEquipmentItem(kindEquipmentId, typeId);
            _progressService.Progress.MoneyData.Subtract(price);
            SetEquipmentState();

        }

        public void SelectEquipment(KindEquipmentId kindEquipmentId, int equipmentTypeId)
        {
            //Debug.Log("SelectEquipment");
            _progressService.Progress.Inventory.SelectEquipmentItem(kindEquipmentId, equipmentTypeId);
            SetEquipmentState();
            
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

        public void SetEquipmentState() //сделать приватным
        {
            GetEquipmentStats getStats = new GetEquipmentStats(_progressService.Progress.EquipmentStats);

            foreach (CategoryEquipment category in _progressService.Progress.Inventory.InstalledEquipments)
            {
                KindEquipmentId kindId = category.KindEquipmentId;
                int selectedId = category.SelectedEquipmentTypeId;

                Equipment equipmen = _staticData.GetEquipment(kindId, selectedId);

                equipmen.Accept(getStats);
            }

            _progressService.Progress.EquipmentStats.CalculationStats();
            _randomService.GenerateNewLureStack();

            _saveLoadService.SaveProgress();

        }     
    }
}

