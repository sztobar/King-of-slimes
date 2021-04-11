using Kite;
using System;
using System.Collections;
using UnityEngine;
using Bt = BtStatus;

  public class PigmanGoBackState : MonoBehaviour, IPigmanState {

  public float getBackTolerance = 1f;

  private Vector2 goBackPosition;

  private ScriptablePigman data;
  private PigmanAnimator animator;
  private PigmanAnimationEvents animationEvents;
  private PigmanPhysics physics;

  private float waitTimeLeft;
  private bool waitForRoarEnd;

  public void StateExit() {
    animationEvents.OnRoarEnd -= OnRoarEnd;
  }

  public void Inject(PigmanController controller) {
    physics = controller.di.physics;
    animator = controller.di.animator;
    animationEvents = controller.di.animationEvents;
    data = controller.data;

    goBackPosition = controller.transform.position;
  }

  public void StateStart() {
    animationEvents.OnRoarEnd += OnRoarEnd;
    if (animator.IsState(PigmanAnimatorState.Roar)) {
      waitForRoarEnd = true;
    } else {
      BeginWaitForGoingBack();
    }
  }

  private void BeginWaitForGoingBack() {
    waitTimeLeft = RandomRange.FromVector(data.abandonChaseTime);
    animator.SetState(PigmanAnimatorState.Idle);
  }

  public void OnRoarEnd() {
    waitForRoarEnd = false;
    BeginWaitForGoingBack();
  }

  public Bt StateUpdate() {
    if (waitForRoarEnd && animator.IsState(PigmanAnimatorState.Roar)) {
      return Bt.Running;
    } else if (waitTimeLeft > 0) {
      return WaitUpdate();
    } else {
      return WalkUpdate();
    }
  }

  private Bt WalkUpdate() {
    Vector2 distance = goBackPosition - (Vector2)transform.position;
    float distanceX = distance.x;
    Direction2H moveDirection = Direction2HHelpers.FromFloat(distanceX);
    physics.WalkInDirection(moveDirection);
    return Bt.Running;
  }

  internal bool IsAtStart() {
    Vector2 distance = goBackPosition - (Vector2)transform.position;
    float distanceX = distance.x;
    float distanceXAmount = Mathf.Abs(distanceX);
    return distanceXAmount <= getBackTolerance;
  }

  private Bt WaitUpdate() {
    waitTimeLeft -= Time.deltaTime;
    if (waitTimeLeft <= 0) {
      animator.SetState(PigmanAnimatorState.Walk);
    }
    return Bt.Running;
  }
}
