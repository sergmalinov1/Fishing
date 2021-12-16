using System.Collections.Generic;
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
        //private readonly PlayerProgress _progres;

        public UnityRandomService(IAssetProvider assets, IPersistentProgress progressService, IStaticDataService staticData)
        {
            _assets = assets;
            _progressService = progressService;
            _staticData = staticData;
        }


        public float TimeToBite()
        {
            return 2.0f;
        }

        public FishStaticData RandomFish()
        {
            //  _progressService.Progress.EquipmentStats.Print();
            //  _progressService.Progress.EquipmentStats.PrintFishes();

            float total = 0;

            List<int> fishesId = _progressService.Progress.EquipmentStats.Fishes;

            Dictionary<FishTypeId, FishStaticData> fishes = new Dictionary<FishTypeId, FishStaticData>();
                 
            foreach (int fish in fishesId)
            {
                FishTypeId fishTypeId = (FishTypeId)fish;
                FishStaticData fishStaticData = _staticData.ForFish(fishTypeId);
                fishes.Add(fishTypeId, fishStaticData);
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
           
            return fishes[0];  //Значение по умолчанию. Сюда не должно дойти
        }

        public bool IsCatchedFish()
        {
            return true;
        }


        private Dictionary<FishTypeId, FishStaticData> FilterByLureOLD(Dictionary<FishTypeId, FishStaticData> fishes)
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

        private void AddInQueue()
        {
        }


        public void GenerateNewLureStack()
        {
            _progressService.Progress.EquipmentStats.StackToCatchFish.Clear();

           // float chanceToCatchFish = _progres.EquipmentStats.AvgRating;

            int lureCount = _progressService.Progress.Inventory.GetCountSelectedEquipmentByKind(KindEquipmentId.Lure);

            for(int i = 0; i< lureCount; i++)
            {
                int rand = Random.Range(0, 3);
                bool chanse = true;

                if(rand == 0)
                {
                    chanse = false;
                }

                _progressService.Progress.EquipmentStats.StackToCatchFish.Add(chanse);
            }

            foreach(bool temp in _progressService.Progress.EquipmentStats.StackToCatchFish)
            {
                Debug.Log("StackToCatchFish: " + temp);
            }

        }
    }
}