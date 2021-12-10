using CodeBase.Data;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace CodeBase.StaticData.Visitor
{
    public class GetEquipmentStats : ISelectableEntityVisitor
    {
        private readonly EquipmentStats _equipmentStats;

        public GetEquipmentStats(EquipmentStats equipmentStats)
        {
            _equipmentStats = equipmentStats;
        }


        public void Visit(BobberStaticData bobber)
        {
            _equipmentStats.CoefficientOfLuck = bobber.CoefficientOfLuck;
            _equipmentStats.AddRating(bobber.Rating);
        }

        public void Visit(FishingLineStaticData fishingLine)
        {
            _equipmentStats.MaxLiftWeight = fishingLine.MaxLiftWeight;
            _equipmentStats.AddRating(fishingLine.Rating);
        }

        public void Visit(FishingRodStaticData fishingRod)
        {
            _equipmentStats.MinFishWeight = fishingRod.MinFishWeight;
            _equipmentStats.MaxFishWeight = fishingRod.MaxFishWeight;
            _equipmentStats.AddRating(fishingRod.Rating);
        }

        public void Visit(HookStaticData hook)
        {
            _equipmentStats.AddRating(hook.Rating);
        }

        public void Visit(LakeStaticData lake)
        {
            _equipmentStats.FishesInLake.Clear();   
            foreach (FishTypeId fishTypeId in lake.TypeFishAreFound)
            {
                _equipmentStats.FishesInLake.Add((int)fishTypeId);
            }
            _equipmentStats.AddRating(lake.Rating);
        }

        public void Visit(LureStaticData lure)
        {
            _equipmentStats.FishesInLure.Clear();
            foreach (FishTypeId fishTypeId in lure.TypeFishEat)
            {
                _equipmentStats.FishesInLure.Add((int)fishTypeId);
            }
            _equipmentStats.AddRating(lure.Rating);
            _equipmentStats.ChangeLure((int)lure.LureTypeId);
        }
    }
}
