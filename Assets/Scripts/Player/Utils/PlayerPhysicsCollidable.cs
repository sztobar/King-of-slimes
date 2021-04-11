using System.Collections;
using UnityEngine;
using Kite;
using System;

public class PlayerPhysicsCollidable : PhysicsCollidable, IPlayerUnitComponent
{
  private PlayerPhysics physics;
  private PlayerDamageModule damage;

  public override void OnMoveInto(PhysicsMove move)
  {
    if (IsCrushingComponent(move.moving) && move.dir == Dir4.down)
    {
      float allowedMove = physics.movement.GetAllowedMovement(move.collideDistance, move.dir);
      if (allowedMove < move.collideDistance)
      {
        damage.TakeFullDamage();
      }
    }
  }

  public override float GetAllowedMoveInto(PhysicsMove move)
  {
    if (IsCrushingComponent(move.moving))
    {
      float allowedMove = physics.movement.GetAllowedMovement(move.collideDistance, move.dir);
      if (move.dir == Dir4.down && allowedMove < move.collideDistance)
      {
        // begin crush
        return move.collideDistance;
      }
      return 0;
    }
    return 0;
  }

  // TODO: Introduce crushable
  private bool IsCrushingComponent(PhysicsMovement wantsToMove)
  {
    return wantsToMove.GetComponent<PushBlock>() || wantsToMove.GetComponent<UnlockableDoor>();
  }

  public void Inject(PlayerUnitDI di)
  {
    physics = di.physics;
    damage = di.damage;
  }
}