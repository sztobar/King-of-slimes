using System;
using System.Collections;
using UnityEngine;

namespace Kite
{
  public class DirY : ScriptableObject
  {
    public static DirY up;
    public static DirY down;

    public static DirY[] GetList() => new DirY[] { up, down };
    public static void OnSettings(KiteSettings settings) =>
      (down, up) = (settings.downDirY, settings.upDirY);

    public static implicit operator int(DirY dir) => dir.value;
    public static implicit operator DirY(int val) => val > 0 ? up : down;

    public static implicit operator float(DirY dir) => dir.value;
    public static implicit operator DirY(float val) => val > 0 ? up : down;

    public static implicit operator Vector2(DirY dir) => Vector2.up * dir.value;
    public static DirY operator -(DirY dir) => dir == down ? up : down;

    public static DirY Random() => RandomHelpers.OneOrMinusOne();

    public string identifier;
    public int value;

    public override string ToString() => identifier;
  }
}