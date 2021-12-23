using CodeBase.UI.Services.Factory;
using CodeBase.UI.Windows;
using System;
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

                case WindowId.PrepareWindow:
                    return _uiFactory.CreatePrepareWindow();

                case WindowId.SettingWindow:
                    return _uiFactory.CreateSettingWindow();

                case WindowId.CategoryEquipment:
                    return _uiFactory.CreateShopCategory();

                case WindowId.ListEquipment:
                    return _uiFactory.CreateListEquipment();

                case WindowId.InfoPopup:
                    return _uiFactory.CreateInfoPopup();

                case WindowId.AdsWindow:
                    return _uiFactory.CreateAdsWindow();
            }

            return null;
        }

    }
}