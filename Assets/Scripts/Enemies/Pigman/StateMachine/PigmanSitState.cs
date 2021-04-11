using Kite;
using System;
using System.Collections;
using UnityEngine;

public class PigmanSitState : MonoBehaviour, IPigmanState {

  private PigmanAnimator animator;
  private PigmanAnimationEvents animationEvents;
  private HorizontalFlipComponent flip;
  private bool listenToEvents;
  private Direction2H initialDirection;

  public void StateExit() {
    if (listenToEvents) {
      animationEvents.OnRoarEnd -= OnAnimationEnd;
      animationEvents.OnStandUpEnd -= OnAnimationEnd;
    }
  }

  public void Inject(PigmanController controller) {
    animator = controller.di.animator;
    animationEvents = controller.di.animationEvents;
    flip = controller.di.physics.flip;
    initialDirection = flip.Direction;
  }

  public void StateStart() {
    if (NeedsToWaitForCurrentAnimation()) {
      listenToEvents = true;
      animationEvents.OnRoarEnd += OnAnimationEnd;
      animationEvents.OnStandUpEnd += OnAnimationEnd;
    } else {
      listenToEvents = false;
      animator.SetState(PigmanAnimatorState.Sit);
      flip.Direction = initialDirection;
    }
  }

  private void OnAnimationEnd() {
    animator.SetState(PigmanAnimatorState.Sit);
    flip.Direction = initialDirection;
  }

  private bool NeedsToWaitForCurrentAnimation() =>
    animator.IsState(PigmanAnimatorState.StandUp) ||
    animator.IsState(PigmanAnimatorState.Roar);

  public BtStatus StateUpdate() {
    return BtStatus.Running;
  }
}
