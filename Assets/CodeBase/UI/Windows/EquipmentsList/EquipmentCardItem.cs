using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
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
        private IEquipment _equipment;
        private int _numberOfPurchasedItems;

        public void Constuct(ListContainer listContainer, IAssetProvider assetProvider)
        {
            _listContainer = listContainer;
            _assetProvider = assetProvider;
        }

        public void Initialize(IEquipment itemEquipment, int numberOfPurchasedItems)
        {
            _equipment = itemEquipment;
            _numberOfPurchasedItems = numberOfPurchasedItems;
            EquipmentName.text = _equipment.GetName();

            SelectButton.onClick.AddListener(SelectCard);

            DefineLabel();
            DefineRating(_equipment.GetRating());
        }

        private void DefineLabel()
        {
            if (_numberOfPurchasedItems > 0)
            {
                SelectLabel.SetActive(true);
                QtyAvailableItems.text = $"Select " + _numberOfPurchasedItems;

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
            if(_numberOfPurchasedItems > 0)
            {
                _listContainer.SelectItemCard(_equipment.GetTypeId());
            }
            else
            {
                _listContainer.ShowPopupToBuyItem(_equipment);
                
            }

        }

        
    }
}
