using System;
using System.Collections;
using UnityEngine;

using Bt = BtStatus;

public class PigmanBtBlockingAnimation : MonoBehaviour, IPigmanBtNode
{
  private PigmanAnimator animator;
  private readonly PigmanAnimatorState[] blockingStates = new PigmanAnimatorState[]
  {
    //PigmanAnimatorState.Attack,
    PigmanAnimatorState.Dead,
    PigmanAnimatorState.Hit,
    //PigmanAnimatorState.Stagger,
    //PigmanAnimatorState.Parry,
    //PigmanAnimatorState.Roar
  };

  public bool IsBlockingState() =>
    Array.IndexOf(blockingStates, animator.GetState()) != -1;
  

  public Bt BtUpdate()
  {
    if (IsBlockingState())
    {
      return Bt.Running;
    }
    return Bt.Failure;
  }

  public void Inject(PigmanController controller)
  {
    animator = controller.di.animator;
  }
}