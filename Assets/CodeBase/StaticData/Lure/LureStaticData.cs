using CodeBase.StaticData.Fish;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
  [CreateAssetMenu(fileName = "LureData", menuName = "StaticData/Lure", order = 1)]
    public class LureStaticData : Equipment
    {
        public LureTypeId LureTypeId;

        public override int GetTypeId() => (int)LureTypeId;

        public FishTypeId[] TypeFishEat;
        public override void Accept(ISelectableEntityVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}