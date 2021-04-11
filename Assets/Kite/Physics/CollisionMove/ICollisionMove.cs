using UnityEngine;
using System.Collections;

namespace Kite {
  public interface ICollisionMove {
    float GetAllowedMoveInto(CollisionMoveHit hit, float distance);
    void ForceMoveInto(CollisionMoveHit raycasterHit, float distance);
  }
}