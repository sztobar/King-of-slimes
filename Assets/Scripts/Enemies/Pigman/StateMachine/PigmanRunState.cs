using Kite;
using UnityEngine;

public class PigmanRunState : MonoBehaviour, IPigmanState {

  private Phase phase = Phase.None;
  private ScriptablePigman data;
  private PigmanAnimator animator;
  private PigmanAnimationEvents animationEvents;
  private PigmanPhysics physics;
  private PigmanRange range;

  private float waitTimeLeft;
  private Direction2H moveDirection;

  public void StateExit() {
    phase = Phase.None;
    animationEvents.OnRoarEnd -= OnRoarEnd;
    animationEvents.OnStandUpEnd -= OnStandUpEnd;
  }

  public void Inject(PigmanController controller) {
    animator = controller.di.animator;
    physics = controller.di.physics;
    animationEvents = controller.di.animationEvents;
    data = controller.data;
    range = controller.di.range;
  }

  public void StateStart() {
    animationEvents.OnRoarEnd += OnRoarEnd;
    animationEvents.OnStandUpEnd += OnStandUpEnd;

    physics.flip.Direction = moveDirection;
    if (animator.IsState(PigmanAnimatorState.Sit)) {
      animator.SetState(PigmanAnimatorState.StandUp);
      phase = Phase.Stand;
    } else {
      float waitToRunTime = RandomRange.FromVector(data.abandonChaseTime);
      if (waitToRunTime > 0) {
        waitTimeLeft = waitToRunTime;
        animator.SetState(PigmanAnimatorState.Idle);
        phase = Phase.Wait;
      } else {
        animator.SetState(PigmanAnimatorState.Run);
        phase = Phase.Run;
      }
    }
  }

  internal void OnStandUpEnd() {
    animator.SetState(PigmanAnimatorState.Roar);
      AudioSingleton.PlaySound(AudioSingleton.Instance.clips.roar);
    phase = Phase.Roar;
  }

  public void OnRoarEnd() {
    animator.SetState(PigmanAnimatorState.Run);
    phase = Phase.Run;
  }

  public BtStatus StateUpdate() {
    if (waitTimeLeft > 0) {
      waitTimeLeft -= Time.deltaTime;
      if (waitTimeLeft <= 0) {
        animator.SetState(PigmanAnimatorState.Run);
        phase = Phase.Run;
      }
    } else if (phase == Phase.Run) {
      physics.RunInDirection(moveDirection);
      if (range.prepareAttackRange.HasAnyPlayer())
      {
        phase = Phase.None;
        animator.SetState(PigmanAnimatorState.Idle);
      }
    }
    return BtStatus.Running;
  }

  internal void SetTarget(PlayerUnitController unitToFollow) {
    Vector2 distance = unitToFollow.transform.position - transform.position;
    moveDirection = Direction2HHelpers.FromFloat(distance.x);
  }

  public bool IsRunning() =>
    phase == Phase.Run && !range.prepareAttackRange.HasAnyPlayer();

  public enum Phase {
    None,
    Wait,
    Stand,
    Roar,
    Run
  }
}
