using NaughtyAttributes;
using UnityEngine;

public class PigmanRoarState : MonoBehaviour, IPigmanState {

  private ScriptablePigman data;
  private PigmanAnimator animator;
  private PigmanAnimationEvents eventsListener;

  private float timeLeftToRoar;

  public void StateExit() {
    eventsListener.OnRoarEnd -= OnRoarEnd;
  }

  public void Inject(PigmanController controller) {
    animator = controller.di.animator;
    eventsListener = controller.di.animationEvents;
    data = controller.data;
  }

  public void StateStart() {
    eventsListener.OnRoarEnd += OnRoarEnd;
    timeLeftToRoar = 0;
    animator.SetState(PigmanAnimatorState.Roar);
    AudioSingleton.PlaySound(AudioSingleton.Instance.clips.roar);
  }

  public void OnRoarEnd() {
    timeLeftToRoar = RandomRange.FromVector(data.roarsInterval);
    animator.SetState(PigmanAnimatorState.Idle);
  }

  public BtStatus StateUpdate() {
    if (timeLeftToRoar > 0) {
      timeLeftToRoar -= Time.deltaTime;
      if (timeLeftToRoar <= 0) {
        animator.SetState(PigmanAnimatorState.Roar);
      }
    }
    return BtStatus.Running;
  }
}
