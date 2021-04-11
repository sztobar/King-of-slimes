using System;
using System.Collections;
using UnityEngine;

using TotemAnimation = UnityAnimatorStates.Totem;

public class Totem : EnemyDamagable
{
  public new Collider2D collider;
  public PoisonField poisonField;
  public EasyAnimator animator;
  private bool destroyed;

  private void Awake()
  {
    animator.OnAnimationEnd += OnAnimationEnd;
  }

  public override Vector2 ColliderCenter => collider.bounds.center;
  public override Vector2 GetGuardedRecoil(PlayerUnitController player) => Vector2.zero;

  public override Vector2 GetCollisionRecoil(PlayerUnitController player) => Vector2.zero;

  public override int CollisionDamage => 0;

  public override void TakeDamageFrom(BoxCollider2D damageCollider, PlayerUnitController player)
  {
    if (destroyed)
      return;

    destroyed = true;
    animator.Play(TotemAnimation.Totem_destroy);
    Destroy(poisonField.gameObject);
  }

  private void OnAnimationEnd(int animatorStateHash)
  {
    if (animatorStateHash == TotemAnimation.Totem_destroy)
      Destroy(gameObject);
  }

  public override bool DealDamageOnCollision() => false;
}