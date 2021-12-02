using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "BobberData", menuName = "StaticData/Bobber", order = 0)]
    public class BobberStaticData : ScriptableObject, IEquipment
    {

        public BobberTypeId BobberTypeId;

        public string BobberName;

        [Range(1, 6)]
        public int Rating = 1;

        [Range(1, 100)]
        public int Cost = 1;

        public AssetReferenceGameObject PrefabReference;

        [Range(0, 1)]
        public float CoefficientOfLuck = 1;

        private KindEquipmentId _kindEquipmentId = KindEquipmentId.Bobber;
        public KindEquipmentId GetKindEquipment() => _kindEquipmentId;

        public string GetName() => BobberName;

        public int GetRating() => Rating;
    }
}


