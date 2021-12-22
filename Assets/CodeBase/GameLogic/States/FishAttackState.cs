using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.RandomService;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using CodeBase.StaticData.Fish;
using System;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
  public class FishAttackState : IStateLogic
  {

        private readonly PlayerProgress _playerProgress;
        private readonly LogicStateMachine _logicStateMachine;
        private readonly IRandomService _randomService;
        private readonly IGameFactory _gameFactory;
        private IInputService _input;

        private float _timeToEndAttack = 5.0f;
        private bool _isCatchStack = false; //Шанс поимки рыбы на основании стека
        private bool _isCatch = false;

        public FishAttackState(
          LogicStateMachine logicStateMachine,
          IInputService inputService,
          PlayerProgress playerProgress,
          IRandomService randomService,
          IGameFactory gameFactory)
        {
            _logicStateMachine = logicStateMachine;
            _input = inputService;
            _playerProgress = playerProgress;
            _randomService = randomService;
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            _isCatchStack = false;
            _isCatch = false;
            _timeToEndAttack = 5.0f;
            RandomFish();
            UseLure();
            _logicStateMachine.TackleContainer.BobberAnimator.PlayFishBite();
        }



        public void Exit()
        {
        }

        public void UpdateLogic()
        {
            _timeToEndAttack -= Time.deltaTime;
            if (_timeToEndAttack <= 0.0f)
            {
                NotCatchFish();
            }

            if (_input.IsAttackButtonUp())
            {
                if (_randomService.IsCatchedFish() && _isCatchStack)
                {
                    CatchFish();
                }
                else
                {
                    NotCatchFish();
                }
            }

            if(_isCatch)
            {
                if(!IsHoldLine())
                {
                    LineBreak();
                }

                _logicStateMachine.Enter<ResultState>();
            }
        }

        private void LineBreak()
        {
            _playerProgress.FishOnHook.IsLineBreak = true;
        }

        private bool IsHoldLine()
        {
            int fishWeight = _playerProgress.FishOnHook.FishWeight;
            int maxLineLift = _playerProgress.EquipmentStats.MaxLineLiftWeight;

            if(fishWeight > maxLineLift)
                return false;

            return true;
        }

        private void CatchFish()
        {
            _playerProgress.ResultOfFishing.AddCaughtFish(_playerProgress.FishOnHook.FishName);
            _playerProgress.FishOnHook.CatchFish();
            CreateFish();
            _isCatch = true;

        }

        private void NotCatchFish()
        {
            _playerProgress.FishOnHook.NotCatchFish();
            _isCatch = true;
        }

        private async void CreateFish()
        {
            FishTypeId fishId = _playerProgress.FishOnHook.FishTypeId;

            int fishWeight = _randomService.RandomFishSize();

            _playerProgress.FishOnHook.SetFishWeight(fishWeight);   

           _logicStateMachine.TackleContainer.Fish = await _gameFactory.CreateFishInContainer(_logicStateMachine.TackleContainer, fishId);
        }

        private void RandomFish()
        {
            FishStaticData fishData = _randomService.RandomFish();

            _playerProgress.FishOnHook.SetFish(fishData);

        }

        private void UseLure()
        {
            _isCatchStack = _playerProgress.EquipmentStats.PopCatchFishStack();

            _playerProgress.Inventory.UseSelectedEquipmentItem(KindEquipmentId.Lure);
            _logicStateMachine.TackleContainer.DestroyLure();
        }


    }
}