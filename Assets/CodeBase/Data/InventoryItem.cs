using System;
using CodeBase.StaticData.Lure;

namespace CodeBase.Data
{
  [Serializable]
  public class InventoryItem
  {
    public string Name;
    public int Count;
    public LureTypeId LureType;
    
    public InventoryItem(string itemName, LureTypeId lureType)
    {
      Name = itemName;
      LureType = lureType;
      Count = 1;
    }
    
    
  }
}