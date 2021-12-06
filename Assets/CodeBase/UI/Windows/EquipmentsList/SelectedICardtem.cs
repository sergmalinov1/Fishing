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


        public async void Initialize(EquipmentConfig equipment)
        {
            _equipment = equipment;
            CategoryButton.onClick.AddListener(OnItemClick);
            CategoryName.text = equipment.Name;
            SetImage();
            DefineRating(equipment.Rating);
        }

        private void OnItemClick()
        {

        }

        private async void SetImage()
        {
            EquipmentPicture.sprite = await _assetProvider.Load<Sprite>(_equipment.ImageName);
        }

        private async void DefineRating(int rating)
        {
            Rating.sprite = await _assetProvider.Load<Sprite>($"grade_" + rating);
        }

     
    }
}
