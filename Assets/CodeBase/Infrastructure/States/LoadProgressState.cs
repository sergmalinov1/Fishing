using System;
using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class LoadProgressState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly IPersistentProgress _progressService;
    private SceneLoader _sceneLoader;
    private LoadingCurtain _curtain;
    private ISaveLoadService _saveLoadService;

    public LoadProgressState(
      GameStateMachine stateMachine, 
      SceneLoader sceneLoader, 
      LoadingCurtain curtain, 
      IPersistentProgress progressService,
      ISaveLoadService saveLoadService)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _curtain = curtain;
      _progressService = progressService;
      _saveLoadService = saveLoadService;
    }
      

    public void Enter()
    {
      _curtain.Show();
      LoadProgressOrInitNew();
      _stateMachine.Enter<LoadLevelState, string>(Constants.GameScene);
    }
    
    public void Exit()
    {
      _curtain.Hide();
    }

    private void LoadProgressOrInitNew()
    {
       _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();
     // _progressService.Progress = NewProgress();
    }

    private PlayerProgress NewProgress()
    {
      PlayerProgress progress = new PlayerProgress();
      progress.MoneyData.Money = 100;
      progress.Inventory.AddStartPack();
      return progress;
      
    }


  }
}