using UnityEngine;

namespace Kite {
  public static class PixelHelpers {

    public static float Floor(float value) =>
      value > 0
        ? Mathf.Floor(value)
        : Mathf.Ceil(value);

    public static Vector2 Floor(Vector2 value) =>
      new Vector2(
        Floor(value.x),
        Floor(value.y)
      );
  }
}