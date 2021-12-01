using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data
{
  [Serializable]
  public class ResultOfFishing
  {

    public List<CaughtFish> CaughtFish = new List<CaughtFish>();
    

    public void AddCaughtFish(string fishName)
    {
  
      foreach (CaughtFish fish in CaughtFish)
      {
        if (fish.FishName == fishName)
        {
          fish.CountCaughtFish++;
          return;
        }
      }
      
      CaughtFish.Add(new CaughtFish(fishName));
      
    }
  }
}