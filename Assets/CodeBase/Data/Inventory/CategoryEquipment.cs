using System;
using System.Collections.Generic;
using CodeBase.StaticData;

namespace CodeBase.Data
{
    [Serializable]
    public class CategoryEquipment
    {
        
        public KindEquipmentId KindEquipmentId;
        public int SelectedEquipmentTypeId;

        public List<EquipmentItem> PurchasedEquipment = new List<EquipmentItem>();

        public CategoryEquipment(KindEquipmentId kindEquipmentId, int equipmentTypeId)
        {
            KindEquipmentId = kindEquipmentId;

            BuyEquipment(equipmentTypeId);

        }


        public void BuyEquipment(int typeId)
        {
            foreach (EquipmentItem item in PurchasedEquipment)
            {
                if (item.TypeId == typeId)
                {
                    return;
                }
            }

            SetSelectedItem(typeId);
            PurchasedEquipment.Add(new EquipmentItem(typeId));
        }

        public void SetSelectedItem(int equipmentTypeId)
        {
            SelectedEquipmentTypeId = equipmentTypeId;
        }

        /* public LakeTypeId LakeId = LakeTypeId.Simple;
         public BobberTypeId BobberId = BobberTypeId.Simple;
         public FishingRodId FishingRodId = FishingRodId.Simple;      
         public HookTypeId HookId = HookTypeId.Simple;
         public FishingLineId FishingId = FishingLineId.Simple;*/


    }

}
