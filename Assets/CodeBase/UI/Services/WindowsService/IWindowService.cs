
using CodeBase.Infrastructure.Services;
using CodeBase.UI.Windows;


namespace CodeBase.UI.Services.WindowsService
{
  public interface IWindowService : IService
  {
    BaseWindow Open(WindowId windowId);

    }
}