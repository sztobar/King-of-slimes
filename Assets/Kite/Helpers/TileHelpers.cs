using System;
using UnityEngine;

namespace Kite
{
  public static class TileHelpers
  {
    public static float tileSize = 16;

    public static float TileToWorld(float value) => value * tileSize;
    public static Vector2 TileToWorld(Vector2 value) => value * tileSize;

    public static float Floor(float value) => Mathf.Floor(value / tileSize) * tileSize;

    public static float Ceil(float value) => Mathf.Ceil(value / tileSize) * tileSize;

    public static void OnSettings(KiteSettings settings)
    {
      tileSize = settings.tileSize;
    }
  }
}