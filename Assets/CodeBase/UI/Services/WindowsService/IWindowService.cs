using CodeBase.UI.Windows.ShopCategory;
using CodeBase.Infrastructure.Services;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.WindowsService
{
  public interface IWindowService : IService
  {
    BaseWindow Open(WindowId windowId);

    BaseWindow OpenByCategory(CategoryShopId categoryId);
    }
}