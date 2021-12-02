using System.Collections.Generic;
using System.Threading.Tasks;
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
    
    Task<GameObject> CreateHud();
    Task<GameObject> CreareBobberSpawner();
    Task<GameObject> CreateBobber(Vector3 at);
    Task<GameObject> CreateSplash(Vector3 at);
    Task<GameObject> CreareFishingLogic();
    Task<GameObject> CreateFish(FishTypeId fishTypeId, Vector3 at);
  }
}