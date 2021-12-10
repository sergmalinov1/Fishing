using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class EquipmentStats
    {



        public int SumRatin;
        public float AvgRating;

        public float CoefficientOfLuck;

        public int MaxLiftWeight;

        public int MinFishWeight;

        public int MaxFishWeight;

        public float ChanceGettingOffHook;

        public List<int> Fishes = new List<int>();

        public List<int> FishesInLake = new List<int>();

        public List<int> FishesInLure = new List<int>();

        public List<bool> ChanceToCatchFish = new List<bool>();


        public Action NewLureItem;
        public Action BuyMoreLure;



        private int SelectedLureId = -1;

        public void Initialize(int newSelectedLureId)
        {
            ChangeLure(newSelectedLureId);

        }
        public void ChangeLure(int lureId)
        {
            if(SelectedLureId == lureId)
            {
                CurrentLure();
                BuyMoreLure?.Invoke();
            }
            else
            {
                SelectedLureId = lureId;
                NewLure();
                NewLureItem?.Invoke();
            }
        }

       

        public void AddRating(int rating)
        {
            SumRatin += rating;
        }

        public void CalculationStats()
        {
            UnicFishes();
            CalculateAvgRating();
        }
        private void CurrentLure()
        {
            Debug.Log("CurrentLure: ");
        }

        private void NewLure()
        {
            Debug.Log("NewLure: ");
        }

        private void CalculateAvgRating()
        {
            AvgRating = (float)SumRatin / Constants.MaxRating; // 
            SumRatin = 0;
         //   Debug.Log("AvgRating: " + AvgRating);

        }

        public void UnicFishes()
        {
            Fishes.Clear();
            foreach (int inLake in FishesInLake)
            {
                foreach (int inLure in FishesInLure)
                {
                    if(inLure == inLake)
                    {
                        Fishes.Add(inLure);
                    }
                }
            }
        }

        public void PrintFishes()
        {
            Debug.Log("FishId======");
            foreach (int fishId in Fishes)
            {
                Debug.Log("FishId: " + fishId);
            }

            foreach (int fishId in FishesInLake)
            {
               // Debug.Log("FishesInLake: " + fishId);
            }

            foreach (int fishId in FishesInLure)
            {
              //  Debug.Log("FishesInLure: " + fishId);
            }
        }

        public void Print()
        {
            Debug.Log("CoefficientOfLuck: " + CoefficientOfLuck);
            Debug.Log("MaxLiftWeight: " + MaxLiftWeight);
            Debug.Log("MinFishWeight: " + MinFishWeight);
            Debug.Log("MaxFishWeight: " + MaxFishWeight);
            Debug.Log("SelectedLureId: " + SelectedLureId);
        }


    }
}
