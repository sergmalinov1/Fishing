using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "HookData", menuName = "StaticData/Hook", order = 0)]
    public class HookStaticData : Equipment
    {
        public HookTypeId HookTypeId;

        [Range(0, 1)]
        public float ChanceGettingOffHook = 1;

        public override int GetTypeId() => (int)HookTypeId;

        public override void Accept(ISelectableEntityVisitor visitor)
        {
           // Debug.Log("HookStaticData");
        }

    }
}





