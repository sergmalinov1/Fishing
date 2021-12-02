using System;


namespace CodeBase.Data
{
    [Serializable]
    public class InventoryEquipment
    {
        public string Name;
        public int Count;
        public int TypeId;

        public InventoryEquipment(string itemName, int typeId)
        {
            Name = itemName;
            TypeId = typeId;
            Count = 1;
        }
    }
}
