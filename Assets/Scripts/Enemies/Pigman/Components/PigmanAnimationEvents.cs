using System;
using System.Collections;
using UnityEngine;

public class PigmanAnimationEvents : MonoBehaviour, IPigmanComponent {

  private PigmanAnimator animator;

  public event Action OnRoarEnd;
  public event Action OnAttackEnd;
  public event Action OnAttackStart = delegate { };
  public event Action OnStandUpEnd;

  public void EmitAttackEnd() {
    EmitActionOrSetIdle(OnAttackEnd);
  }

  public void EmitStandUpEnd() {
    EmitActionOrSetIdle(OnStandUpEnd);
  }

  public void EmitRoarEnd() {
    EmitActionOrSetIdle(OnRoarEnd);
  }

  public void EmitAttackStart() {
    OnAttackStart();
  }

  private void EmitActionOrSetIdle(Action action) {
    if (action.GetInvocationList().Length > 0) {
      action();
    } else {
      animator.SetState(PigmanAnimatorState.Idle);
    }
  }

  public void Inject(PigmanController controller) {
  }
}
