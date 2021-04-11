using Kite;
using System.Collections;
using UnityEngine;

public class RockbatDi : MonoBehaviour {

  public RockbatAnimator animator;
  public RockbatCameraSegmentCollider attackCollider;
  public RockbatDamagable damagable;
  public RockbatPhysics physics;
  public RockbatRange range;
  public HorizontalFlipComponent flip;
  public DeathPoof deathPoof;
  public BoxCollider2D boxCollider;

  //public void Inject(RockbatController controller) {
  //  foreach(IRockbatComponent component in GetComponents()) {
  //    component.Inject(controller);
  //  }
  //}

  private IRockbatComponent[] GetComponents() =>
    new IRockbatComponent[] {
      animator,
      attackCollider,
      damagable,
      physics,
      range
    };
}
