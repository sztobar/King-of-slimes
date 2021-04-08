using UnityEngine;

namespace Kite
{
  public static class BoundsHelpers
  {
    public static Vector2 GetEnd(this Bounds bounds, Direction2H horizontalDir, Direction2V vericalDir)
    {
      Vector3 skinWidth = Vector2.one * RaycastHelpers.skinWidth;
      bounds.extents -= skinWidth;
      return new Vector2(
        horizontalDir == Direction2H.Left ? bounds.min.x : bounds.max.x,
        vericalDir == Direction2V.Down ? bounds.min.y : bounds.max.y
      );
    }

    public static Vector2 GetCorner(this Bounds bounds, DirX dirX, DirY dirY) =>
      new Vector2(
        dirX == DirX.left ? bounds.min.x : bounds.max.x,
        dirY == DirY.down ? bounds.min.y : bounds.max.y
      );
  }
}