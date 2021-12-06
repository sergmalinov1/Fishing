using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Infrastructure.Inventory
{
    public class InventoryService : IInventoryService
    {

        private readonly IPersistentProgress _persistentService;
        private readonly IAssetProvider _assetsProvider;
        private readonly IStaticDataService _staticData;
        private readonly ISaveLoadService _saveLoadService;


        public InventoryService(
            IAssetProvider assetsProvider,
            IPersistentProgress persistentService,
            IStaticDataService staticData,
            ISaveLoadService saveLoadService)
        {
            _persistentService = persistentService;
            _assetsProvider = assetsProvider;
            _staticData = staticData;
            _saveLoadService = saveLoadService;
        }



        public List<EquipmentConfig> GetEquipmentsConfig()
        {
            List<EquipmentConfig> equipmentsList = new List<EquipmentConfig>();


            KindEquipmentId _kindEquipmentId = _persistentService.Progress.SettingWindow.KindOpenedWindowList;
            List<IEquipment> allEquipments = _staticData.GetListByKind(_kindEquipmentId);
            List<EquipmentItem> purchasedEquipment = _persistentService.Progress.Inventory.GetListEquipmentByKind(_kindEquipmentId);

            int typePurchaseditem = _persistentService.Progress.Inventory.GetTypeEquipmentByKind(_kindEquipmentId);

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


        public void BuyEquipment(KindEquipmentId kindEquipmentId, int typeId, int price)
        {
            if(!IsCanBuy(price))
            {
                return;
            }
            _persistentService.Progress.Inventory.BuyEquipmentItem(kindEquipmentId, typeId);
            _persistentService.Progress.MoneyData.Money -= price;

            _saveLoadService.SaveProgress();
        }

        public void SelectEquipment(KindEquipmentId kindEquipmentId, int equipmentTypeId)
        {
            _persistentService.Progress.Inventory.SelectEquipmentItem(kindEquipmentId, equipmentTypeId);
            _saveLoadService.SaveProgress();
        }

        public bool IsCanBuy(int ProductPrice)
        {
            if(ProductPrice <= _persistentService.Progress.MoneyData.Money)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}

