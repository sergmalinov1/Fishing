using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LakeData", menuName = "StaticData/Lake", order = 0)]
    public class LakeStaticData : Equipment
    {
        public LakeTypeId LakeTypeId;

        public override int GetTypeId() => (int)LakeTypeId;

        /*

        public string LakeName;

        [Range(1, 6)]
        public int Rating = 1;

        [Range(1, 100)]
        public int Price = 1;

        private KindEquipmentId _kindEquipmentId = KindEquipmentId.Lake;
        public KindEquipmentId GetKindEquipment() => _kindEquipmentId;

        public string GetName() => LakeName;

        public int GetRating() => Rating;

       

        public int GetPrice() => Price;

        public string MainImage;
        public string GetImageName() => MainImage;*/
    }
}
