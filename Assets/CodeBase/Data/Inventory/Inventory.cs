using System;
using System.Collections.Generic;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Data
{
  [Serializable]
    public class Inventory
    {
        public List<CategoryEquipment> InstalledEquipments = new List<CategoryEquipment>();

       
        public Inventory()
        {
         
        }

        public void AddStartPack() // вынести в LoadProgressState
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
                    return;
                }
            }

            Debug.Log("NOT FIND KindEquipmentId");
        }

        public void UseEquipmentItem(KindEquipmentId kindEquipmentId, int typeEquipmentId)
        {
            foreach (CategoryEquipment category in InstalledEquipments)
            {
                if (category.KindEquipmentId == kindEquipmentId)
                {
                    category.UseEquipment(typeEquipmentId);
                   // return;
                }
            }
        }

        public void UseSelectedEquipmentItem(KindEquipmentId kindEquipmentId)
        {
            int selectedItem = GetSelectedEquipmentByKind(kindEquipmentId);
            UseEquipmentItem(kindEquipmentId, selectedItem);
        }

        public void SelectEquipmentItem(KindEquipmentId kindEquipmentId, int typeEquipmentId)
        {
         

            foreach (CategoryEquipment category in InstalledEquipments)
            {
                if (category.KindEquipmentId == kindEquipmentId)
                {
                    category.SetSelectedItem(typeEquipmentId);
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

        public int GetSelectedEquipmentByKind(KindEquipmentId kindEquipmentId)
        {
            foreach (CategoryEquipment category in InstalledEquipments)
            {
                if (category.KindEquipmentId == kindEquipmentId)
                {
                    return category.SelectedEquipmentTypeId;
                }
            }
            return -1;
        }

        public int GetCountSelectedEquipmentByKind(KindEquipmentId kindEquipmentId)
        {
            foreach (CategoryEquipment category in InstalledEquipments)
            {
                if (category.KindEquipmentId == kindEquipmentId)
                {
                    foreach(EquipmentItem items in category.PurchasedEquipment)
                    {
                        if(items.TypeId == category.SelectedEquipmentTypeId)
                        {
                            return items.Count;
                        }
                    }
                }
            }

            return -1;
        }

        public bool IsEquipmentCompete()
        {
            if (InstalledEquipments.Count < 6)
            {
                return false;
            }

            foreach (CategoryEquipment category in InstalledEquipments)
            {

                if(category.SelectedEquipmentTypeId == -1) //Check for category availability
                {
                    return false;
                }

                EquipmentItem item = category.FindPurchasedByTypeId(category.SelectedEquipmentTypeId); //Check for EquipmentItem availability
                if (item == null)
                {
                    return false;
                }
                if (item.Count == 0)
                {
                    return false;
                }
            }

            return true;
        }

        

        public void PrintSelectedEquipment()
        {
            int Lure =  GetSelectedEquipmentByKind(KindEquipmentId.Lure);
            Debug.Log("Lure " + Lure);

            int bobberId = GetSelectedEquipmentByKind(KindEquipmentId.Bobber);
            Debug.Log("BobberId " + bobberId);

            int Lake = GetSelectedEquipmentByKind(KindEquipmentId.Lake);
            Debug.Log("Lake " + Lake);

            int Hook = GetSelectedEquipmentByKind(KindEquipmentId.Hook);
            Debug.Log("Hook " + Hook);

            int FishingLine = GetSelectedEquipmentByKind(KindEquipmentId.FishingLine);
            Debug.Log("FishingLine " + FishingLine);

            int FishingRod = GetSelectedEquipmentByKind(KindEquipmentId.FishingRod);
            Debug.Log("Hook " + FishingRod);
        }
    }
}