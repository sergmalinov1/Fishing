using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.PrepareState
{
  public class LureItemPrepare : MonoBehaviour
  {
    public TextMeshProUGUI ProductItem;
    public TextMeshProUGUI ProductDescription;
    public Button SelectItemButton;
    public Image Icon;
    public TextMeshProUGUI SelectItemButtonText;
    
    private PlayerProgress _progress;
    private ISaveLoadService _saveLoadService;
    private IAssetProvider _assetProvider;
    private IStaticDataService _staticData;
    private PrepareWindow _prepareWindow;
    
    private LureStaticData _lureStaticData;
    private InventoryLure _item;


    public void Construct(
      PlayerProgress progress, 
      ISaveLoadService saveLoadService, 
      IAssetProvider assetProvider, 
      IStaticDataService staticData,
      PrepareWindow prepareWindow)
    {
      _progress = progress;
      _saveLoadService = saveLoadService;
      _assetProvider = assetProvider;
      _staticData = staticData;
      _prepareWindow = prepareWindow;
    }
    
    public async void Initialize(InventoryLure item)
    {
      _item = item;
      SelectItemButton.onClick.AddListener(OnBuyItemClick);

      SelectItemButtonText.text = $"выбрать " + item.Count;
      
      ProductItem.text = item.Name;
     // _lureStaticData = _staticData.ForLure(item.LureType);
     // ProductDescription.text = _lureStaticData.ProductDescription;
    //  Icon.sprite = await _assetProvider.Load<Sprite>(_lureStaticData.Icon);

    }

    private void OnBuyItemClick()
    {
      //_progress.Inventory.SelectItemLure(_item.Name);
      _progress.FishOnHook.SelectLure(_lureStaticData.LureTypeId);
      _prepareWindow.CloseWindow();
    }
  }
}