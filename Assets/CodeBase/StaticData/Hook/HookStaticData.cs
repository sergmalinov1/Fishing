using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Assets.CodeBase.StaticData.Hook
{
    [CreateAssetMenu(fileName = "HookData", menuName = "StaticData/Hook", order = 0)]
    public class HookStaticData : ScriptableObject
    {
        public HookTypeId HookTypeId;

        public string HookName;

        [Range(1, 6)]
        public int Rating = 1;

        [Range(1, 100)]
        public int Cost = 1;

        [Range(0, 1)]
        public float ChanceGettingOffHook = 1;

        public AssetReferenceGameObject PrefabReference;
    }
}





