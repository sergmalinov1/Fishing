
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows.EquipmentsCategory;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.EquipmentsList
{
    public class ListContainer : MonoBehaviour
    {
        public Transform SelectedItemTransform;
        public Transform CardsListTransform;

        public GameObject PopupBuyItem;
        public GameObject PopupNoMoney;


        //Popup elements
        public Button PopupBuyButton;
        public Image PopupRating;
        public Image PopupItemImage;
        public TextMeshProUGUI PopupPrice;
        public TextMeshProUGUI PopupItemName;


        private PlayerProgress _progress;
        private IAssetProvider _assetsProvider;
        private IStaticDataService _staticData;
        private KindEquipmentId _kindEquipmentId;
        private ISaveLoadService _saveLoadService;

        private List<GameObject> _cardListItems = new List<GameObject>();

        private IEquipment _itemToBuy;

        public void Construct(
            PlayerProgress progress, 
            IAssetProvider assetsProvider, 
            IStaticDataService staticData)
        {
            _progress = progress;
            _assetsProvider = assetsProvider;
            _staticData = staticData;
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>(); //логику нужно вынести в отдельный сервис
        } 

        public void Initialize()
        {
            _kindEquipmentId = _progress.SettingWindow.KindOpenedWindowList;

            PopupBuyButton.onClick.AddListener(BuyCardItem);
            DefineItems();
        }

        public void ShowPopupToBuyItem(IEquipment equipment)
        {
            _itemToBuy = equipment;

            PopupItemName.text = _itemToBuy.GetName();
            PopupPrice.text = "66";

            PopupBuyItem.SetActive(true);
        }


        public void BuyCardItem()
        {
            //  if(IsNotEnoughMoney())
            //     return;

            _progress.Inventory.ButEquipmentItem(_kindEquipmentId, _itemToBuy.GetTypeId());
            PopupBuyItem.SetActive(false);
            RefreshListAndSaveData();
        }

        public void SelectItemCard(int equipmentTypeId)
        {
            _progress.Inventory.SelectEquipmentItem(_kindEquipmentId, equipmentTypeId);
            RefreshListAndSaveData();
        }

       

        private async void DefineItems()
        {
            List<IEquipment> allEquipments = _staticData.GetListByKind(_kindEquipmentId);
            List<EquipmentItem> purchasedEquipment = _progress.Inventory.GetListEquipmentByKind(_kindEquipmentId);

            int typePurchaseditem = _progress.Inventory.GetTypeEquipmentByKind(_kindEquipmentId);

            foreach (IEquipment item in allEquipments)
            {
                if(item.GetTypeId() == typePurchaseditem)
                {
                    GameObject selectedObject = await _assetsProvider.Instantiate(Constants.SelectedICardtemPath, SelectedItemTransform);
                    SelectedICardtem selectedItem = selectedObject.GetComponent<SelectedICardtem>();
                    _cardListItems.Add(selectedObject);


                    //_equipmentCategoryWindow
                    selectedItem.Construct(_progress, _assetsProvider);
                    selectedItem.Initialize(item.GetName(), item.GetRating());
                    continue; 
                }

                GameObject cardObject = await _assetsProvider.Instantiate(Constants.EquipmentCardPath, CardsListTransform);
                EquipmentCardItem itemToSelect = cardObject.GetComponent<EquipmentCardItem>();
                _cardListItems.Add(cardObject);

                itemToSelect.Constuct(this, _assetsProvider);


                int numberOfPurchasedItems = 0;
                foreach (EquipmentItem purchasedItem in purchasedEquipment)
                {
                    if(item.GetTypeId() == purchasedItem.TypeId)
                    {
                        numberOfPurchasedItems = purchasedItem.Count;

                    }
                }

                itemToSelect.Initialize(item, numberOfPurchasedItems);
            }
        }       

        private bool IsNotEnoughMoney()
        {
            PopupNoMoney.SetActive(true);
            return true;
        }

        private void ClearCardList()
        {
            while(_cardListItems.Count > 0)
            {
                Destroy(_cardListItems[0]);
                _cardListItems.RemoveAt(0);
            }
        }

        private void RefreshListAndSaveData()
        {
            _saveLoadService.SaveProgress();
            ClearCardList();
            DefineItems();
        }
             
    }
}
