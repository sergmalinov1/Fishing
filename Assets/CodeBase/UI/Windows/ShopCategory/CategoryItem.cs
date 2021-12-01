using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.ShopCategory
{
    public class CategoryItem : MonoBehaviour
    {
        public TextMeshProUGUI CategoryName;

        private int _categoryTypeId;
        private int _typeId;

        public Button BuyItemButton;
        public Button SelectItemButton;


        public void Construct(int categoryTypeId)
        {
            _categoryTypeId = categoryTypeId;
        }
        public async void Initialize(string name, int typeId)
        {
            BuyItemButton.onClick.AddListener(OnBuyItemClick);
            SelectItemButton.onClick.AddListener(OnSelectItemClick);

            CategoryName.text = name;
            _typeId = typeId;
        }

        private void OnSelectItemClick()
        {
            
        }

        private void OnBuyItemClick()
        {
            
        }
    }
}
