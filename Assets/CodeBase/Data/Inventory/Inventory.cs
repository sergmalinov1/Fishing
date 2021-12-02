using System;
using System.Collections.Generic;
using CodeBase.StaticData;

namespace CodeBase.Data
{
  [Serializable]
    public class Inventory
    {

        public List<InstalledEquipment> InstalledEquipments = new List<InstalledEquipment>();     

        public List<InventoryLure> InventoryLures = new List<InventoryLure>();
        public List<InventoryEquipment> InventoryHook = new List<InventoryEquipment>();

        public Inventory()
        {
            InstalledEquipments.Add(new InstalledEquipment(KindEquipmentId.Lake, 0));
            InstalledEquipments.Add(new InstalledEquipment(KindEquipmentId.Bobber, 0));
            InstalledEquipments.Add(new InstalledEquipment(KindEquipmentId.FishingRod, 0));
            InstalledEquipments.Add(new InstalledEquipment(KindEquipmentId.Hook, 0));
            InstalledEquipments.Add(new InstalledEquipment(KindEquipmentId.FishingLine, 0));
        }

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


        public void AddItemByType(KindEquipmentId itemTypeId, string itemName, int typeId)
        {
            switch (itemTypeId)
            {
                case KindEquipmentId.Bobber:
                    break;

                case KindEquipmentId.Hook:
                    AddItem(InventoryHook, itemName, typeId);
                    break;



            }
        }


        private void AddItem(List<InventoryEquipment> InventoryList, string itemName, int typeId)
        {
            foreach (InventoryEquipment item in InventoryList)
            {
                if (item.TypeId == typeId)
                {
                   // item.Count++;
                    return;
                }
            }

            InventoryList.Add(new InventoryEquipment(itemName, typeId));
        }

        private void SelectItem(List<InventoryEquipment> InventoryList, string itemName)
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