using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.StaticData;
using TMPro;


namespace CodeBase.UI.Windows.EquipmentCategory
{
    public class EquipmentCategoryWindow : BaseWindow
    {

        public TextMeshProUGUI Title;

        public CategoryContainer CategoryContainer;

        public void Construct(
          PlayerProgress progressServiceProgress,
          IStaticDataService staticData,
          IAssetProvider assetsProvider)
        {


            CategoryContainer.Construct(progressServiceProgress, staticData, assetsProvider);
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


