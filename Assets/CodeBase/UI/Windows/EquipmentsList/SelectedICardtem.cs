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
    public class SelectedICardtem : MonoBehaviour
    {
        public TextMeshProUGUI CategoryName;

        public Button CategoryButton;

        public Image Rating;
        public Image EquipmentPicture;

        private IAssetProvider _assetProvider;
        private EquipmentConfig _equipment;


        public void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }


        public async void Initialize(EquipmentConfig equipment, bool IsSelectedItem)
        {
            _equipment = equipment;
            CategoryButton.onClick.AddListener(OnItemClick);

            if(IsSelectedItem)
            {
                SetSelectedItem();
            }
            else
            {
                SetEmptyItem();
            }
          
        }

        private void OnItemClick()
        {

        }

        private void SetSelectedItem()
        {
            CategoryName.text = _equipment.Name;
            SetImage(_equipment.ImageName);
            DefineRating(_equipment.Rating);
        }

        private void SetEmptyItem()
        {
            CategoryName.text = "???";
            SetImage(AssetsAddress.DefaultImage);
            DefineRating(0);
        }

        private async void SetImage(string imageName)
        {
            EquipmentPicture.sprite = await _assetProvider.Load<Sprite>(imageName);
        }

        private async void DefineRating(int rating)
        {
            Rating.sprite = await _assetProvider.Load<Sprite>($"grade_" + rating);
        }

     
    }
}
