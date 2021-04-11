using System;
using System.Collections;
using UnityEngine;

public class PigmanAnimator : MonoBehaviour, IPigmanComponent {

  private static readonly int stateHash = Animator.StringToHash("State");

  public Animator animator;

  public void SetState(PigmanAnimatorState state) { 
    animator.SetInteger(stateHash, (int)state);
  }

  public bool IsState(PigmanAnimatorState state) => GetState() == state;

  public PigmanAnimatorState GetState() => (PigmanAnimatorState)animator.GetInteger(stateHash);

  public void Inject(PigmanController controller) {
  }
}
