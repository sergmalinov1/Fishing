using CodeBase.StaticData.Fish;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.Lure
{
  [CreateAssetMenu(fileName = "LureData", menuName = "StaticData/Lure", order = 1)]
  public class LureStaticData : ScriptableObject
  {
    public LureTypeId LureTypeId;

    public string LureName;

    [Range(1, 6)]
    public int Rating = 1;

   [Range(1, 100)] 
    public int Price = 1;


    public FishTypeId[] TypeFishEat;

    
    public string ProductDescription;
  
    public string Icon;
  
  }
}