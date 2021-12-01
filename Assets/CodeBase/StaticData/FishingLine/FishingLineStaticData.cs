using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.FishingLine
{
    [CreateAssetMenu(fileName = "FishingLineData", menuName = "StaticData/FishingLine", order = 0)]
    public class FishingLineStaticData : ScriptableObject
    {
        public FishingLineId FishingLineId;

        public string FishingLineName;

        [Range(1, 6)]
        public int Rating = 1;

        [Range(1, 100)]
        public int Cost = 1;

        [Range(1, 10000)]
        public int MaxLiftWeight = 200;

        [Range(1, 10)]
        public int Distance = 1;

        public AssetReferenceGameObject PrefabReference;
    }

}






