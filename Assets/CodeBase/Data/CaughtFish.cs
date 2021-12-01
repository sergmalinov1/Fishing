using System;

namespace CodeBase.Data
{
  [Serializable]
  public class CaughtFish
  {
    public string FishName;
    public int CountCaughtFish;
    
    public CaughtFish(string fishName)
    {
      FishName = fishName;
      CountCaughtFish = 1;
    }
  }
}