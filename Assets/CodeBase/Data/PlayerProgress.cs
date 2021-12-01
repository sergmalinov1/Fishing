using System;
using UnityEngine;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerProgress
  {
    public FishOnHook FishOnHook;
    public MoneyData MoneyData;
    public ResultOfFishing ResultOfFishing;
    public Inventory Inventory;

    public PlayerProgress()
    {
      FishOnHook = new FishOnHook();
      MoneyData = new MoneyData();
      ResultOfFishing = new ResultOfFishing();
      Inventory = new Inventory();
    }
  }
}