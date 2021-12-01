using System;
using System.Collections.Generic;
using CodeBase.StaticData.Lure;

namespace CodeBase.Data
{
  [Serializable]
  public class Inventory
  {
    public List<InventoryLure> InventoryLures = new List<InventoryLure>();
    
    public void AddItemLure(string itemName, LureTypeId lureType)
    {
  
      foreach (InventoryLure item  in InventoryLures)
      {
        if (item.Name == itemName)
        {
          item.Count++;
          return;
        }
      }

            InventoryLures.Add(new InventoryLure(itemName, lureType));
      
    }

    public void AddStartPack()
    {
            InventoryLures.Add(new InventoryLure("Хлеб", LureTypeId.Bread));
            InventoryLures[0].Count = 10;

    }

    public void SelectItemLure(string itemName)
    {
      foreach (InventoryLure item  in InventoryLures)
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
  }
}