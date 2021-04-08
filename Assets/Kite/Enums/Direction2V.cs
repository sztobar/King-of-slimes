using UnityEngine;

namespace Kite {

  public enum Direction2V {
    Down = -1,
    Up = 1
  }

  public static class Direction2VHelpers {

    public static Direction2V Flip(this Direction2V value) =>
      value == Direction2V.Down ? Direction2V.Up : Direction2V.Down;

    public static float ToFloat(this Direction2V value) =>
      value == Direction2V.Down ? -1 : 1;

    public static Vector2 ToVector2(this Direction2V value) =>
      value == Direction2V.Down ? Vector2.down : Vector2.up;

    public static Vector3 ToVector3(this Direction2V value) =>
      value == Direction2V.Down ? Vector3.down : Vector3.up;

    public static Direction4 ToDir4(this Direction2V dir2V) =>
      dir2V == Direction2V.Up ? Direction4.Up : Direction4.Down;
  }
}