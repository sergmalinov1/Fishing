using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Inventory;
using CodeBase.StaticData;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.EquipmentsList
{
    public class EquipmentCardItem : MonoBehaviour
    {
      //  public Button BuyButton;
        public Button SelectButton;

        public GameObject BuyLebel;
        public GameObject SelectLabel;

        public TextMeshProUGUI EquipmentName;

        public Image Rating;
        public Image EquipmentPicture;

        public TextMeshProUGUI Price;
        public TextMeshProUGUI QtyAvailableItems;


        private IAssetProvider _assetProvider;
        private ListContainer _listContainer;

        private EquipmentConfig _equipment;
        private int _QtyOfPurchasedItems;

        public void Constuct(ListContainer listContainer, IAssetProvider assetProvider)
        {
            _listContainer = listContainer;
            _assetProvider = assetProvider;
        }


        public void Initialize(EquipmentConfig equipment)
        {
            SelectButton.onClick.AddListener(SelectCard);

            _equipment = equipment;
            _QtyOfPurchasedItems = equipment.QtyPurchasedEquipment;
            EquipmentName.text = equipment.Name;

            DefineLabel();
            DefineRating(equipment.Rating);
        }

        private void DefineLabel()
        {
            if (_QtyOfPurchasedItems > 0)
            {
                SelectLabel.SetActive(true);
                QtyAvailableItems.text = $"Select " + _QtyOfPurchasedItems;

            }
            else
            {
                BuyLebel.SetActive(true);

            }
        }

        private async void DefineRating(int rating)
        {
            Rating.sprite = await _assetProvider.Load<Sprite>($"grade_" + rating);
        }

        private void SelectCard()
        {
            if(_QtyOfPurchasedItems > 0)
            {
                _listContainer.SelectItemCard(_equipment.TypeId);
            }
            else
            {
                _listContainer.ShowPopupToBuyItem(_equipment);
                
            }

        }

       
    }
}
