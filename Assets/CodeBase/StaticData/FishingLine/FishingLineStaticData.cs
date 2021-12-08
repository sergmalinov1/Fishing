using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "FishingLineData", menuName = "StaticData/FishingLine", order = 0)]
    public class FishingLineStaticData : Equipment
    {

        [Range(1, 10000)]
        public int MaxLiftWeight = 200;



        public FishingLineId FishingLineId;

        public override int GetTypeId() => (int)FishingLineId;

        public override void Accept(ISelectableEntityVisitor visitor)
        {
           // Debug.Log("FishingLineStaticData");
        }

       
    }

}






