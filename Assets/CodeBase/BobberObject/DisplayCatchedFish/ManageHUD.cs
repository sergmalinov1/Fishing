using System;
using CodeBase.Data;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.BobberObject.DisplayCatchedFish
{
  public class ManageHUD : MonoBehaviour
  {

    public Button SellFish;
    public Button OneMore;
    
    public GameObject GameElements;
    public GameObject WinElements;
    public GameObject LoseElements;
    
    
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

    public void Initialize()
    { 
      SellFish.onClick.AddListener(OnSellClick);
      OneMore.onClick.AddListener(GoToGameState);
      
      _playerProgress.FishOnHook.Cathed += SwitchToCatchState;
    }

    public void SwitchToGameState()
    {
      WinElements.SetActive(false);
      LoseElements.SetActive(false);
      GameElements.SetActive(true);
    }

    public void SwitchToCatchState()
    {
     /* if (_playerProgress.fishOnHook.FishOnHook)
      {
        WinElements.SetActive(true);
        GameElements.SetActive(false);

        SetFishData();
      }
      else
      {
        LoseElements.SetActive(true);
        GameElements.SetActive(false);
      }*/
    }

    private void OnSellClick()
    {
      SwitchToGameState();
      _playerProgress.MoneyData.Add(_playerProgress.FishOnHook.PrizeMoney);
      _saveLoadService.SaveProgress();
    }

    private void GoToGameState()
    {
      SwitchToGameState();
      _playerProgress.MoneyData.Add(0);
    }

    private void SetFishData()
    {
      FishName.text = $"{_playerProgress.FishOnHook.FishName}";
      FishSize.text = $"12 sm";
      PrizeAmmount.text = $"{_playerProgress.FishOnHook.PrizeMoney}";
      PrizePoints.text = $"777";
    }


    private void OnDestroy()
    {
      _playerProgress.FishOnHook.Cathed -= SwitchToCatchState;
    }
  }
}