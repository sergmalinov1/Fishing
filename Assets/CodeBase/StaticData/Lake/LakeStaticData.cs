using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "LakeData", menuName = "StaticData/Lake", order = 0)]
    public class LakeStaticData : Equipment
    {
        public LakeTypeId LakeTypeId;

        public FishTypeId[] TypeFishAreFound;

        public override int GetTypeId() => (int)LakeTypeId;

        public override void Accept(ISelectableEntityVisitor visitor)
        {
         //   Debug.Log("LakeStaticData");
        }
    }
}
