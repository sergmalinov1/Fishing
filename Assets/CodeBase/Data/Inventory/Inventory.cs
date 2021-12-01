using System;
using System.Collections.Generic;
using CodeBase.StaticData.Lure;
using CodeBase.StaticData;

namespace CodeBase.Data
{
  [Serializable]
    public class Inventory
    {
        public List<InventoryLure> InventoryLures = new List<InventoryLure>();
        public List<InventoryItem> InventoryHook = new List<InventoryItem>();

        public void AddStartPack()
        {
            InventoryLures.Add(new InventoryLure("Хлеб", LureTypeId.Bread));
            InventoryLures[0].Count = 10;

        }

        public void AddItemLure(string itemName, LureTypeId lureType)
        {
            foreach (InventoryLure item in InventoryLures)
            {
                if (item.Name == itemName)
                {
                    item.Count++;
                    return;
                }
            }

            InventoryLures.Add(new InventoryLure(itemName, lureType));
        }

        public void SelectItemLure(string itemName)
        {
            foreach (InventoryLure item in InventoryLures)
            {
                if (item.Name == itemName)
                {
                    item.Count--;
                    if (item.Count <= 0)
                    {
                        InventoryLures.Remove(item);
                    }
                    return;
                }
            }
        }


        public void AddItemByType(ItemTypeId itemTypeId, string itemName, int typeId)
        {
            switch (itemTypeId)
            {
                case ItemTypeId.Bobber:
                    break;

                case ItemTypeId.Hook:
                    AddItem(InventoryHook, itemName, typeId);
                    break;



            }
        }


        private void AddItem(List<InventoryItem> InventoryList, string itemName, int typeId)
        {
            foreach (InventoryItem item in InventoryList)
            {
                if (item.TypeId == typeId)
                {
                   // item.Count++;
                    return;
                }
            }

            InventoryList.Add(new InventoryItem(itemName, typeId));
        }

        private void SelectItem(List<InventoryItem> InventoryList, string itemName)
        {
            foreach (InventoryLure item in InventoryLures)
            {
                /*if (item.Name == itemName)
                {
                    item.Count--;
                    if (item.Count <= 0)
                    {
                        InventoryLures.Remove(item);
                    }
                    return;
                }*/
            }
        }


    }
}