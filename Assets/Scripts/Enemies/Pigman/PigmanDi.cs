using System.Collections;
using UnityEngine;

public class PigmanDi : MonoBehaviour {

  public PigmanRange range;
  public PigmanStateMachine stateMachine;
  public PigmanAnimator animator;
  public PigmanPhysics physics;
  public PigmanAnimationEvents animationEvents;
  public PigmanAttackCollider attackCollider;
  public PigmanDamagable damagable;

  public void Init(PigmanController controller) {
    foreach(IPigmanComponent component in GetPigmanComponents()) {
      component.Inject(controller);
    }
  }

  private IPigmanComponent[] GetPigmanComponents() =>
    new IPigmanComponent[] {
      range,
      stateMachine,
      animator,
      physics,
      animationEvents,
      attackCollider,
      damagable
    };
}
