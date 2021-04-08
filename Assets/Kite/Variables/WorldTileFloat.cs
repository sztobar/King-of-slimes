using Kite;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kite
{
  [Serializable]
  public struct WorldTileFloat
  {
    public float value;
    public Type type;

    public static implicit operator float(WorldTileFloat v)
    {
      if (v.type == Type.World)
        return v.value;
      else
        return v.value * TileHelpers.tileSize;
    }

    public static implicit operator WorldTileFloat(float v) =>
      new WorldTileFloat { value = v, type = Type.World };

    public override string ToString() => ((float)this).ToString();

    public enum Type
    {
      World,
      Tile
    }
  }
}