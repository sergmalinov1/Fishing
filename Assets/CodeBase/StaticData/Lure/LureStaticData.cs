using CodeBase.StaticData.Fish;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LureData", menuName = "StaticData/Lure", order = 1)]
    public class LureStaticData : ScriptableObject, IEquipment
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

        private KindEquipmentId _kindEquipmentId = KindEquipmentId.Lure;
        public KindEquipmentId GetKindEquipment() => _kindEquipmentId;

        public string GetName() => LureName;

        public int GetRating() => Rating;

        public int GetTypeId() => (int)LureTypeId;

        public int GetPrice() => Price;
    }
}