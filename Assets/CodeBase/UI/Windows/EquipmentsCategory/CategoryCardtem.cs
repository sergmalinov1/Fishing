using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Inventory;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.EquipmentsCategory
{
    public class CategoryCardtem : MonoBehaviour
    {
        public TextMeshProUGUI CategoryName;
        public TextMeshProUGUI CountProduct;

        
        public Button CategoryButton;

        public Image Rating;
        public Image EquipmentPicture;

        private EquipmentCategoryWindow _equipmentCategoryWindow;
        private IUIFactory _UIFactory;
        private PlayerProgress _progress;
        private KindEquipmentId _categoryTypeId;
        private IAssetProvider _assetProvider;



        public void Construct(
            EquipmentCategoryWindow equipmentCategoryWindow, 
            IUIFactory UIFactory, 
            PlayerProgress progress, 
            IAssetProvider assetProvider, 
            KindEquipmentId categoryTypeId)
        {
            _equipmentCategoryWindow = equipmentCategoryWindow;
            _UIFactory = UIFactory;
            _progress = progress;
            _categoryTypeId = categoryTypeId;
            _assetProvider = assetProvider;
        }


        public async void Initialize(EquipmentConfig equipment)
        {
            CategoryButton.onClick.AddListener(OnItemClick);

            if (equipment.QtyPurchasedEquipment > 0)
                ShowEquipmentItem(equipment);
            else
                ShowDefaultState();
        }



        private void OnItemClick()
        {
            _progress.SettingWindow.KindOpenedWindowList = _categoryTypeId;
            _equipmentCategoryWindow.CloseWindow();
            _UIFactory.CreateListEquipment();

        }

        private async void DefineRating(int rating)
        {
            Rating.sprite = await _assetProvider.Load<Sprite>($"grade_" + rating);
        }

        private async void ShowEquipmentItem(EquipmentConfig equipment)
        {
            CategoryName.text = equipment.Name;
            CountProduct.text = $"Qty: " + equipment.QtyPurchasedEquipment.ToString();

            EquipmentPicture.sprite = await _assetProvider.Load<Sprite>(equipment.ImageName);
            DefineRating(equipment.Rating);
        }

        private async void ShowDefaultState()
        {
            CategoryName.text = "???";
            CountProduct.text = $"Qty: 0";

            EquipmentPicture.sprite = await _assetProvider.Load<Sprite>(AssetsAddress.DefaultImage);
            DefineRating(0);
        }
    }
}
