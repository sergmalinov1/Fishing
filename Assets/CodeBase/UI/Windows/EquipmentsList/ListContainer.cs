
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows.EquipmentsCategory;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI.Windows.EquipmentsList
{
    public class ListContainer : MonoBehaviour
    {
        public Transform SelectedItemTransform;
        public Transform CardsListTransform;


        private PlayerProgress _progress;
        private IAssetProvider _assetsProvider;
        private IStaticDataService _staticData;
        private IUIFactory _UIFactory;
        private BaseWindow _baseWindow;

        private List<IEquipment> _staticDataObject = new List<IEquipment>();

        public void Construct(
            ListEquipmentsWindow listEquipmentsWindow,
            PlayerProgress progress, 
            IAssetProvider assetsProvider, 
            IStaticDataService staticData, 
            IUIFactory uIfactory)
        {
            _baseWindow = listEquipmentsWindow as BaseWindow;

            _progress = progress;
            _assetsProvider = assetsProvider;
            _staticData = staticData;
            _UIFactory = uIfactory;
        }

        public void Initialize()
        {
            DefineItems();
        }

        private async void DefineItems()
        {
            KindEquipmentId kindEquipmentId = _progress.SettingWindow.KindOpenedWindowList;

            List<IEquipment> allEquipments = _staticData.GetListByKind(kindEquipmentId);
            List<EquipmentItem> purchasedEquipment = _progress.Inventory.GetListEquipmentByKind(kindEquipmentId);

            int typePurchaseditem = _progress.Inventory.GetTypeEquipmentByKind(kindEquipmentId);



            foreach (IEquipment item in allEquipments)
            {
                if(item.GetTypeId() == typePurchaseditem)
                {
                    GameObject productObject = await _assetsProvider.Instantiate(Constants.SelectedICardtemPath, SelectedItemTransform);
                    SelectedICardtem selectedItem = productObject.GetComponent<SelectedICardtem>();

                    //_equipmentCategoryWindow
                    selectedItem.Construct(_progress, _assetsProvider);
                    selectedItem.Initialize(item.GetName(), item.GetRating());
                    continue; 
                }

                GameObject otherObject = await _assetsProvider.Instantiate(Constants.EquipmentCardPath, CardsListTransform);
                EquipmentCardItem itemToSelect = otherObject.GetComponent<EquipmentCardItem>();
                itemToSelect.Constuct(_assetsProvider);
                itemToSelect.Initialize(item.GetName(), item.GetRating());



            }


        }

       
    }
}
