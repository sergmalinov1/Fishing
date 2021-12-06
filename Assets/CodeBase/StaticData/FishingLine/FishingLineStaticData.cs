using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "FishingLineData", menuName = "StaticData/FishingLine", order = 0)]
    public class FishingLineStaticData : ScriptableObject, IEquipment
    {
        public FishingLineId FishingLineId;

        public string FishingLineName;

        [Range(1, 6)]
        public int Rating = 1;

        [Range(1, 100)]
        public int Price = 1;

        [Range(1, 10000)]
        public int MaxLiftWeight = 200;


        public AssetReferenceGameObject PrefabReference;

        private KindEquipmentId _kindEquipmentId = KindEquipmentId.FishingLine;
        public KindEquipmentId GetKindEquipment() => _kindEquipmentId;

        public string GetName() => FishingLineName;

        public int GetRating() => Rating;

        public int GetTypeId() => (int)FishingLineId;

        public int GetPrice() => Price;

        public string MainImage;
        public string GetImageName() => MainImage;
    }

}






