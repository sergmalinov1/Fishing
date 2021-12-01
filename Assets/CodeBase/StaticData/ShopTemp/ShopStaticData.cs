using CodeBase.StaticData.Lure;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.ShopTemp
{
  [CreateAssetMenu(fileName = "ShopProducts", menuName = "StaticData/Shop", order = 2)]
  public class ShopStaticData : ScriptableObject
  {
    
    public ShopId ShopId;
    
    public LureTypeId LureTypeId;
    
    public string ProductName;
    public string ProductDescription;
    public int ProductPrice;
    
    public AssetReferenceGameObject PrefabReference;
  }
}