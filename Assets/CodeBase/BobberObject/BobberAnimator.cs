using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.BobberObject
{
  public class BobberAnimator : MonoBehaviour, IAnimationStateReader
  {
    private static readonly int ThrowBobberHash = Animator.StringToHash("ThrowBobber");
    private static readonly int FishBiteHash = Animator.StringToHash("FishBite");
    private static readonly int PushBobberHash = Animator.StringToHash("Push");
    
    
    private readonly int _throwStateHash = Animator.StringToHash("Throw");
    private readonly int _waitStateHash = Animator.StringToHash("Wait");
    private readonly int _biteStateHash = Animator.StringToHash("Bite");
    private readonly int _hookStateHash = Animator.StringToHash("Hook");
    
    
    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;
   
    public AnimatorState State { get; private set; }
    public Animator Animator;

      
    public void PlayThrowBobber() => Animator.SetTrigger(ThrowBobberHash);
    public void PlayFishBite() => Animator.SetTrigger(FishBiteHash);
    
    public void PlayPushBobber() => Animator.SetTrigger(PushBobberHash);
    
    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
      StateExited?.Invoke(StateFor(stateHash));
    
    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == _waitStateHash)
        state = AnimatorState.Wait;
      if (stateHash == _biteStateHash)
        state = AnimatorState.Bite;
      if (stateHash == _hookStateHash)
        state = AnimatorState.Hook;
      else
        state = AnimatorState.Unknown;
      
      return state;
    }
  }
}