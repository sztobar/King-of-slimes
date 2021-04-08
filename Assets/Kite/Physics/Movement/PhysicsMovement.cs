using UnityEngine;
using System.Collections.Generic;
using System;

namespace Kite
{
  [Serializable]
  public class PhysicsMovement : MonoBehaviour
  {
    public new Rigidbody2D rigidbody;
    public BoxCollider2D boxCollider;
    public Transform moveTransform;
    public LayerMask layerMask;

    [HideInInspector]
    public CollisionMoveHitsFactory collisionMoveHitsFactory;
    
    [HideInInspector]
    public BoxRaycaster raycaster;
    
    [HideInInspector]
    public CollisionMoveController collisionMoveController;

    public Vector2 GetAllowedMovement(Vector2 wantsToMoveAmount, Vector2 deltaPosition = default, MoveMode mode = MoveMode.HorizontalFirst)
    {
      Vector2 movedAmount = Vector2.zero;
      foreach (MoveUnit moveUnit in mode.GetMoveUnits(wantsToMoveAmount))
      {
        movedAmount[moveUnit.axis] = moveUnit.dir * GetAllowedMovementAt(moveUnit.distance, moveUnit.dir, deltaPosition);
      }
      return movedAmount;
    }

    [Obsolete("Use GetAllowedMovement(float, Dir4[, Vector2])")]
    public float GetAllowedMovement(float distance, Direction4 direction, Vector2 deltaPosition = default)
    {
      return GetAllowedMovementAt(distance, direction, deltaPosition);
    }

    public float GetAllowedMovement(float distance, Dir4 direction, Vector2 deltaPosition = default)
    {
      return GetAllowedMovementAt(distance, direction, deltaPosition);
    }

    public Vector2 ForceMove(Vector2 wantsToMoveAmount, MoveMode mode = MoveMode.HorizontalFirst)
    {
      Vector2 effectiveMove = Vector2.zero;
      foreach (MoveUnit moveUnit in mode.GetMoveUnits(wantsToMoveAmount))
      {
        effectiveMove[moveUnit.axis] = ForceMove(moveUnit.distance, moveUnit.dir);
      }
      return effectiveMove;
    }

    [Obsolete]
    public float ForceMove(float distance, Direction4 direction)
    {
      if (!RaycastHelpers.IsValidDistance(distance))
      {
        return NoDistanceForceMove(direction);
      }

      Vector2 origin = rigidbody.position;
      IEnumerable<CollisionMoveHit> raycasterHits = GetCollisionMoveHits(distance, direction);

      foreach (CollisionMoveHit raycasterHit in raycasterHits)
      {
        collisionMoveController.ForceMoveInto(raycasterHit, distance);
      }
      rigidbody.position = origin + direction.ToVector2(distance);

      return distance;
    }

    public float ForceMove(float distance, Dir4 dir)
    {
      if (!RaycastHelpers.IsValidDistance(distance))
      {
        return NoDistanceForceMove(dir);
      }

      Vector2 origin = rigidbody.position;
      (RaycastHit2D[] raycastHits, int count) = raycaster.GetHits(distance, dir);
      IEnumerable<RaycastHit2D> uniqueHits = collisionMoveHitsFactory.GetUnique(raycastHits, count);

      foreach (RaycastHit2D hit in uniqueHits)
      {
        collisionMoveController.ForceMoveInto(hit, distance, dir);
      }

      Vector2 moveAmount = (Vector2)dir * distance;
      rigidbody.position = origin + moveAmount;

      return distance;
    }

    private float NoDistanceForceMove(Dir4 dir)
    {
      float rayDistance = RaycastHelpers.skinWidth;
      (RaycastHit2D[] raycastHits, int count) = raycaster.GetHits(rayDistance, dir);
      IEnumerable<RaycastHit2D> uniqueHits = collisionMoveHitsFactory.GetUnique(raycastHits, count);

      foreach (RaycastHit2D hit in uniqueHits)
      {
        collisionMoveController.ForceMoveInto(hit, 0, dir);
      }
      return 0;
    }

    [Obsolete]
    private float NoDistanceForceMove(Direction4 direction)
    {
      float distance = RaycastHelpers.skinWidth;
      IEnumerable<CollisionMoveHit> raycasterHits = GetCollisionMoveHits(distance, direction);

      foreach (CollisionMoveHit raycasterHit in raycasterHits)
      {
        collisionMoveController.ForceMoveInto(raycasterHit, 0);
      }
      return 0;
    }

