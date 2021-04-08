using System;
using System.Collections;
using UnityEngine;

namespace Kite
{
  public static class RaycastHelpers 
  {
    private static readonly RaycastHit2D[] results = new RaycastHit2D[4];
    public static float skinWidth = 0.01f;
    public static float raycastGap = 8f;
    public static bool drawDebug = true;
    public static Color debugColor = Color.red;

    /// <summary>
    /// Checks if distance is greater or equal to skinWidth
    /// </summary>
    /// <param name="dist"></param>
    /// <returns></returns>
    public static bool IsValidDistance(float dist) => dist >= skinWidth;

    /// <summary>
    /// Floors the distance to 0 if its lower than skinWidth
    /// </summary>
    /// <param name="dist"></param>
    /// <returns></returns>
    public static float FloorDistance(float dist) => IsValidDistance(dist) ? dist : 0;

    /// <summary>
    /// Ceils the distance to skinWidth if its lower
    /// </summary>
    /// <param name="dist"></param>
    /// <returns></returns>
    public static float CeilDistance(float dist) => Mathf.Max(dist, skinWidth);

    public static (int, RaycastHit2D[]) Raycast(Vector2 position, float distance, Dir4 direction, int layerMask)
    {
      Vector2 rayDirection = direction;
      Vector2 rayOrigin = position + (-rayDirection * skinWidth);
      float rayLength = distance + skinWidth;
      int count = Physics2D.RaycastNonAlloc(rayOrigin, rayDirection, results, rayLength, layerMask);

      for (int i = 0; i < count; i++)
      {
        ref RaycastHit2D hit = ref results[i];
        if (hit.distance <= 0)
          continue;

        hit.distance = Mathf.Max(hit.distance - skinWidth, 0);
        hit.fraction = hit.distance / distance;
      }

      if (drawDebug)
        Debug.DrawRay(position, rayDirection * distance, debugColor);

      return (count, results);
    }

    public static RaycastHit2D SingleHit(Vector2 position, float distance, Dir4 direction, int layerMask)
    {
      Vector2 rayDirection = direction;
      Vector2 rayOrigin = position + (-rayDirection * skinWidth);
      float rayLength = distance + skinWidth;

      if (drawDebug)
        Debug.DrawRay(position, rayDirection * distance, debugColor);

      RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, layerMask);

      if (hit.distance > 0)
      {
         hit.distance = Mathf.Max(hit.distance - skinWidth, 0);
        hit.fraction = hit.distance / distance;
      }
      return hit;
    }
  }
}