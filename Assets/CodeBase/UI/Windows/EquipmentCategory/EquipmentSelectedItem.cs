using CodeBase.Data;
using CodeBase.StaticData;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.EquipmentCategory
{
    public class EquipmentSelectedItem : MonoBehaviour
    {
        public TextMeshProUGUI CategoryName;
        
        public Button CategoryButton;

        public Image Rating;
        public Image EquipmentPicture;

        private PlayerProgress _progress;
        private KindEquipmentId _categoryTypeId;
        private int _typeId;


        public void Construct(Data.PlayerProgress progress, KindEquipmentId categoryTypeId)
        {
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
        }


    }
}
