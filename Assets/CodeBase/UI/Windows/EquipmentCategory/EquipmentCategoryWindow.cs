using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using System;
using TMPro;


namespace CodeBase.UI.Windows.EquipmentCategory
{
    public class EquipmentCategoryWindow : BaseWindow
    {

        public TextMeshProUGUI Title;

        public CategoryContainer CategoryContainer;


        public void Construct(UIFactory uIFactory, PlayerProgress progress, IAssetProvider assetsProvider, IStaticDataService staticData)
        {
            CategoryContainer.Construct(uIFactory, progress, assetsProvider, staticData);
        }


        protected override void Initialize()
        {
            Title.text = "Equipment";
            CategoryContainer.Initialize();
        }

        protected override void SubscribeUpdate() { }

        protected override void Cleanup() { }

        
    }
}


