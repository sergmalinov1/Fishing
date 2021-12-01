using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.Fish
{
  
  [CreateAssetMenu(fileName = "FishData", menuName = "StaticData/Fish", order = 0)]
  public class FishStaticData : ScriptableObject
  {
    public FishTypeId FishTypeId;

    public string FishName;

    [Range(1, 6)]
    public int Rating = 1;

    [Range(1, 100)] 
    public int PrizeMoney = 1;

    [Range(1, 100)] 
    public int ChanceOfBite = 1;

    [Range(1, 100)] 
    public int ChanceToCatch = 1;

    public AssetReferenceGameObject PrefabReference;
  }
  
}