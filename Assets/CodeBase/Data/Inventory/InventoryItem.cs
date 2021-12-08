using CodeBase.StaticData;
using System;


namespace CodeBase.Data
{
    [Serializable]
    public class InventoryItem
    {
        public KindEquipmentId KindEquipmentId;
        public int TypeId;
        public int Count;
   

        public InventoryItem(KindEquipmentId kind, int typeId, int qty)
        {
            KindEquipmentId = kind;
            TypeId = typeId;
            Count = qty;
        }
    }
}


