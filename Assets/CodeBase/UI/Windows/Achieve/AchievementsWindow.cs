using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;

namespace CodeBase.UI.Windows.Achieve
{
  public class AchievementsWindow : BaseWindow
  {
    
    public TextMeshProUGUI Title;
    public AchieveItemContainer AchieveItemContainer;
    
    public void Construct(IPersistentProgress progressService, IAssetProvider assetProvider)
    {
      AchieveItemContainer.Construct(progressService, assetProvider);
    }

    protected override void Initialize()
    {
      Title.text = "Достижения";
      AchieveItemContainer.Initialize();
    }
    
    protected override void SubscribeUpdate(){}

    protected override void Cleanup(){ }
  }
}