using System;
using UnityEngine;

namespace Kite
{
  public class GridPlatformCollidable : PhysicsCollidable, ICollidable
  {
    public PlatformEffector effector;

    [Obsolete]
    public float GetAllowedMoveInto(Transform wantsToMove, float collideDistance, Direction4 direction, Vector2 hitPoint)
    {
      return PlatformCollidableHelpers.GetGridAllowedMovementInto(wantsToMove, collideDistance, direction, hitPoint);
    }

    [Obsolete]
    public void OnPhysicsMoveInto(Transform moving, float collideDistance, Direction4 direction, Vector2 hitPoint) { }

    public override float GetAllowedMoveInto(PhysicsMove move) =>
      PlatformCollidableHelpers.GetGridAllowedMovementInto(move, effector);

    public override void OnMoveInto(PhysicsMove move) { }
  }
}