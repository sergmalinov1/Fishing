using UnityEngine;
using UnityEngine.AddressableAssets;


namespace CodeBase.StaticData.FishingRod
{
    [CreateAssetMenu(fileName = "FishingRodData", menuName = "StaticData/FishingRod", order = 0)]
    public class FishingRodStaticData : ScriptableObject
    {
        public FishingRodId FishingRodId;

        public string FishingRodName;

        [Range(1, 6)]
        public int Rating = 1;

        [Range(1, 100)]
        public int Cost = 1;

        [Range(1, 10000)]
        public int MinFishWeight = 100;

        [Range(1, 10000)]
        public int MaxFishWeight = 200;

        [Range(1, 10)]
        public int Distance = 1;

        public AssetReferenceGameObject PrefabReference;
    }
}












