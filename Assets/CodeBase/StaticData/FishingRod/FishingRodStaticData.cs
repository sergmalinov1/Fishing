using UnityEngine;
using UnityEngine.AddressableAssets;


namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "FishingRodData", menuName = "StaticData/FishingRod", order = 0)]
    public class FishingRodStaticData : ScriptableObject, IEquipment
    {
        public FishingRodId FishingRodId;

        public string FishingRodName;

       

        [Range(1, 6)]
        public int Rating = 1;

        [Range(1, 100)]
        public int Price = 1;

        [Range(1, 10000)]
        public int MinFishWeight = 100;

        [Range(1, 10000)]
        public int MaxFishWeight = 200;

        [Range(1, 10)]
        public int Distance = 1;

        public AssetReferenceGameObject PrefabReference;

        private KindEquipmentId _kindEquipmentId = KindEquipmentId.FishingRod;
        public KindEquipmentId GetKindEquipment() => _kindEquipmentId;

        public string GetName() => FishingRodName;

        public int GetPrice() => Price;


        public int GetRating() => Rating;

        public int GetTypeId() => (int)FishingRodId;

        public string MainImage;
        public string GetImageName() => MainImage;

    }
}












