using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kite {
  public interface IPhysicsCarry {
    void CheckIfTransformIsCarried(Transform moving, float collideDistance, Direction4 direction);
    HashSet<Transform> GetCarriedTransforms();
    void ClearCarriedTransforms();
  }
}