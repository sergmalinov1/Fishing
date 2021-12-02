using System;
using CodeBase.StaticData.Fish;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Data
{
  [Serializable]
  public class FishOnHook
  {
    public Action Cathed;
    public Action SelectedLure;
    
    public LureTypeId LureTypeId;
    public FishTypeId FishTypeId;  
    public bool IsFishOnHook;
    public string FishName;
    public int PrizeMoney;
    public int ChanceToCatch;

    public void SetFish(FishStaticData fishData)
    {
      FishName = fishData.FishName;
      PrizeMoney = fishData.PrizeMoney;
      ChanceToCatch = fishData.ChanceToCatch;
      FishTypeId = fishData.FishTypeId;
    }
    
    public void CatchFish()
    {
      IsFishOnHook = true;
      Cathed?.Invoke();
    }

    public void NotCatchFish()
    {
      IsFishOnHook = false;
      Cathed?.Invoke();
    }

    public void SelectLure(LureTypeId lureTypeId)
    {
      LureTypeId = lureTypeId;
      SelectedLure?.Invoke();
    }
  }
}