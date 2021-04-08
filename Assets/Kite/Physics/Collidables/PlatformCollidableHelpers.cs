using UnityEngine;
using System.Collections;
using System;

namespace Kite
{
  public static class PlatformCollidableHelpers
  {
    private static readonly float BOX_TOP_TOLERANCE = 0.1f;

    public static float GetGridAllowedMovementInto(PhysicsMove move, PlatformEffector effector)
    {
      if (move.dir == Dir4.down)
      {
        float roundedPointY = Mathf.Round(move.hit.point.y * 100f) / 100f;
        bool isTileCoord = roundedPointY % 16f == 0;
        if (isTileCoord)
        {
          StandEffectable effectable = move.moving.GetComponent<StandEffectable>();
          if (effectable)
          {
            effector.AddEffectable(effectable);
            if (effectable.CanSkipPlatform())
            {
              return move.collideDistance;
            }
          }
          return 0;
        }
      }
      return move.collideDistance;
    }

    [Obsolete]
    public static float GetGridAllowedMovementInto(Transform wantsToMove, float collideDistance, Direction4 direction, Vector2 hitPoint)
    {
      if (direction == Direction4.Down && !CanSkipPlatform(wantsToMove))
      {
        float roundedPointY = (float)Mathf.Round(hitPoint.y * 100f) / 100f;
        if (roundedPointY % 16f == 0)
        {
          return 0;
        }
      }
      return collideDistance;
    }

    public static float GetBoxColliderAllowedMovementInto(Transform wantsToMove, float collideDistance, Direction4 direction, Vector2 hitPoint, BoxCollider2D boxCollider)
    {
      if (direction == Direction4.Down && CollidesWithBoxTop(hitPoint, boxCollider) && !CanSkipPlatform(wantsToMove))
      {
        return 0;
      }
      return collideDistance;
    }

    [Obsolete]
    public static float GetEdgeColliderAllowedMovementInto(Transform wantsToMove, float collideDistance, Direction4 direction)
    {
      if (direction == Direction4.Down && !CanSkipPlatform(wantsToMove))
      {
        return 0;
      }
      return collideDistance;
    }

    public static float GetEdgeColliderAllowedMovementInto(PhysicsMove move)
    {
      if (move.dir == Dir4.down && !CanSkipPlatform(move.moving))
      {
        return 0;
      }
      return move.collideDistance;
    }

    private static bool CollidesWithBoxTop(Vector2 hitPoint, BoxCollider2D boxCollider)
    {
      return boxCollider.bounds.max.y - hitPoint.y < BOX_TOP_TOLERANCE;
    }

    [Obsolete]
    private static bool CanSkipPlatform(Transform transform)
    {
      PlatformSkippable skipabble = transform.GetComponent<PlatformSkippable>();
      return skipabble && skipabble.CanSkip;
    }

    private static bool CanSkipPlatform(PhysicsMovement movement)
    {
      PlatformSkippable skipabble = movement.GetComponent<PlatformSkippable>();
      return skipabble && skipabble.CanSkip;
    }
  }
}