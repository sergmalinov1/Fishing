using System;
using System.Collections.Generic;
using CodeBase.StaticData;

namespace CodeBase.Data
{
    [Serializable]
    public class CategoryEquipment
    {
        private const int QtyItemsInOrder = 10;

        public KindEquipmentId KindEquipmentId;
        public int SelectedEquipmentTypeId;
        public bool IsQuantitative = false;

        public List<EquipmentItem> PurchasedEquipment = new List<EquipmentItem>();

        public CategoryEquipment(KindEquipmentId kindEquipmentId, int equipmentTypeId, bool isQuantitative)
        {
            KindEquipmentId = kindEquipmentId;
            IsQuantitative = isQuantitative;
            BuyEquipment(equipmentTypeId);

        }


        public void BuyEquipment(int typeId)
        {
            foreach (EquipmentItem item in PurchasedEquipment)
            {
                if (item.TypeId == typeId)
                {
                    item.Count += QtyItemsInOrder;
                    return;
                }
            }

            SetSelectedItem(typeId);
            PurchasedEquipment.Add(new EquipmentItem(typeId, QtyItemsInOrder));
        }

        public void SetSelectedItem(int equipmentTypeId)
        {
            SelectedEquipmentTypeId = equipmentTypeId;
        }



    }

}
