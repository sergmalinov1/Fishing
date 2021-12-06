using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Inventory;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.EquipmentsList
{
    public class ListEquipmentsWindow : BaseWindow
    {
        public TextMeshProUGUI Title;

        public ListContainer ListContainer;

        public Button BackButton;


      

        private IUIFactory _UIfactory;

        public void Construct(
            IUIFactory UIfactory,  
            PlayerProgress progress, 
            IAssetProvider assetsProvider,
            IInventoryService inventoryService)
        {
            _UIfactory = UIfactory;
            ListContainer.Construct(progress, assetsProvider, inventoryService);
        }


        protected override void Initialize()
        {
            BackButton.onClick.AddListener(OnBackClick);

            Title.text = "Equipment";
            ListContainer.Initialize();
            
        }

        private void OnBackClick()
        {
            _UIfactory.CreateShopCategory();
            CloseWindow();
        }

        protected override void SubscribeUpdate() { }

        protected override void Cleanup() { }

    }
}
