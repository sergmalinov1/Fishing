using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Inventory;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using System;
using TMPro;


namespace CodeBase.UI.Windows.EquipmentsCategory
{
    public class EquipmentCategoryWindow : BaseWindow
    {

        public TextMeshProUGUI Title;

        public CategoryContainer CategoryContainer;



        public void Construct(IUIFactory uIFactory, PlayerProgress progress, IAssetProvider assetsProvider, IStaticDataService staticData, IInventoryService _inventoryService)
        {
            CategoryContainer.Construct(this, uIFactory, progress, assetsProvider, staticData, _inventoryService);
            
        }


        protected override void Initialize()
        {
            Title.text = "Equipment";
            CategoryContainer.Initialize();
        }

        protected override void SubscribeUpdate() { }

        protected override void Cleanup()  { }

        
    }
}


