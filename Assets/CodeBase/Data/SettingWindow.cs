using CodeBase.StaticData;
using System;

namespace CodeBase.Data
{
    [Serializable]
    public class SettingWindow
    {
        public KindEquipmentId KindOpenedWindowList = KindEquipmentId.Bobber;

        public string MsgForPopup = "";

        //Открытие окна которое ставит на паузу основной процесс игры. 
        //Не используется!
        public Action OpenWindow;
        public Action CloseWindow;


        //Событие которое срабатывает после запуска основной логики игры. Используется для блокирования HUD
        public Action StartGameLoop;
        public Action EndGameLoop;
    }
}
