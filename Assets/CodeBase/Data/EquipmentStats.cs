using System;
using System.Collections.Generic;

namespace CodeBase.Data
{
    [Serializable]
    public class EquipmentStats
    {
        public float CoefficientOfLuck;

        public int MaxLiftWeight;

        public int MinFishWeight;

        public int MaxFishWeight;

        public float ChanceGettingOffHook;

        public List<int> Fishes = new List<int>();

        public EquipmentStats()
        {

        }
    }
}
