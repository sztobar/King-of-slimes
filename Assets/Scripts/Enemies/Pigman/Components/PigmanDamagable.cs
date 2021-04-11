using Kite;
using System;
using System.Collections;
using UnityEngine;

public class PigmanDamagable : EnemyDamagable, IPigmanComponent
{
  [Header("Debug")]
  public int currentHp;

  private PigmanAnimator animator;
  private PigmanStateMachine stateMachine;
  private PigmanController controller;
  private ScriptablePigman data;

  private Vector2 afterParryRecoil;
  private bool hasAfterParryRecoil;

  // TODO: move boxCollider up to PigmanPhysics
  public override Vector2 ColliderCenter => controller.di.physics.physics.movement.boxCollider.bounds.center;

  public override bool HasRecoilAfterDamage() => hasAfterParryRecoil;

  public override Vector2 GetRecoilAfterDamage() => afterParryRecoil;

  public override Vector2 GetGuardedRecoil(PlayerUnitController player)
  {
    float tileForce = player.di.stats.IsFullAssembly
      ? data.guardFullAssemblyRecoilTileForce
      : data.guardPartialAssemblyRecoilTileForce;
    return RecoilHelpers.GetRecoilFromTo(player.transform, controller.transform, tileForce);
  }

  public override Vector2 GetCollisionRecoil(PlayerUnitController player) =>
    RecoilHelpers.GetRecoilFromTo(player.transform, controller.transform, controller.data.collisionForce);

  public override int CollisionDamage => controller.data.collisionDamage;

  public override void TakeDamageFrom(BoxCollider2D damageCollider, PlayerUnitController player)
  {
    if (animator.IsState(PigmanAnimatorState.Stagger))
    {
      hasAfterParryRecoil = false;
      currentHp--;
      if (currentHp == 0)
      {
        Die();
      }
      else
      {
        TakeHit();
      }
    }
    else if (animator.IsState(PigmanAnimatorState.Hit))
    {
      // do nothing
    }
    else
    {
      hasAfterParryRecoil = true;
      float tileForce = player.di.stats.IsFullAssembly
        ? data.parryFullAssemblyRecoilTileForce
        : data.parryPartialAssemblyRecoilTileForce;
      afterParryRecoil = RecoilHelpers.GetRecoilFromTo(player.transform, controller.transform, tileForce);
      stateMachine.attack.StartParry();
    }
  }

  private void TakeHit()
  {
    stateMachine.hit.StartHit();
  }

  private void Die()
  {
    controller.RemoveGameObject();
  }

  public void Inject(PigmanController controller)
  {
    this.controller = controller;
    stateMachine = controller.di.stateMachine;
    animator = controller.di.animator;
    currentHp = controller.data.hp;
    data = controller.data;
  }
}