    public Vector2 TryToMove(Vector2 wantsToMoveAmount, Vector2 deltaPosition = default, MoveMode mode = MoveMode.HorizontalFirst)
    {
      Vector2 movedAmount = deltaPosition;
      foreach (MoveUnit moveUnit in mode.GetMoveUnits(wantsToMoveAmount))
      {
        if (!RaycastHelpers.IsValidDistance(moveUnit.distance))
          continue;

        movedAmount[moveUnit.axis] = moveUnit.dir * TryToMove(moveUnit.distance, moveUnit.dir, deltaPosition);
      }
      return movedAmount - deltaPosition;
    }

    [Obsolete("Use TryToMove(float, Dir4[, Vector2])")]
    public float TryToMove(float distance, Direction4 direction, Vector2 deltaPosition = default)
    {
      float allowedMove = GetAllowedMovement(distance, direction, deltaPosition);
      float effectiveMove = ForceMove(allowedMove, direction);
      return effectiveMove;
    }

    public float TryToMove(float distance, Dir4 dir, Vector2 deltaPosition = default)
    {
      float allowedMove = GetAllowedMovement(distance, dir, deltaPosition);
      float effectiveMove = ForceMove(allowedMove, dir);
      return effectiveMove;
    }

    public bool CanMoveInDirection(Dir4 dir)
    {
      float testDistance = RaycastHelpers.skinWidth * 2;
      return GetAllowedMovement(testDistance, dir) > RaycastHelpers.skinWidth;
    }

    [Obsolete]
    public bool CanMoveInDirection(Direction4 direction)
    {
      float testDistance = RaycastHelpers.skinWidth * 2;
      return GetAllowedMovement(testDistance, direction) > RaycastHelpers.skinWidth;
    }

    [Obsolete]
    private float GetAllowedMovementAt_Obsolete(float distance, Direction4 direction, Vector2 deltaPosition = default)
    {
      if (!RaycastHelpers.IsValidDistance(distance))
        return 0;

      IEnumerable<CollisionMoveHit> raycasterHits = GetCollisionMoveHits(distance, direction, deltaPosition);
      float allowedDistance = distance;
      foreach (CollisionMoveHit raycasterHit in raycasterHits)
      {
        allowedDistance = collisionMoveController.GetAllowedMoveInto(raycasterHit, allowedDistance);
      }
      return allowedDistance;
    }

    [Obsolete]
    private float GetAllowedMovementAt(float distance, Direction4 direction, Vector2 deltaPosition = default)
    {
      Dir4 dir = Direction4Helpers.ToDir4(direction);
      return GetAllowedMovementAt(distance, dir, deltaPosition);
    }

    private float GetAllowedMovementAt(float distance, Dir4 dir, Vector2 deltaPosition = default)
    {
      if (!RaycastHelpers.IsValidDistance(distance))
        return 0;

      (RaycastHit2D[] raycastHits, int count) = raycaster.GetHits(distance, dir, deltaPosition);
      IEnumerable<RaycastHit2D> uniqueHits = collisionMoveHitsFactory.GetUnique(raycastHits, count);
      float allowedDistance = distance;
      foreach (RaycastHit2D hit in uniqueHits)
      {
        allowedDistance = collisionMoveController.GetAllowedMoveInto(hit, allowedDistance, dir);
      }
      return allowedDistance;
    }

    [Obsolete]
    public IEnumerable<CollisionMoveHit> GetCollisionMoveHits(float distance, Direction4 direction, Vector2 deltaPosition = default)
    {
      (RaycastHit2D[] raycastHits, int count) = raycaster.GetHits(distance, direction, deltaPosition);
      return collisionMoveHitsFactory.GetUniqueRaycasterHits(raycastHits, count, direction);
    }

    private void Awake()
    {
      raycaster = BoxRaycaster.Create(boxCollider, layerMask);
      moveTransform = !!moveTransform ? moveTransform : transform;
      collisionMoveController = CollisionMoveController.Create(moveTransform, this);
      collisionMoveHitsFactory = CollisionMoveHitsFactory.Create(boxCollider);
    }
  }
}