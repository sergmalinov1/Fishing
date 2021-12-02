using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.StaticData;
using TMPro;


namespace CodeBase.UI.Windows.ShopCategory
{
    public class ShopCategoryWindow : BaseWindow
    {

        public TextMeshProUGUI Title;

        public CategoryContainer CategoryContainer;

        public void Construct(
          PlayerProgress progressServiceProgress,
          IStaticDataService staticData,
          IAssetProvider assetsProvider)
        {


            //   CategoryContainer.Construct(progressServiceProgress, staticData, assetsProvider);
        }

        protected override void Initialize()
        {
            Title.text = "Shop";
           // CategoryContainer.Initialize();
        }

        protected override void SubscribeUpdate() { }

        protected override void Cleanup() { }
    }
}


