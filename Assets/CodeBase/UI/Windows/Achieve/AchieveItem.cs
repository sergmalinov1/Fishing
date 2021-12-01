using CodeBase.Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Achieve
{
  public class AchieveItem : MonoBehaviour
  {
    public TextMeshProUGUI AchivName;
    public TextMeshProUGUI AchivCount;
    
    public Image Icon;
    
    private IPersistentProgress _progressService;
    
    public void Construct(string caughtFishFishName, int caughtFishCountCaughtFish)
    {
      AchivName.text = caughtFishFishName;
      AchivCount.text = $"{caughtFishCountCaughtFish.ToString()} шт";
      
    }



  }
}