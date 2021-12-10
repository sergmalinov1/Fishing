using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "BobberData", menuName = "StaticData/Bobber", order = 0)]
    public class BobberStaticData : Equipment
    {

        public BobberTypeId BobberTypeId;

        [Range(0, 1)]
        public float CoefficientOfLuck = 1;

        public override void Accept(ISelectableEntityVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override int GetTypeId() => (int)BobberTypeId;

    }
}


