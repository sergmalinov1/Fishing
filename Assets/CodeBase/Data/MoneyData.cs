using System;

namespace CodeBase.Data
{
  [Serializable]
  public class MoneyData
  {
    public int Money;
    
    public Action Changed;
    
    public void Add(int loot)
    {
      Money += loot;
      Changed?.Invoke();
    }

    public void Subtract(int loot)
    {
      Money -= loot;
      Changed?.Invoke();
    }
  }
}