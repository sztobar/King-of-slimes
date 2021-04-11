using UnityEngine;
using System.Collections.Generic;

namespace Kite {
  public interface ICollisionMoveHitsFactory {
    IEnumerable<CollisionMoveHit> GetUniqueRaycasterHits(RaycastHit2D[] hits, int count, Direction4 direction);
  }
}