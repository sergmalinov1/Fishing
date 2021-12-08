using System;
using System.Collections.Generic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Data
{
  [Serializable]
    public class Inventory
    {
        public Action ChangedInventoryConfig;


        public List<CategoryEquipment> InstalledEquipments = new List<CategoryEquipment>();

        public List<InventoryItem> items = new List<InventoryItem>();


        public Inventory()
        {
         
        }

        public void AddStartPack()
        {
            InstalledEquipments.Add(new CategoryEquipment(KindEquipmentId.Lake, 0, false));
            InstalledEquipments.Add(new CategoryEquipment(KindEquipmentId.Bobber, 0, false));
            InstalledEquipments.Add(new CategoryEquipment(KindEquipmentId.FishingRod, 0, false));
            InstalledEquipments.Add(new CategoryEquipment(KindEquipmentId.Hook, 0, true));
            InstalledEquipments.Add(new CategoryEquipment(KindEquipmentId.FishingLine, 0, true));
            InstalledEquipments.Add(new CategoryEquipment(KindEquipmentId.Lure, 0, true));

        }

        public void BuyEquipmentItem(KindEquipmentId kindEquipmentId, int typeEquipmentId)
        {
            foreach (CategoryEquipment category in InstalledEquipments)
            {
                if (category.KindEquipmentId == kindEquipmentId)
                {
                    category.BuyEquipment(typeEquipmentId);
                    ChangedInventoryConfig?.Invoke();
                    return;
                }
            }

            Debug.Log("NOT FIND KindEquipmentId");
        }


        public void SelectEquipmentItem(KindEquipmentId kindEquipmentId, int typeEquipmentId)
        {
            foreach (CategoryEquipment category in InstalledEquipments)
            {
                if (category.KindEquipmentId == kindEquipmentId)
                {
                    category.SetSelectedItem(typeEquipmentId);
                    ChangedInventoryConfig?.Invoke();
                    return;
                }
            }

            Debug.Log("NOT FIND KindEquipmentId");
        }

        public List<EquipmentItem> GetListEquipmentByKind(KindEquipmentId kindEquipmentId)
        {
            foreach (CategoryEquipment category in InstalledEquipments)
            {
                if(category.KindEquipmentId == kindEquipmentId)
                {
                    return category.PurchasedEquipment;
                }
            }
            return null;
        }

        public int GetTypeEquipmentByKind(KindEquipmentId kindEquipmentId)
        {
            foreach (CategoryEquipment category in InstalledEquipments)
            {
                if (category.KindEquipmentId == kindEquipmentId)
                {
                    return category.SelectedEquipmentTypeId;
                }
            }
            return 0;
        }
    }
}