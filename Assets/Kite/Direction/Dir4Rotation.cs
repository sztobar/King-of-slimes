using System;
using System.Collections;
using UnityEngine;

namespace Kite
{
  public static class Dir4Rotation
  {
    public static Dir4 Clockwise(Dir4 dir)
    {
      Dir4[] list = Dir4.GetList();
      int idx = Array.IndexOf(list, dir);
      if (idx != -1)
        return list[(idx + 1) % 4];
      return null;
    }

    public static Dir4 CounterClockwise(Dir4 dir)
    {
      Dir4[] list = Dir4.GetList();
      int idx = Array.IndexOf(list, dir);
      if (idx != -1)
        return list[idx > 0 ? idx - 1 : 3];
      return null;
    }

    public static float GetRotation(Dir4 dir)
    {
      Dir4[] list = Dir4.GetList();
      int idx = Array.IndexOf(list, dir);
      if (idx != -1)
        return idx == 0 ? 0 : (360 - (idx * 90));
      return 0;
    }
  }
}