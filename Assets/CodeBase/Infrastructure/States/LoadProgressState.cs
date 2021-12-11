using System;
using System.Threading.Tasks;
using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Inventory;
using CodeBase.Infrastructure.RandomService;
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
        private readonly IInventoryService _inventoryService;

        public LoadProgressState(
          GameStateMachine stateMachine,
          SceneLoader sceneLoader,
          LoadingCurtain curtain,
          IPersistentProgress progressService,
          ISaveLoadService saveLoadService,
          IInventoryService inventoryService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _inventoryService = inventoryService;
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
           // _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

            if(_saveLoadService.LoadProgress() == null)
            {
                _progressService.Progress = NewProgress();
                _inventoryService.SetEquipmentState();

            }
            else
            {
                _progressService.Progress = _saveLoadService.LoadProgress();
            }           
        }

        private PlayerProgress NewProgress()
        {
            PlayerProgress progress = new PlayerProgress();
            progress.MoneyData.Money = 100;

            progress.Inventory.AddStartPack();
            progress.EquipmentStats.Initialize(0); //айди наживки
         
            return progress;

        }
    }
}