using UnityEngine;
using UnityEngine.AddressableAssets;


namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "FishingRodData", menuName = "StaticData/FishingRod", order = 0)]
    public class FishingRodStaticData : Equipment
    {
        public FishingRodId FishingRodId;


        [Range(1, 10000)]
        public int MinFishWeight = 100;

        [Range(1, 10000)]
        public int MaxFishWeight = 200;

        [Range(1, 10)]
        public int Distance = 1;
        public override int GetTypeId() => (int)FishingRodId;

        public override void Accept(ISelectableEntityVisitor visitor)
        {
            visitor.Visit(this);
        }

    }
}












