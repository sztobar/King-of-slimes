using System;
using UnityEngine;

namespace Kite
{
  public enum Direction2H
  {
    Right = 1,
    Left = -1,
  }

  public static class Direction2HHelpers
  {

    public static Direction2H Flip(this Direction2H value) =>
      value == Direction2H.Left ? Direction2H.Right : Direction2H.Left;

    public static float ToFloat(this Direction2H value) =>
      value == Direction2H.Left ? -1 : 1;

    public static Vector2 ToVector2(this Direction2H value, float distance = 1f) =>
      (value == Direction2H.Left ? Vector2.left : Vector2.right) * distance;

    public static Vector3 ToVector3(this Direction2H value) =>
      value == Direction2H.Left ? Vector3.left : Vector3.right;

    public static Direction2H FromFloat(float value) =>
      value >= 0 ? Direction2H.Right : Direction2H.Left;

    public static Direction2H Random()
    {
      float value = UnityEngine.Random.value * 2 - 1;
      return FromFloat(value);
    }
  }
}