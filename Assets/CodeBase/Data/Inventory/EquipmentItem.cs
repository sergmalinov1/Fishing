using System;


namespace CodeBase.Data
{
    [Serializable]
    public class EquipmentItem
    {
        public int Count;
        public int TypeId;
        
        public EquipmentItem(int typeId, int qtyItemsInOrder)
        {
            TypeId = typeId;
            Count = qtyItemsInOrder;
        }
    }
}
