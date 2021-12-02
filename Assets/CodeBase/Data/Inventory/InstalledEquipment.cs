using System;
using CodeBase.StaticData;

namespace CodeBase.Data
{
    [Serializable]
    public class InstalledEquipment
    {
        
        public KindEquipmentId KindEquipmentId;
        public int EquipmentTypeId;

        public InstalledEquipment(KindEquipmentId kindEquipmentId, int equipmentTypeId)
        {
            KindEquipmentId = kindEquipmentId;
            EquipmentTypeId = equipmentTypeId;

        }


        /* public LakeTypeId LakeId = LakeTypeId.Simple;
         public BobberTypeId BobberId = BobberTypeId.Simple;
         public FishingRodId FishingRodId = FishingRodId.Simple;      
         public HookTypeId HookId = HookTypeId.Simple;
         public FishingLineId FishingId = FishingLineId.Simple;*/


    }

}
