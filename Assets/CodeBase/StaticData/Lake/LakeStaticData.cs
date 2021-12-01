using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Lake
{
    [CreateAssetMenu(fileName = "LakeData", menuName = "StaticData/Lake", order = 0)]
    public class LakeStaticData : ScriptableObject
    {
        public LakeTypeId LakeTypeId;

        [Range(1, 6)]
        public int Rating = 1;

        [Range(1, 100)]
        public int Cost = 1;
    }
}
