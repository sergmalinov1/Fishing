using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.StaticData;
using TMPro;

namespace CodeBase.UI.Windows.PrepareState
{
  public class PrepareWindow : BaseWindow
  {
    public TextMeshProUGUI Title;
 
    public SettingForFishContainer SettingForFishContainer;

        public void Construct(
          PlayerProgress progressServiceProgress,
          IStaticDataService staticData,
          IAssetProvider assetsProvider)
        {

            SettingForFishContainer.Construct(progressServiceProgress, staticData, assetsProvider, this);

        }

        public void Initialize()
        {
            Title.text = "Наживка";

            SettingForFishContainer.Initialize();
        }
        protected override void SubscribeUpdate() { }

        protected override void Cleanup() { }


    }
}