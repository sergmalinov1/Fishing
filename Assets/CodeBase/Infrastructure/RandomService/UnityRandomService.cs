﻿using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using CodeBase.StaticData.Fish;
using UnityEngine;

namespace CodeBase.Infrastructure.RandomService
{
  public class UnityRandomService : IRandomService
  {
        private readonly IAssetProvider _assets;
        private readonly IPersistentProgress _progressService;
        private readonly IStaticDataService _staticData;
        private readonly PlayerProgress _progres;

        public UnityRandomService(IAssetProvider assets, IPersistentProgress progressService, IStaticDataService staticData)
        {
            _assets = assets;
            _progressService = progressService;
            _staticData = staticData;
            _progres = progressService.Progress;
        }


        public float TimeToBite()
        {
            return 4.0f;
        }

        public FishStaticData RandomFish()
        {
            float total = 0;

            List<int> fishesId = _progressService.Progress.EquipmentStats.Fishes;

            Debug.Log("count " + fishesId.Count);

            
            Dictionary<FishTypeId, FishStaticData> fishes = new Dictionary<FishTypeId, FishStaticData>();
                 
            foreach (int fish in fishesId)
            {
                FishTypeId fishTypeId = (FishTypeId)fish;
                FishStaticData temp = _staticData.ForFish(fishTypeId);
                fishes.Add(fishTypeId, temp);
            }

            foreach (KeyValuePair<FishTypeId, FishStaticData> fish in fishes)
            {
                total += fish.Value.ChanceOfBite;
            }

            float randomPoint = Random.value * total;

            foreach (KeyValuePair<FishTypeId, FishStaticData> fish in fishes)
            {
                if (randomPoint < fish.Value.ChanceOfBite)
                {
                    return fish.Value;
                }
                else
                {
                    randomPoint -= fish.Value.ChanceOfBite;
                }
            }

            return fishes[0];
        }

        public bool IsCatchedFish()
        {
            return true;
        }


        private void GenerateLureStack()
        {

        }

        private void PrintFish(Dictionary<FishTypeId, FishStaticData> fishes)
        {
            foreach (KeyValuePair<FishTypeId, FishStaticData> fish in fishes)
            {
                Debug.Log(fish.Value.FishName);
            }
            Debug.Log("==============");
        }


        private Dictionary<FishTypeId, FishStaticData> FilterByLure(Dictionary<FishTypeId, FishStaticData> fishes)
        {

            int typeLureId =  _progressService.Progress.Inventory.GetSelectedEquipmentByKind(KindEquipmentId.Lure);

            Equipment equipmentLure = _staticData.GetEquipment(KindEquipmentId.Lure, typeLureId);
            LureStaticData lure = equipmentLure as LureStaticData; //ПЕРЕДЕЛАТЬ!


            int typeLaceId = _progressService.Progress.Inventory.GetSelectedEquipmentByKind(KindEquipmentId.Lake);
            Equipment equipmentLake = _staticData.GetEquipment(KindEquipmentId.Lake, typeLaceId);
            LakeStaticData lake = equipmentLake as LakeStaticData; //ПЕРЕДЕЛАТЬ!




            Dictionary<FishTypeId, FishStaticData> filteredFishes = new Dictionary<FishTypeId, FishStaticData>();

            foreach (KeyValuePair<FishTypeId, FishStaticData> fish in fishes)
            {
                foreach (FishTypeId FishEat in lure.TypeFishEat)
                {
                    if (fish.Key == FishEat)
                    {
                        filteredFishes.Add(fish.Key, fish.Value);
                    }
                }
            }
            return filteredFishes;

            /* LureTypeId lureType = _progressService.Progress.FishOnHook.LureTypeId;
             LureStaticData lure = _staticData.ForLure(lureType);
             Dictionary<FishTypeId, FishStaticData> filteredFishes = new Dictionary<FishTypeId, FishStaticData>();

             foreach (KeyValuePair<FishTypeId, FishStaticData> fish in fishes)
             {
                 foreach (FishTypeId FishEat in lure.TypeFishEat)
                 {
                     if (fish.Key == FishEat)
                     {
                         filteredFishes.Add(fish.Key, fish.Value);
                     }
                 }
             }*/

            //  return filteredFishes;
            // return null;

        }


        //Алгоритм рандома на примере массива
        private float Choose(float[] probs)
        {

            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }
            return probs.Length - 1;
        }


    }
}