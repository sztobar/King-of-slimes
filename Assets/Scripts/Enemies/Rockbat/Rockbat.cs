using Kite;
using System.Collections;
using UnityEngine;

public class Rockbat : MonoBehaviour
{
  public RockbatStateMachine fsm;
  public RockbatCameraSegmentCollider cameraCollider;
  public new RockbatCollider collider;
  public RockbatAnimator animator;
  public RockbatPhysics physics;
  public RockbatDamagable damagable;
  public RockbatRange range;
  public EnemyDestroyable destroyable;

  public DeathPoof deathPoof;
  public HorizontalFlipComponent flip;

  private void Awake()
  {
    foreach (var component in GetInjectables())
      component.Inject(this);
  }

  private IRockbatComponent[] GetInjectables() =>
    new IRockbatComponent[] {
      fsm,
      collider,
      cameraCollider,
      animator,
      physics,
      damagable,
      range
    };
}