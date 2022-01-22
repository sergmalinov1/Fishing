using System;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public FishOnHook FishOnHook;
        public MoneyData MoneyData;
        public ResultOfFishing ResultOfFishing;
        public Inventory Inventory;
        public SettingWindow SettingWindow;
        public EquipmentStats EquipmentStats;
        public PurchaseData PurchaseData;
        public PlayerProgress()
        {
            FishOnHook = new FishOnHook();
            MoneyData = new MoneyData();
            ResultOfFishing = new ResultOfFishing();
            Inventory = new Inventory();
            SettingWindow = new SettingWindow();
            EquipmentStats = new EquipmentStats();
            PurchaseData = new PurchaseData();
        }
    }
}