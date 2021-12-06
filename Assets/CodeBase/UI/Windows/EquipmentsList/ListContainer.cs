
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Inventory;
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
        private KindEquipmentId _kindEquipmentId;
        private IInventoryService _inventoryService;

        private List<GameObject> _cardListItems = new List<GameObject>();

        private EquipmentConfig _itemToBuy;

        public void Construct(
            PlayerProgress progress, 
            IAssetProvider assetsProvider, 
            IInventoryService inventoryService)
        {
            _progress = progress;
            _assetsProvider = assetsProvider;
            _inventoryService = inventoryService;   
        } 

        public void Initialize()
        {
            _kindEquipmentId = _progress.SettingWindow.KindOpenedWindowList;

            PopupBuyButton.onClick.AddListener(BuyCardItem);
            DefineItems();
        }


        public void ShowPopupToBuyItem(EquipmentConfig equipment)
        {
            _itemToBuy = equipment;
            PopupItemName.text = equipment.Name;
            PopupPrice.text = equipment.Price.ToString();
            PopupBuyItem.SetActive(true);
        }

        public void BuyCardItem()
        {
            
            if (!_inventoryService.IsCanBuy(_itemToBuy.Price))
            {
                PopupBuyItem.SetActive(false);
                PopupNoMoney.SetActive(true);
                return;
            }
               

            _inventoryService.BuyEquipment(_kindEquipmentId, _itemToBuy.TypeId, _itemToBuy.Price);

            PopupBuyItem.SetActive(false);
            RefreshListAndSaveData();
        }

        public void SelectItemCard(int equipmentTypeId)
        {
            _inventoryService.SelectEquipment(_kindEquipmentId, equipmentTypeId);
            RefreshListAndSaveData();
        }

        private async void DefineItems()
        {
            List<EquipmentConfig> equipmentsList = _inventoryService.GetEquipmentsConfig();

            foreach(EquipmentConfig equipment in equipmentsList)
            {
                GameObject equipmentCard = null;

                if(equipment.IsSelectetitem)
                {
                    equipmentCard = await _assetsProvider.Instantiate(Constants.SelectedICardtemPath, SelectedItemTransform);
                    SelectedICardtem selectedItem = equipmentCard.GetComponent<SelectedICardtem>();

                    selectedItem.Construct(_assetsProvider);
                    selectedItem.Initialize(equipment);
                }
                else
                {
                    equipmentCard = await _assetsProvider.Instantiate(Constants.EquipmentCardPath, CardsListTransform);
                    EquipmentCardItem itemToSelect = equipmentCard.GetComponent<EquipmentCardItem>();
            

                    itemToSelect.Constuct(this, _assetsProvider);
                    itemToSelect.Initialize(equipment);
                }
                _cardListItems.Add(equipmentCard);
            }    
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
            ClearCardList();
            DefineItems();
        }
    }
}
