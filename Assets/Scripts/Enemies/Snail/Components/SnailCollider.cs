using Kite;
using System.Collections;
using UnityEngine;

public class SnailCollider : EnemyCollider, ISnailComponent
{
  SnailController controller;

  public void Inject(SnailController snail) => controller = snail;

  protected override void OnCollisionAttack(PlayerUnitController player)
  {
    if (!controller.di.physics.canPuff)
      CheckForReverseOnCollisionWithPlayer(player);
  }

  protected override void OnGuarded(PlayerUnitController player)
  {
    if (!controller.di.physics.canPuff)
      CheckForReverseOnCollisionWithPlayer(player);
  }

  protected override void OnStomped(PlayerUnitController player) =>
    controller.di.destroyable.DestroyEnemyWithPoof();

  protected override void OnTakeDamage(PlayerUnitController player) =>
    controller.di.destroyable.DestroyEnemyWithPoof();

  private void CheckForReverseOnCollisionWithPlayer(PlayerUnitController player)
  {
    Bounds playerBounds = player.di.boxCollider.bounds;
    Bounds snailBounds = boxCollider.bounds;
    Vector2 distance = playerBounds.center - snailBounds.center;

    bool performReverse = ShouldPerformReverse(distance);
    if (performReverse)
    {
      controller.di.physics.ReverseSnail();
    }
  }

  private bool ShouldPerformReverse(Vector2 distance)
  {
    bool performReverse = false;
    switch (controller.di.rotation.dir4)
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