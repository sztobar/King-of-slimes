using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kite {
  public class PhysicsCarry : MonoBehaviour, ICollidable {

    private readonly HashSet<Transform> carriedObjects = new HashSet<Transform>();

    public void OnPhysicsMoveInto(Transform moving, float collideDistance, Direction4 direction, Vector2 hitPoint) {
      CheckIfTransformIsCarried(moving, collideDistance, direction);
    }

    public float GetAllowedMoveInto(Transform wantsToMove, float collideDistance, Direction4 direction, Vector2 hitPoint) {
      return 0;
    }

    public HashSet<Transform> GetCarriedTransforms() {
      return carriedObjects;
    }

    public void ClearCarriedTransforms() {
      carriedObjects.Clear();
    }

    public void CheckIfTransformIsCarried(Transform moving, float collideDistance, Direction4 direction) {
      if (direction == Direction4.Down) {
        carriedObjects.Add(moving);
      }
    }
  }
}