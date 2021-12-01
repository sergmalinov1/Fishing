using System.Collections;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData.Lure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Shop
{
  public class ShopItem : MonoBehaviour
  {
    public TextMeshProUGUI ProductItem;
    public TextMeshProUGUI ProductDescription;
    public TextMeshProUGUI ProductPrice;
    public Button BuyItemButton;
    
    public GameObject AppearText;
 
    public Image Icon;

    private LureStaticData _lureValue;
    private PlayerProgress _progress;
    private ISaveLoadService _saveLoadService;
    private IAssetProvider _assetProvider;
    
    public void Construct(
      PlayerProgress progress, 
      LureStaticData lureValue,
      ISaveLoadService saveLoadService, 
      IAssetProvider assetProvider)
    {
      _progress = progress;
      _lureValue = lureValue;
      _saveLoadService = saveLoadService;
      _assetProvider = assetProvider;
    }

    public async void Initialize()
    {
      BuyItemButton.onClick.AddListener(OnBuyItemClick);
      
      ProductItem.text = _lureValue.LureName;
      ProductDescription.text = _lureValue.ProductDescription;
      ProductPrice.text = $"BUY {_lureValue.Price} $" ;
      Icon.sprite = await _assetProvider.Load<Sprite>(_lureValue.Icon);
        
    }

    private void OnBuyItemClick()
    {
      _progress.MoneyData.Subtract(_lureValue.Price);
      _progress.Inventory.AddItem(_lureValue.LureName, _lureValue.LureTypeId);

      _saveLoadService.SaveProgress();
      ProductPrice.text = "Куплено!";
      StartCoroutine("ShowBuyText");

    }


    IEnumerator ShowBuyText()
    {
      yield return new WaitForSeconds(2);

      ProductPrice.text = $"BUY {_lureValue.Price} $" ;
    }
    
    
  }
}