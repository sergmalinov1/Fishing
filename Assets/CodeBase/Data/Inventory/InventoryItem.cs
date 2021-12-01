using System;


namespace CodeBase.Data
{
    [Serializable]
    public class InventoryItem
    {
        public string Name;
        public int Count;
        public int TypeId;

        public InventoryItem(string itemName, int typeId)
        {
            Name = itemName;
            TypeId = typeId;
            Count = 1;
        }
    }
}
