using System;
using System.Collections.Generic;
using CodeBase.StaticData;

namespace CodeBase.Data
{
    [Serializable]
    public class CategoryEquipment
    {
        private const int QtyItemsInOrder = 10;
        private const int MaxQty = 999;

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
            int qty = 0;
            if (IsQuantitative == true) //эту логику необходимо вынести
                qty = QtyItemsInOrder;
            else
                qty = MaxQty;


            foreach (EquipmentItem item in PurchasedEquipment)
            {
                if (item.TypeId == typeId)
                {
                    item.Count += qty;
                    return;
                }
            }

            SetSelectedItem(typeId);


            PurchasedEquipment.Add(new EquipmentItem(typeId, qty));
        }

        public void SetSelectedItem(int equipmentTypeId)
        {
            SelectedEquipmentTypeId = equipmentTypeId;
        }

        public EquipmentItem FindPurchasedByTypeId(int typeId)
        {
            foreach (EquipmentItem item in PurchasedEquipment)
            {
                if (item.TypeId == typeId)
                {
                    return item;
                }
            }
            return null;
        }

    }
}
