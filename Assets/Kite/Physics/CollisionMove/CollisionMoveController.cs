using System;
using UnityEngine;

namespace Kite
{
  public class CollisionMoveController : ScriptableObject
  {
    public Transform transform;
    public PhysicsMovement movement;

    public static CollisionMoveController Create(Transform transform, PhysicsMovement movement)
    {
      CollisionMoveController instance = CreateInstance<CollisionMoveController>();
      instance.transform = transform;
      instance.movement = movement;
      return instance;
    }

    [Obsolete("Use GetAllowedMoveInto(RaycastHit2D, float, Dir4)")]
    public float GetAllowedMoveInto(CollisionMoveHit hit, float wantToMove)
    {
      if (!hit.isTilemap && wantToMove <= hit.distanceToHit)
      {
        return wantToMove;
      }
      ICollidable collidable = CollidableHelpers.GetCollidable(hit.transform);

      float collideDistance = hit.GetCollideDistance(wantToMove);
      float allowedCollideDistance = collidable.GetAllowedMoveInto(transform, collideDistance, hit.rayDirection, hit.point);
      float allowedDistance = allowedCollideDistance + hit.distanceToHit;

      return Mathf.Clamp(allowedDistance, 0, wantToMove);
    }

    public float GetAllowedMoveInto(RaycastHit2D hit, float wantsToMove, Dir4 dir)
    {
      if (wantsToMove <= hit.distance && !TilemapHelpers.IsColliderTilemap(hit.collider))
      {
        return wantsToMove;
      }
      float allowedCollideDistance = 0;
      PhysicsCollidable moveable = hit.collider.GetComponent<PhysicsCollidable>();
      if (moveable)
      {
        float collideDistance = wantsToMove - hit.distance;
        PhysicsMove physicsMove = new PhysicsMove(hit, collideDistance, dir, movement);
        allowedCollideDistance = moveable.GetAllowedMoveInto(physicsMove);
      }
      float allowedDistance = allowedCollideDistance + hit.distance;

      return Mathf.Clamp(allowedDistance, 0, wantsToMove);
    }

    public void ForceMoveInto(RaycastHit2D hit, float moveAmount, Dir4 dir)
    {
      float collideDistance = moveAmount - hit.distance;
      PhysicsCollidable moveable = hit.collider.GetComponent<PhysicsCollidable>();
      if (moveable)
      {
        PhysicsMove physicsMove = new PhysicsMove(hit, collideDistance, dir, movement);
        moveable.OnMoveInto(physicsMove);
      }
    }

    [Obsolete]
    public void ForceMoveInto(CollisionMoveHit hit, float moveAmount)
    {
      ICollidable collidable = CollidableHelpers.GetCollidable(hit.transform);
      float collideDistance = hit.GetCollideDistance(moveAmount);
      collidable.OnPhysicsMoveInto(transform, collideDistance, hit.rayDirection, hit.point);
    }
  }
}