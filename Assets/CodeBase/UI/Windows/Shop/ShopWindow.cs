using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.StaticData;
using TMPro;

namespace CodeBase.UI.Windows.Shop
{
  public class ShopWindow : BaseWindow
  {

    public TextMeshProUGUI Title;

    public ShopItemsContainer ShopItemsContainer;
    
    private ISaveLoadService _saveLoadService;
  
    
    public void Construct(
      PlayerProgress progressServiceProgress, 
      IStaticDataService staticData, 
      IAssetProvider assetsProvider)
    {
      

      ShopItemsContainer.Construct(progressServiceProgress, staticData, assetsProvider);
    }
    
    protected override void Initialize()
    {
      Title.text = "Магазин";
      ShopItemsContainer.Initialize();
    }
    
    protected override void SubscribeUpdate(){}

    protected override void Cleanup(){ }
  }
}