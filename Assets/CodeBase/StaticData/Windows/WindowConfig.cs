using System;
using CodeBase.UI.Services.WindowsService;
using CodeBase.UI.Windows;


namespace CodeBase.StaticData.Windows
{
  [Serializable]
  public class WindowConfig
  {
    public WindowId WindowId;
    public BaseWindow Prefab;
  }
}