using System;
using UnityEngine;

namespace Kite
{
  public static class Vector2Helpers
  {
    public static Direction4 ToDirection4Horizontal(this Vector2 vector) =>
      vector.x >= 0 ? Direction4.Right : Direction4.Left;

    public static Direction4 ToDirection4Vertical(this Vector2 vector) =>
      vector.y >= 0 ? Direction4.Up : Direction4.Down;

    public static Direction4 ToDirection4InAxis(this Vector2 vector, int axis) =>
      axis == 0 ? ToDirection4Horizontal(vector) : ToDirection4Vertical(vector);

    public static bool HasValueInDirection(this Vector2 vector, Direction4 direction) =>
      ToDirection4InAxis(vector, direction.ToVector2Index()) == direction;

    public static Direction4 NormalToDirection4(this Vector2 normal)
    {
      if (normal == Vector2.up)
      {
        return Direction4.Up;
      }
      else if (normal == Vector2.down)
      {
        return Direction4.Down;
      }
      else if (normal == Vector2.left)
      {
        return Direction4.Left;
      }
      else if (normal == Vector2.right)
      {
        return Direction4.Right;
      }
      throw new Exception($"Cannot convert non-normal Vector2 {normal} to Direction4");
    }

    public static Vector2 AxisVector(int axis, float v)
    {
      Vector2 newVector = Vector2.zero;
      newVector[axis] = v;
      return newVector;
    }

    public static Vector2 DegreeToVector2(float degree)
    {
      float radian = degree * Mathf.Deg2Rad;
      return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 QuaternionToVector2(Quaternion quaternion)
    {
      Vector3 eulerAngles = quaternion.eulerAngles;
      float y = eulerAngles.y;
      float z = eulerAngles.z;
      if (Mathf.RoundToInt(y) == 0)
        return DegreeToVector2(z);
      else
        return DegreeToVector2(y - z);
    }

    /// <summary>
    /// Rotates counter clockwise
    /// </summary>
    public static Vector2 Rotate(this Vector2 vector, float degrees) =>
      Quaternion.Euler(0, 0, degrees) * vector;

    public static float GetValue(this Vector2 vector, Orientation orientation) =>
      vector[orientation.ToVector2Index()];
  }
}