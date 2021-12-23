using CodeBase.Data;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows
{
  public class FishingResult : BaseWindow
  {
    public TextMeshProUGUI FishName;
    public TextMeshProUGUI FishSize;
    public TextMeshProUGUI PrizeAmmount;
    public TextMeshProUGUI PrizePoints;

    private PlayerProgress _playerProgress;
    private ISaveLoadService _saveLoadService;

    public void Construct(PlayerProgress playerProgress)
    {
      _playerProgress = playerProgress;
      _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
    }
    
    protected override void Initialize()
    {
      SetFishData();
    }
    
    protected override void SubscribeUpdate()
    {
    }

    protected override void Cleanup()
    {
    }
    
    private void SetFishData()
    {
            if (_playerProgress.FishOnHook.IsFishOnHook)
            {
                FishName.text = $"{_playerProgress.FishOnHook.FishName}";
                FishSize.text = $"{_playerProgress.FishOnHook.FishWeight} sm";
                PrizeAmmount.text = $"{_playerProgress.FishOnHook.PrizeMoney}";
                PrizePoints.text = $"777";
            }
            else if (_playerProgress.FishOnHook.IsLineBreak)
            {
                FishName.text = $"Обрыв";
                FishSize.text = $"лески";
                PrizeAmmount.text = "0";
                PrizePoints.text = "0";
            }
            else if (_playerProgress.FishOnHook.IsEatLure)
            {
                FishName.text = $"Съели";
                FishSize.text = $"приманку";
                PrizeAmmount.text = "0";
                PrizePoints.text = "0";
            }
            else if(_playerProgress.FishOnHook.IsBadLuck)
            {
                FishName.text = $"НЕ";
                FishSize.text = $"повезло(";
                PrizeAmmount.text = "0";
                PrizePoints.text = "0";
            }
            else 
            {
                FishName.text = $"Попробуй";
                FishSize.text = $"еще раз";
                PrizeAmmount.text = "0";
                PrizePoints.text = "0";
            }
    }
  }
}