using UnityEngine;
using System.Collections.Generic;

namespace Kite {
  public interface IRaycaster {
    (RaycastHit2D[], int) GetHits(float distance, Direction4 direction, Vector2 deltaPosition = default);
    void InvalidateBounds();
  }
}