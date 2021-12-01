using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.Setting
{
  public class SettingWindow : BaseWindow
  {
    public TextMeshProUGUI Title;

    public Button ResetButton;
    
    public void Construct()
    {
    }
    
    public void Initialize()
    {
      Title.text = "Настройки";
      ResetButton.onClick.AddListener(ResetAccount);
    }

    public void ResetAccount()
    {
      PlayerPrefs.DeleteAll();
      PlayerPrefs.Save();
      Application.Quit();
    }
  }
}