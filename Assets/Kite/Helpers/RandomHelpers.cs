using System.Collections;
using UnityEngine;

namespace Kite
{
  public static class RandomHelpers 
  {
    public static int OneOrMinusOne() =>
      Random.value > 0.5f ? 1 : -1;

    public static float Range(Vector2 v) =>
      Random.Range(v.x, v.y);
  }
}