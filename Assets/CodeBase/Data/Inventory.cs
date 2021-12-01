using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using CodeBase.StaticData.Lure;

namespace CodeBase.Data
{
  [Serializable]
  public class Inventory
  {
    public List<InventoryItem> InventoryItems = new List<InventoryItem>();
    
    public void AddItem(string itemName, LureTypeId lureType)
    {
  
      foreach (InventoryItem item  in InventoryItems)
      {
        if (item.Name == itemName)
        {
          item.Count++;
          return;
        }
      }
      
      InventoryItems.Add(new InventoryItem(itemName, lureType));
      
    }

    public void AddStartPack()
    {
      InventoryItems.Add(new InventoryItem("Хлеб", LureTypeId.Bread));
      InventoryItems[0].Count = 10;

    }

    public void SelectItem(string itemName)
    {
      foreach (InventoryItem item  in InventoryItems)
      {
        if (item.Name == itemName)
        {
          item.Count--;
          if (item.Count <= 0)
          {
            InventoryItems.Remove(item);
          }
          return;
        }
      }
    }
  }
}