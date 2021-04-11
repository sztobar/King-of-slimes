using Kite;
using System.Collections;
using UnityEngine;

public class RockbatDamagable : EnemyDamagable, IRockbatComponent
{
  public WorldTileFloat collisionForce;
  public WorldTileFloat shieldRecoilForce;
  public int collisionDamage;

  private Rockbat controller;

  public override Vector2 ColliderCenter => controller.cameraCollider.collider.bounds.center;

  public override Vector2 GetGuardedRecoil(PlayerUnitController player)
  {
    SetHitState(player);
    //controller.DestroyWithPoof();
    if (shieldRecoilForce == 0)
    {
      return Vector2.zero;
    }
    return RecoilHelpers.GetRecoilFromTo(player.transform, controller.transform, shieldRecoilForce);
  }

  private void SetHitState(PlayerUnitController player)
  {
    controller.fsm.hit.Direction = RecoilHelpers.GetRecoilFromTo(controller.transform, player.transform, 1);
    controller.fsm.PlayHit();
  }

  public override Vector2 GetCollisionRecoil(PlayerUnitController player) =>
    RecoilHelpers.GetRecoilFromTo(player.transform, controller.transform, shieldRecoilForce);

  public override int CollisionDamage => collisionDamage;

  public void Inject(Rockbat rockbat)
  {
    controller = rockbat;
  }

  public override void TakeDamageFrom(BoxCollider2D damageCollider, PlayerUnitController player)
  {
    controller.destroyable.DestroyEnemyWithPoof();
  }
}
