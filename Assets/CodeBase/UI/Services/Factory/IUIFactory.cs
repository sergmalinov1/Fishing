using System.Threading.Tasks;
using CodeBase.Infrastructure.Services;
using CodeBase.UI.Windows;

namespace CodeBase.UI.Services.Factory
{
  public interface IUIFactory : IService
  {
    Task CreateUIRoot();
    BaseWindow CreateResult();
    BaseWindow CreateShop();
    BaseWindow CreateAchievements();
    BaseWindow CreatePrepareWindow();
    BaseWindow CreateSettingWindow();
    BaseWindow CreateShopCategory();
  }
}