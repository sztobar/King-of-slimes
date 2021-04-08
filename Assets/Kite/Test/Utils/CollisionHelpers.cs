using UnityEngine;
using System.Collections;

namespace KiteEditTests {
  public static class CollisionHelpers {

    public const int layer = 10;

    public static int GetLayerMask() =>
      Physics2D.GetLayerCollisionMask(layer);
  }
}