using System;
using System.Collections.Generic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class CategoryEquipment
    {
        private const int QtyItemsInOrder = 2;
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
            //PrintCategoty();
        }
        public void UseEquipment(int typeId)
        {
            
            EquipmentItem itemForRemove = null;

            if (IsQuantitative == false)
            {
                Debug.Log($"You can not use IsNotQuantitative categoty");
                return;
            }

            foreach (EquipmentItem item in PurchasedEquipment)
            {
                if (item.TypeId == typeId)
                {
                    item.Count--;

                    if(item.Count <= 0)
                    {
                        itemForRemove = item;
                    }

                }
            }

            if (itemForRemove != null)
                PurchasedEquipment.Remove(itemForRemove);

            //PrintCategoty();
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


        public void PrintCategoty()
        {
            Debug.Log("===================");
            Debug.Log($"KindEquipmentId " + KindEquipmentId);
            Debug.Log($"SelectedEquipmentTypeId " + SelectedEquipmentTypeId);
            Debug.Log($"IsQuantitative " + IsQuantitative);
            Debug.Log("-----------");
            foreach (EquipmentItem item in PurchasedEquipment)
            {
                Debug.Log($"itemId " + item.TypeId + ", count " + item.Count);

            }
            Debug.Log("===================");
        }


    }
}
