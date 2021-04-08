using UnityEngine;
using System;
using System.Collections.Generic;

namespace Kite
{
  public class BoxRaycaster : ScriptableObject
  {
    public EdgeRaycaster xEdge;
    public EdgeRaycaster yEdge;
    public BoxCollider2D collider;

    public static BoxRaycaster Create(BoxCollider2D collider, LayerMask layerMask)
    {
      BoxRaycaster instance = CreateInstance<BoxRaycaster>();
      instance.collider = collider;
      Bounds bounds = collider.bounds;
      instance.xEdge = EdgeRaycaster.Create(Vector2.right * bounds.size.x, layerMask);
      instance.yEdge = EdgeRaycaster.Create(Vector2.up * bounds.size.y, layerMask);
      return instance;
    }

    public void InvalidateBounds()
    {
      Bounds bounds = collider.bounds;
      xEdge.GenerateRayDeltas(Vector2.right * bounds.size.x);
      yEdge.GenerateRayDeltas(Vector2.up * bounds.size.y);
    }

    [Obsolete("Use GetHits with Dir4")]
    public (RaycastHit2D[], int) GetHits(float distance, Direction4 dir, Vector2 deltaPosition = default) =>
      GetHits(distance, Direction4Helpers.ToDir4(dir), deltaPosition);

    public (RaycastHit2D[], int) GetHits(float distance, Dir4 dir, Vector2 deltaPosition = default)
    {
      Bounds bounds = collider.bounds;
      if (dir.Axis == VectorAxis.x)
      {
        Vector2 origin = BoundsHelpers.GetCorner(bounds, dir.x, DirY.down);
        origin += deltaPosition;
        return yEdge.GetHits(origin, distance, dir);
      }
      else
      {
        Vector2 origin = BoundsHelpers.GetCorner(bounds, DirX.left, dir.y);
        origin += deltaPosition;
        return xEdge.GetHits(origin, distance, dir);
      }
    }
  }
}