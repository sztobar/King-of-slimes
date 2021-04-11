using System.Collections.Generic;
using UnityEngine;

namespace Kite {
  public interface IPhysicsMovement {

    Vector2 GetAllowedMovement(Vector2 wantsToMoveAmount, Vector2 deltaPosition = default, MoveMode mode = MoveMode.HorizontalFirst);
    float GetAllowedMovement(float distance, Direction4 direction, Vector2 deltaPosition = default);

    Vector2 ForceMove(Vector2 wantsToMoveAmount, MoveMode mode = MoveMode.HorizontalFirst);
    float ForceMove(float distance, Direction4 direction);

    Vector2 TryToMove(Vector2 wantsToMoveAmount, Vector2 deltaPosition = default, MoveMode mode = MoveMode.HorizontalFirst);
    float TryToMove(float distance, Direction4 direction, Vector2 deltaPosition = default);

    IEnumerable<CollisionMoveHit> GetCollisionMoveHits(float distance, Direction4 direction, Vector2 deltaPosition = default);
  }
}