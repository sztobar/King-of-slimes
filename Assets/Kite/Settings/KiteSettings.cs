using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kite
{
  public class KiteSettings : ScriptableObject
  {
    public static KiteSettings instance;
    public const string resourceName = "KiteSettings";

    public int tileSize = 16;

    public DirX rightDirX, leftDirX;
    public DirY upDirY, downDirY;
    public Dir4 upDir4, rightDir4, downDir4, leftDir4;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void RuntimeInit()
    {
      if (!instance)
      {
        instance = Resources.Load<KiteSettings>(resourceName);
        DirX.OnSettings(instance);
        DirY.OnSettings(instance);
        Dir4.OnSettings(instance);
        TileHelpers.OnSettings(instance);
      }
    }
  }
}