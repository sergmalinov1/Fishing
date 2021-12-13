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

        public int SelectedLureId = -1;

        public Action NewLureItem;
        public Action BuyMoreLure;
        public Action ChangeStats;

        

        public void Initialize(int newSelectedLureId)
        {
            ChangeLure(newSelectedLureId);

        }
        public void ChangeLure(int lureId)
        {
            if(SelectedLureId == lureId)
            {
                CurrentLure();
            }
            else
            {
                SelectedLureId = lureId;
                NewLure();
            }
        }

       

        public void AddRating(int rating)
        {
            SumRatin += rating;
        }

        public void CalculationStats()
        {
            ChangeStats?.Invoke();
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
            AvgRating = (float)SumRatin / Constants.MaxRating;  
            SumRatin = 0;
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

            Debug.Log("FishesInLake ======");
            foreach (int fishId in FishesInLake)
            {
                Debug.Log("FishesInLake: " + fishId);
            }

            Debug.Log("FishesInLure ======");
            foreach (int fishId in FishesInLure)
            {
                Debug.Log("FishesInLure: " + fishId);
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
