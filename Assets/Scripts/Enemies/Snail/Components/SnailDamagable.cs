using Kite;
using UnityEngine;

public class SnailDamagable : EnemyDamagable, ISnailComponent
{
  public WorldTileFloat collisionForce;
  public int collisionDamage;
  public WorldTileFloat shielRecoilForce;
  public float maxYNormalPuffGuard;
  public float shieldRecoilTileForce;

  private SnailPhysics physics;
  private SnailController controller;
  private SnailRotation rotation;

  public override Vector2 ColliderCenter => controller.di.boxCollider.bounds.center;

  public override bool IsGuardingWorks(PlayerUnitController player)
  {
    if (physics.canPuff)
    {
      Vector2 collisionNormal = (player.transform.position - controller.transform.position).normalized;
      if (collisionNormal.y > maxYNormalPuffGuard)
      {
        return false;
      }
    }
    return true;
  }

  public override Vector2 GetGuardedRecoil(PlayerUnitController player) =>
    RecoilHelpers.GetRecoilFromTo(player.transform, controller.transform, shielRecoilForce);

  public override Vector2 GetCollisionRecoil(PlayerUnitController player) =>
    RecoilHelpers.GetRecoilFromTo(player.transform, controller.transform, collisionForce);

  public override int CollisionDamage => collisionDamage;

  public override void TakeDamageFrom(BoxCollider2D damageCollider, PlayerUnitController player) =>
    controller.di.destroyable.DestroyEnemyWithPoof();

  public override void OnPlayerDamageCollision(PlayerUnitController player)
  {
    if (!physics.canPuff)
      CheckForReverseOnCollisionWithPlayer(player);
  }

  public override void OnPlayerGuardCollision(PlayerUnitController player)
  {
    if (!physics.canPuff)
      CheckForReverseOnCollisionWithPlayer(player);
  }

  public void Inject(SnailController controller)
  {
    this.controller = controller;
    physics = controller.di.physics;
    rotation = controller.di.rotation;
  }

  private void CheckForReverseOnCollisionWithPlayer(PlayerUnitController player)
  {
    Bounds playerBounds = player.di.boxCollider.bounds;
    Bounds snailBounds = controller.di.boxCollider.bounds;
    Vector2 distance = playerBounds.center - snailBounds.center;

    bool performReverse = ShouldPerformReverse(distance);
    if (performReverse)
    {
      physics.ReverseSnail();
    }
  }

  private bool ShouldPerformReverse(Vector2 distance)
  {
    bool performReverse = false;
    switch (rotation.dir4)
    {
      case Direction4.Up:
        performReverse = distance.y > 0;
        break;
      case Direction4.Down:
        performReverse = distance.y < 0;
        break;
      case Direction4.Right:
        performReverse = distance.x > 0;
        break;
      case Direction4.Left:
        performReverse = distance.x < 0;
        break;
    }

    return performReverse;
  }
}