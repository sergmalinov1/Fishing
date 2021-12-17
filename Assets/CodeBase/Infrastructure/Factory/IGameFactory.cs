using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.GameLogic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
  
  public interface IGameFactory : IService
  {

        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }

        Task CreateBackground();

        Task<GameObject> CreateHud();

        Task<GameObject> CreateSplash(Vector3 at);
        Task<GameObject> CreareFishingLogic();
        Task<GameObject> CreateFish(FishTypeId fishTypeId, Vector3 at);
  

        Task<GameObject> CreateTackleContainer(Vector3 position);

        Task<GameObject> CreateLureInContainer(TackleContainer tackleContainer, int lureId);
        Task<GameObject> CreateBobberInContainer(TackleContainer tackleContainer, int bobberId);

        Task<GameObject> CreateFishInContainer(TackleContainer tackleContainer, FishTypeId fishId);


        //-----
        Task<GameObject> CreateBobber(Vector3 at);
      
    }
}