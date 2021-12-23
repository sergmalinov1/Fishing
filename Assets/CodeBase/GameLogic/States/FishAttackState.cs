using CodeBase.Data;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.RandomService;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.States;
using CodeBase.StaticData;
using CodeBase.StaticData.Fish;
using System;
using UnityEngine;

namespace CodeBase.GameLogic.States
{
  public class FishAttackState : IStateLogic
  {

        private readonly IPersistentProgress _progressService;

        private readonly LogicStateMachine _logicStateMachine;
        private readonly IRandomService _randomService;
        private readonly IGameFactory _gameFactory;
        private readonly ISaveLoadService _saveLoadService;
        private IInputService _input;

        private float _timeToEndAttack = 5.0f;

        public FishAttackState(
          LogicStateMachine logicStateMachine,
          IInputService inputService,
          IPersistentProgress progressService,
          IRandomService randomService,
          IGameFactory gameFactory,
          ISaveLoadService saveLoadService)
        {
            _logicStateMachine = logicStateMachine;
            _input = inputService;
            _progressService = progressService;
            _randomService = randomService;
            _gameFactory = gameFactory;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            _timeToEndAttack = 5.0f;

            RandomFish();
            _logicStateMachine.TackleContainer.BobberAnimator.PlayFishBite();
        }



        public void Exit()
        {
        }


        /*
         * Статусы
            s1 Поднять рыбу
            s2 Поднять пустой крючок
            s3 Поднять крючек с наживкой
            s4 Поднять рыбу и обрыв лески
            s5 Соравалась с крючка
         */
        public void UpdateLogic()
        {
            _timeToEndAttack -= Time.deltaTime;
            if (_timeToEndAttack <= 0.0f) //Вышло время ожидания -> s2 Поднять пустой крючок
            {
                UseLure();
                _progressService.Progress.FishOnHook.IsEatLure = true;
                _logicStateMachine.Enter<EmptyHookState>();
                
            }

            if (_input.IsAttackButtonUp())
            {
                bool isPoolInTime = _randomService.IsCatchedFish();
                bool _isCatchStack = _progressService.Progress.EquipmentStats.PeekCatchFishStack();

                if (isPoolInTime == false) //Если не вовремя подсек рыбу
                {
                    //s3 Поднять крючек с наживкой
                }
                else if (_isCatchStack == false) //Если вероятность поймать рыбу нулевая -> s2 Поднять пустой крючок
                {
                    UseLure();
                    _progressService.Progress.FishOnHook.IsBadLuck = true;  //не повезло. повезет в другой раз
                    _logicStateMachine.Enter<EmptyHookState>();
                }
                else if (isPoolInTime && _isCatchStack) //Если вовремя дернул и в стеке True вероятность поймать рыбы -> то вытягиваем рыбы
                {
                    UseLure();
                    CatchFish();

                    if (!IsHoldLine()) // проверка выдержит ли леска вес рыбы. Если рвется -> s4 Поднять рыбу и обрыв лески
                    {                
                        _progressService.Progress.FishOnHook.IsLineBreak = true;
                        _logicStateMachine.Enter<FishWithBreakLineState>();
                    } 
                    else if (!IsHoldHook()) //Проверка держит ли крючек
                    {
                        // если не держит-> s5  Соравалась с крючка
                    }
                    else //Если все условия соблюдены вытягиваем рыбу  -> s1 Поднять рыбу
                    {
                        _progressService.Progress.FishOnHook.IsFishOnHook = true;  
                        _logicStateMachine.Enter<FishOnHookState>();
                    }
                }

            }
        }

    

        private bool IsHoldLine()
        {
            int fishWeight = _progressService.Progress.FishOnHook.FishWeight;
            int maxLineLift = _progressService.Progress.EquipmentStats.MaxLineLiftWeight;

            if(fishWeight > maxLineLift)
                return false;

            return true;
        }

        private bool IsHoldHook()
        {
            return true;
        }

        private void RandomFish()
        {
            FishStaticData fishData = _randomService.RandomFish();
            _progressService.Progress.FishOnHook.SetFish(fishData);
        }

        private void CatchFish()
        {
           // _progressService.Progress.ResultOfFishing.AddCaughtFish(_progressService.Progress.FishOnHook.FishName);
            _progressService.Progress.FishOnHook.CatchFish();
            CreateFishInContainer();

        }

        private async void CreateFishInContainer()
        {
            FishTypeId fishId = _progressService.Progress.FishOnHook.FishTypeId;
            int fishWeight = _randomService.RandomFishSize();
            _progressService.Progress.FishOnHook.SetFishWeight(fishWeight);   
           _logicStateMachine.TackleContainer.Fish = await _gameFactory.CreateFishInContainer(_logicStateMachine.TackleContainer, fishId);
        }

        private void UseLure()
        {
            _progressService.Progress.EquipmentStats.PopCatchFishStack();
            _progressService.Progress.Inventory.UseSelectedEquipmentItem(KindEquipmentId.Lure);
            _saveLoadService.SaveProgress();
        }


    }
}