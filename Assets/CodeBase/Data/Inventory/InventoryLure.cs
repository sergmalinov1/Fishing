using System;
using CodeBase.StaticData;

namespace CodeBase.Data
{
  [Serializable]
  public class InventoryLure
  {
    public string Name;
    public int Count;
    public LureTypeId LureType;
    
    public InventoryLure(string itemName, LureTypeId lureType)
    {
      Name = itemName;
      LureType = lureType;
      Count = 1;
    }
    
    
  }
}