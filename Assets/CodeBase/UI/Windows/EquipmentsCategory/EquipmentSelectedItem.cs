using CodeBase.Data;
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
        private int _typeId;


        public void Construct(EquipmentCategoryWindow equipmentCategoryWindow, IUIFactory UIFactory, PlayerProgress progress, KindEquipmentId categoryTypeId)
        {
            _equipmentCategoryWindow = equipmentCategoryWindow;
            _UIFactory = UIFactory;
            _progress = progress;
            _categoryTypeId = categoryTypeId;
        }

        public async void Initialize(string name, int typeId)
        {
            CategoryButton.onClick.AddListener(OnItemClick);


            CategoryName.text = name;
            _typeId = typeId;
        }

        private void OnItemClick()
        {
            _progress.SettingWindow.KindOpenedWindowsList = _categoryTypeId;
            _equipmentCategoryWindow.CloseWindow();
            _UIFactory.CreateListEquipment();

        }


    }
}
