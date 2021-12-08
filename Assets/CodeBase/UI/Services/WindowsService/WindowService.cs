using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows;
using UnityEngine;

namespace CodeBase.UI.Services.WindowsService
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public BaseWindow Open(WindowId windowId)
        {
            switch (windowId)
            {
                case WindowId.Unknow:
                    break;

                case WindowId.Result:
                    return _uiFactory.CreateResult();

                case WindowId.AchievementsWindow:
                    return _uiFactory.CreateAchievements();

                case WindowId.PrepareWindow:
                    return _uiFactory.CreatePrepareWindow();

                case WindowId.SettingWindow:
                    return _uiFactory.CreateSettingWindow();

                case WindowId.CategoryEquipment:
                    return _uiFactory.CreateShopCategory();

                case WindowId.ListEquipment:
                    return _uiFactory.CreateListEquipment();
            }

            return null;
        }

    }
}