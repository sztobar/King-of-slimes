using System;
using System.Collections;
using UnityEngine;

public class SnailDi : MonoBehaviour, ISnailComponent
{
  public SnailPhysics physics;
  public SnailRaycaster raycaster;
  public SnailRotation rotation;
  public SnailAnimator animator;
  public SnailPuffCollider puffCollider;
  [Obsolete] public SnailDamagable damagable;
  public EnemyDestroyable destroyable;
  public new SnailCollider collider;

  [Obsolete] public BoxCollider2D boxCollider;

  public void Inject(SnailController controller)
  {
    foreach (ISnailComponent component in GetComponents())
      component.Inject(controller);
  }

  public ISnailComponent[] GetComponents() =>
    new ISnailComponent[] {
      physics,
      raycaster,
      rotation,
      animator,
      puffCollider,
      collider,
      damagable
    };
}