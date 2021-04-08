using System;
using System.Collections;
using UnityEngine;

namespace Kite
{
  public class DirX : ScriptableObject
  {
    public static DirX right;
    public static DirX left;

    public static DirX[] GetList() => new DirX[] { right, left };
    public static void OnSettings(KiteSettings settings) =>
      (left, right) = (settings.leftDirX, settings.rightDirX);

    public static implicit operator int(DirX dir) => dir.value;
    public static implicit operator DirX(int val) => val > 0 ? right : left;

    public static implicit operator float(DirX dir) => dir.value;
    public static implicit operator DirX(float val) => val > 0 ? right : left;

    public static implicit operator Vector2(DirX dir) => Vector2.right * dir.value;
    public static DirX operator -(DirX dir) => dir == left ? right : left;

    public static DirX Random() => RandomHelpers.OneOrMinusOne();

    public string identifier;
    public int value;

    public override string ToString() => identifier;
  }
}