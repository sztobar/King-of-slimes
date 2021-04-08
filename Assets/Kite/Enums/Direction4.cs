using UnityEngine;
using System;
using System.Collections.Generic;

namespace Kite
{
  public enum Direction4
  {
    Left,
    Right,
    Up,
    Down
  }

  public static class Direction4Helpers
  {
    public static Quaternion ToEulerQuaternion(this Direction4 direction)
    {
      switch (direction)
      {
        case Direction4.Up:
          return Quaternion.Euler(0, 0, 0);
        case Direction4.Left:
          return Quaternion.Euler(0, 0, 90);
        case Direction4.Down:
          return Quaternion.Euler(0, 0, 180);
        case Direction4.Right:
          return Quaternion.Euler(0, 0, 270);
        default:
          Debug.LogError($"Unhandled enum value: {direction}");
          return Quaternion.Euler(0, 0, 0);
      }
    }

    public static Direction4 RotateClockwise(this Direction4 dir)
    {
      switch (dir)
      {
        case Direction4.Up:
          return Direction4.Right;
        case Direction4.Right:
          return Direction4.Down;
        case Direction4.Down:
          return Direction4.Left;
        case Direction4.Left:
          return Direction4.Up;
        default:
          Debug.LogError($"Unhandled enum value: {dir}");
          return Direction4.Up;
      }
    }

    internal static Dir4 ToDir4(Direction4 dir)
    {
      switch (dir)
      {
        case Direction4.Up:
          return Dir4.up;
        case Direction4.Left:
          return Dir4.left;
        case Direction4.Down:
          return Dir4.down;
        case Direction4.Right:
          return Dir4.right;
        default:
          Debug.LogError($"Unhandled enum value: {dir}");
          return Dir4.up;
      }
    }

    public static Direction4 RotateCounterClockwise(this Direction4 dir)
    {
      switch (dir)
      {
        case Direction4.Up:
          return Direction4.Left;
        case Direction4.Left:
          return Direction4.Down;
        case Direction4.Down:
          return Direction4.Right;
        case Direction4.Right:
          return Direction4.Up;
        default:
          Debug.LogError($"Unhandled enum value: {dir}");
          return Direction4.Up;
      }
    }

    public static Vector3 ToVector3(this Direction4 direction, float length = 1.0f)
    {
      switch (direction)
      {
        case Direction4.Down:
          return new Vector3(0.0f, -length);
        case Direction4.Left:
          return new Vector3(-length, 0.0f);
        case Direction4.Right:
          return new Vector3(length, 0.0f);
        case Direction4.Up:
          return new Vector3(0.0f, length);
        default:
          Debug.LogError($"Unhandled enum value: {direction}");
          return new Vector3(0.0f, 0.0f);
      }
    }

    public static Vector2 ToVector2(this Direction4 direction, float length = 1.0f)
    {
      switch (direction)
      {
        case Direction4.Down:
          return new Vector2(0.0f, -length);
        case Direction4.Left:
          return new Vector2(-length, 0.0f);
        case Direction4.Right:
          return new Vector2(length, 0.0f);
        case Direction4.Up:
          return new Vector2(0.0f, length);
        default:
          Debug.LogError($"Unhandled enum value: {direction}");
          return new Vector2(0.0f, 0.0f);
      }
    }

    public static Direction2H ToDirection2H(this Direction4 direction)
    {
      if (direction.IsVertical())
      {
        throw new Exception("Passed vertical direction4 in place of horizontal");
      }
      return direction == Direction4.Left ? Direction2H.Left : Direction2H.Right;
    }

    public static Direction2V ToDirection2V(this Direction4 direction)
    {
      if (direction.IsHorizontal())
      {
        throw new Exception("Passed horizontal direction4 in place of vertical");
      }
      return direction == Direction4.Up ? Direction2V.Up : Direction2V.Down;
    }

    public static float Sign(this Direction4 direction)
    {
      switch (direction)
      {
        case Direction4.Down:
        case Direction4.Left:
          return -1f;
        case Direction4.Right:
        case Direction4.Up:
          return 1f;
        default:
          Debug.LogError($"Unhandled enum value: {direction}");
          return 0f;
      }
    }

    public static Direction4 Opposite(this Direction4 direction)
    {
      switch (direction)
      {
        case Direction4.Down:
          return Direction4.Up;
        case Direction4.Left:
          return Direction4.Right;
        case Direction4.Right:
          return Direction4.Left;
        case Direction4.Up:
          return Direction4.Down;
        default:
          Debug.LogError($"Unhandled enum value: {direction}");
          return Direction4.Up;
      }
    }

    public static int ToVector2Index(this Direction4 direction) =>
      IsHorizontal(direction) ? 0 : 1;

    public static Orientation ToOrientation(this Direction4 direction) =>
      direction.IsHorizontal() ? Orientation.Horizontal : Orientation.Vertical;

    public static Direction4 FromFloatHorizontal(float sign) =>
      sign > 0 ? Direction4.Right : Direction4.Left;

    public static Direction4 FromFloatVertical(float sign) =>
      sign > 0 ? Direction4.Up : Direction4.Down;

    public static bool IsHorizontal(this Direction4 direction) =>
      direction == Direction4.Left || direction == Direction4.Right;

    public static bool IsVertical(this Direction4 direction) =>
      direction == Direction4.Up || direction == Direction4.Down;

    public static bool IsOrientation(this Direction4 direction, Orientation orientation) =>
      direction.ToOrientation() == orientation;

    public static bool IsPerpendicular(this Direction4 direction, Orientation orientation) =>
      direction.ToOrientation() != orientation;

    public static IEnumerable<Direction4> GetEnumerable()
    {
      yield return Direction4.Up;
      yield return Direction4.Right;
      yield return Direction4.Down;
      yield return Direction4.Left;
    }
  }
}