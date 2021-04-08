using UnityEngine;

namespace Kite
{
  public class Dir4 : ScriptableObject
  {
    public static Dir4 up, right, down, left;

    public static Dir4[] GetList() => new Dir4[] { up, right, down, left };
    public static void OnSettings(KiteSettings settings) =>
      (up, right, down, left) = (settings.upDir4, settings.rightDir4, settings.downDir4, settings.leftDir4);

    public static implicit operator Vector2(Dir4 dir) => new Vector2(dir.x, dir.y);
    public static implicit operator float(Dir4 dir) => dir.x != 0 ? dir.x : dir.y;
    public static Dir4 FromXFloat(float value) => value > 0 ? right : left;
    public static Dir4 FromYFloat(float value) => value > 0 ? up : down;
    public static Dir4 FromFloat(float value, int axis) => axis == VectorAxis.x ? FromXFloat(value) : FromYFloat(value);

    public static Dir4 operator -(Dir4 dir)
    {
      if (dir == up)    return down;
      if (dir == right) return left;
      if (dir == down)  return up;
      if (dir == left)  return right;
      return null;
    }

    public string identifier;
    public int x;
    public int y;

    public int Axis => x != 0 ? 0 : 1;
    public bool IsAxis(int axis) => Axis == axis;

    public override string ToString() => identifier;
  }
}