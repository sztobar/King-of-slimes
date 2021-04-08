using UnityEngine;
using System.Collections;

namespace Kite {
  public class PlatformEdgeCollidable : MonoBehaviour, ICollidable {

    public void OnPhysicsMoveInto(Transform moving, float collideDistance, Direction4 direction, Vector2 hitPoint) {}

    public float GetAllowedMoveInto(Transform wantsToMove, float collideDistance, Direction4 direction, Vector2 hitPoint) =>
      PlatformCollidableHelpers.GetEdgeColliderAllowedMovementInto(wantsToMove, collideDistance, direction);
  }
}