using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "HookData", menuName = "StaticData/Hook", order = 0)]
    public class HookStaticData : ScriptableObject, IEquipment
    {
        public HookTypeId HookTypeId;

        private KindEquipmentId _kindEquipmentId = KindEquipmentId.Hook;

        public string HookName;

        [Range(1, 6)]
        public int Rating = 1;

        [Range(1, 100)]
        public int Cost = 1;

        [Range(0, 1)]
        public float ChanceGettingOffHook = 1;

        public KindEquipmentId GetKindEquipment()
        {
            return _kindEquipmentId;
        }

        public string GetName() => HookName;

        public int GetRating() => Rating;

        //     public AssetReferenceGameObject PrefabReference;
    }
}





