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



        /* public string FishingLineName;

         [Range(1, 6)]
         public int Rating = 1;

         [Range(1, 100)]
         public int Price = 1;*/




        /*   public AssetReferenceGameObject PrefabReference;

           private KindEquipmentId _kindEquipmentId = KindEquipmentId.FishingLine;
           public KindEquipmentId GetKindEquipment() => _kindEquipmentId;

           public string GetName() => FishingLineName;

           public int GetRating() => Rating;



           public int GetPrice() => Price;

           public string MainImage;
           public string GetImageName() => MainImage;*/
    }

}






