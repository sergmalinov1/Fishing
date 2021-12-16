using CodeBase.Data;
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
    private readonly IStaticDataService _staticData;
    
    private readonly LogicStateMachine _logicStateMachine;
    private readonly IRandomService _randomService;
    
    private IInputService _input;

    private float _timeToEndAttack = 5.0f;

    public FishAttackState(
      LogicStateMachine logicStateMachine, 
      IInputService inputService, 
      PlayerProgress playerProgress, 
      IStaticDataService staticData, 
      IRandomService randomService)
    {
      _logicStateMachine = logicStateMachine;
      _input = inputService;
      _playerProgress = playerProgress;
      _staticData = staticData;
      _randomService = randomService;

    }

        public void Enter()
        {
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
                if (_randomService.IsCatchedFish())
                {
                    CatchFish();
                }
                else
                {
                    NotCatchFish();
                }
            }
        }

        private void CatchFish()
        {
            _playerProgress.ResultOfFishing.AddCaughtFish(_playerProgress.FishOnHook.FishName);
            _playerProgress.FishOnHook.CatchFish();
            EndAttack();
        }

        private void NotCatchFish()
        {
            _playerProgress.FishOnHook.NotCatchFish();
            EndAttack();
        }

        private void EndAttack()
        {
            _logicStateMachine.Enter<ResultState>();
        }

        private void RandomFish()
        {
            FishStaticData fishData = _randomService.RandomFish();

            _playerProgress.FishOnHook.SetFish(fishData);
        }

        private void UseLure()
        {
            _logicStateMachine.TackleContainer.DestroyLure();
        }


    }
}