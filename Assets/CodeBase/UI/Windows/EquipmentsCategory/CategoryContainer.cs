using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Inventory;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using UnityEngine;


namespace CodeBase.UI.Windows.EquipmentsCategory
{
    public class CategoryContainer : MonoBehaviour
    {
        public Transform[] ParentCard;

        private IAssetProvider _assetsProvider;
        private IStaticDataService _staticData;
        private PlayerProgress _progress;
        private IUIFactory _UIFactory;
        private EquipmentCategoryWindow _equipmentCategoryWindow;
        private IInventoryService _inventoryService;

        private List<EquipmentConfig> _selectedEquipments = null;


        public void Construct(
            EquipmentCategoryWindow equipmentCategory, 
            IUIFactory UIFactory, 
            PlayerProgress progress,
            IAssetProvider assetsProvider, 
            IStaticDataService staticData,
            IInventoryService inventoryService)
        {
            _equipmentCategoryWindow = equipmentCategory;
            _UIFactory = UIFactory;
            _progress = progress;
            _assetsProvider = assetsProvider;
            _staticData = staticData;
            _inventoryService = inventoryService;
        }

        public void Initialize()
        {
            _selectedEquipments = _inventoryService.GetSelectedEquipments();
            RefreshAvailableItems();
        }

    /*    private void IdentificationInstalledEequipment()
        {
            
            foreach(CategoryEquipment item in _progress.Inventory.InstalledEquipments)
            {
                switch(item.KindEquipmentId)
                {
                    case (KindEquipmentId.Bobber):
                        _staticDataObject.Add(_staticData.ForBobber((BobberTypeId)item.SelectedEquipmentTypeId));
                        break;

                    case (KindEquipmentId.FishingLine):
                        _staticDataObject.Add(_staticData.ForFishingLine((FishingLineId)item.SelectedEquipmentTypeId));
                        break;

                    case (KindEquipmentId.FishingRod):
                        _staticDataObject.Add(_staticData.ForFishingRod((FishingRodId)item.SelectedEquipmentTypeId));
                        break;

                    case (KindEquipmentId.Hook):
                        _staticDataObject.Add(_staticData.ForHook((HookTypeId)item.SelectedEquipmentTypeId));
                        break;

                    case (KindEquipmentId.Lake):
                        _staticDataObject.Add(_staticData.ForLake((LakeTypeId)item.SelectedEquipmentTypeId));
                        break;

                    case (KindEquipmentId.Lure):
                        _staticDataObject.Add(_staticData.ForLure((LureTypeId)item.SelectedEquipmentTypeId));
                        break;
                }
            }   
        }*/

        private async void RefreshAvailableItems()
        {
            for(int i=0; i < _selectedEquipments.Count; i++)
            {

                GameObject productObject = await _assetsProvider.Instantiate(Constants.CategoryCardtemPath, ParentCard[i]);
                CategoryCardtem selectedItem = productObject.GetComponent<CategoryCardtem>();


                EquipmentConfig equipment = _selectedEquipments[i];
                selectedItem.Construct(_equipmentCategoryWindow, _UIFactory, _progress, _assetsProvider, equipment.KindEquipmentId);
                selectedItem.Initialize(equipment);

            }
        }   
    }
}
