using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData.Bobber
{
    [CreateAssetMenu(fileName = "BobberData", menuName = "StaticData/Bobber", order = 0)]
    public class BobberStaticData : ScriptableObject
    {

        public BobberTypeId BobberTypeId;

        public string BobberName;

        [Range(1, 6)]
        public int Rating = 1;

        [Range(1, 100)]
        public int Cost = 1;

        [Range(0, 1)]
        public float CoefficientOfLuck = 1;

        public AssetReferenceGameObject PrefabReference;
    }
}


