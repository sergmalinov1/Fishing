using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.EquipmentsCategory
{
    public class EquipmentSelectedItem : MonoBehaviour
    {
        public TextMeshProUGUI CategoryName;
        
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

        public async void Initialize(string name, int rating)
        {
            CategoryButton.onClick.AddListener(OnItemClick);


            CategoryName.text = name;
            DefineRating(rating);
        }

        private void OnItemClick()
        {
            _progress.SettingWindow.KindOpenedWindowsList = _categoryTypeId;
            _equipmentCategoryWindow.CloseWindow();
            _UIFactory.CreateListEquipment();

        }

        private async void DefineRating(int rating)
        {
            Debug.Log($"grade_" + rating);
            Rating.sprite = await _assetProvider.Load<Sprite>($"grade_" + rating);
        }


    }
}
