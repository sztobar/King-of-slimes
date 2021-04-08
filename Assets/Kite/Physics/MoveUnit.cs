using UnityEngine;
using System.Collections;

namespace Kite
{
  public struct MoveUnit
  {
    public float distance;
    public Dir4 dir;
    public int axis;

    public static MoveUnit FromVector2(Vector2 vector, int axis)
    {
      float distance = Mathf.Abs(vector[axis]);
      Dir4 dir = Dir4.FromFloat(vector[axis], axis);
      return new MoveUnit { distance = distance, dir = dir, axis = axis };
    }
  }
}