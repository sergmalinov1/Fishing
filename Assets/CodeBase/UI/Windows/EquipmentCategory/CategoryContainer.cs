using System;
using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using UnityEngine;


namespace CodeBase.UI.Windows.EquipmentCategory
{
    public class CategoryContainer : MonoBehaviour
    {
        public Transform[] ParentCard;

        private IAssetProvider _assetsProvider;
        private IStaticDataService _staticData;
        private PlayerProgress _progress;
   

        private List<IEquipment> _staticDataObject = new List<IEquipment>();

        // private readonly List<GameObject> _hooks = new List<GameObject>();

        public void Construct(PlayerProgress progressServiceProgress, IStaticDataService staticData, IAssetProvider assetsProvider)
        {
            _progress = progressServiceProgress;
            _assetsProvider = assetsProvider;
            _staticData = staticData;


        }

        public void Initialize()
        {
            IdentificationInstalledEequipment();
            RefreshAvailableItems();
        }

        private void IdentificationInstalledEequipment()
        {
            

            foreach(InstalledEquipment item in _progress.Inventory.InstalledEquipments)
            {
                switch(item.KindEquipmentId)
                {
                    case (KindEquipmentId.Bobber):
                        _staticDataObject.Add(_staticData.ForBobber((BobberTypeId)item.EquipmentTypeId));
                        break;

                    case (KindEquipmentId.FishingLine):
                        _staticDataObject.Add(_staticData.ForFishingLine((FishingLineId)item.EquipmentTypeId));
                        break;

                    case (KindEquipmentId.FishingRod):
                        _staticDataObject.Add(_staticData.ForFishingRod((FishingRodId)item.EquipmentTypeId));
                        break;

                    case (KindEquipmentId.Hook):
                        _staticDataObject.Add(_staticData.ForHook((HookTypeId)item.EquipmentTypeId));
                        break;
                }
            }   
        }

        private async void RefreshAvailableItems()
        {
            for(int i=0; i < _staticDataObject.Count; i++)
            {

                GameObject productObject = await _assetsProvider.Instantiate(Constants.SelectedEquipmentCardPath, ParentCard[i]);
                EquipmentSelectedItem selectedItem = productObject.GetComponent<EquipmentSelectedItem>();


                IEquipment equipment = _staticDataObject[i];
                selectedItem.Initialize(equipment.GetName(), equipment.GetRating());

            }
        }   
    }
}
