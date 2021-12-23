using System;
using CodeBase.StaticData.Fish;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Data
{
  [Serializable]
  public class FishOnHook
  {
        public Action Cathed;
        public Action SelectedLure;

        public bool IsFishOnHook = false;
        public bool IsLineBreak = false;
        public bool IsEatLure = false;
        public bool IsBadLuck = false;


        public LureTypeId LureTypeId;
        public FishTypeId FishTypeId;
        
        public string FishName;
        public int PrizeMoney;
        public int ChanceToCatch;
        public int FishWeight;



        public void SetFishWeight(int fishWeight)
        {
            FishWeight = fishWeight;
        }


        public void SetFish(FishStaticData fishData)
        {
            FishName = fishData.FishName;
            PrizeMoney = fishData.PrizeMoney;
            ChanceToCatch = fishData.ChanceToCatch;
            FishTypeId = fishData.FishTypeId;
        }

        public void CatchFish()
        {
            IsFishOnHook = true;
            Cathed?.Invoke();
        }

        public void NotCatchFish()
        {
            IsFishOnHook = false;
            Cathed?.Invoke();
        }

        public void SelectLure(LureTypeId lureTypeId)
        {
            LureTypeId = lureTypeId;
            SelectedLure?.Invoke();
        }

        public void ClearBool()
        {
            IsFishOnHook = false;
            IsLineBreak = false;
            IsEatLure = false;
            IsBadLuck = false;
        }
    }
}