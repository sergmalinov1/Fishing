using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace CodeBase.Infrastructure.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private readonly IPersistentProgress _progressService;
   
    private const string ProgressKey = "Progress";

    public SaveLoadService(IPersistentProgress progressService)
    {
      _progressService = progressService;
      //_gameFactory = gameFactory;
    }

    public void SaveProgress()
    {
      /*foreach (ISavedProgress progressWriter in _gameFactory.ProgressWriters)
      {
        progressWriter.UpdateProgress(_progressService.Progress);
      }*/

      PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
    }

    public PlayerProgress LoadProgress() =>
      PlayerPrefs.GetString(ProgressKey)?
        .ToDeserialized<PlayerProgress>();
  }
}