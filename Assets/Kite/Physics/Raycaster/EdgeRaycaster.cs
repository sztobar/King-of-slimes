using UnityEngine;
using System;

namespace Kite
{
  public class EdgeRaycaster : ScriptableObject
  {
    private readonly static int maxEdgeRayHitsCount = 24;
    private readonly static RaycastHit2D[] edgeRayHits = new RaycastHit2D[maxEdgeRayHitsCount];

    public Vector2[] rayDeltas;
    public int layerMask;

    public static EdgeRaycaster Create(Vector2 edge, LayerMask layerMask)
    {
      EdgeRaycaster instance = CreateInstance<EdgeRaycaster>();
      instance.layerMask = layerMask;
      instance.GenerateRayDeltas(edge);
      return instance;
    }

    public (RaycastHit2D[], int) GetHits(Vector2 origin, float distance, Dir4 dir)
    {
      distance = RaycastHelpers.CeilDistance(distance);
      int rays = rayDeltas.Length;
      int edgeRayHitsCount = 0;
      for (int i = 0; i < rays; i++)
      {
        Vector2 rayOrigin = origin + rayDeltas[i];
        (int singleRayHitsCount, RaycastHit2D[] singleRayHits) = RaycastHelpers.Raycast(rayOrigin, distance, dir, layerMask);
        if (edgeRayHitsCount + singleRayHitsCount > maxEdgeRayHitsCount)
        {
          return ForceReturnOverHitsResults(edgeRayHitsCount, singleRayHitsCount, singleRayHits);
        }
        Array.Copy(singleRayHits, 0, edgeRayHits, edgeRayHitsCount, singleRayHitsCount);
        edgeRayHitsCount += singleRayHitsCount;
      }

      return (edgeRayHits, edgeRayHitsCount);
    }

    public void GenerateRayDeltas(Vector2 edge)
    {
      int rays = Mathf.CeilToInt(edge.magnitude / RaycastHelpers.raycastGap) + 1;
      Vector2 deltaVectorNormalized = (edge / rays).normalized;
      int lastDeltaIndex = rays - 1;

      rayDeltas = new Vector2[rays];
      rayDeltas[0] = deltaVectorNormalized * RaycastHelpers.skinWidth;
      for (int i = 1; i < lastDeltaIndex; i++)
      {
        rayDeltas[i] = deltaVectorNormalized * RaycastHelpers.raycastGap * i;
      }
      rayDeltas[lastDeltaIndex] = deltaVectorNormalized * (edge.magnitude - RaycastHelpers.skinWidth);
    }

    private static (RaycastHit2D[], int) ForceReturnOverHitsResults(int edgeRayHitsCount, int singleRayHitsCount, RaycastHit2D[] singleRayHits)
    {
      Debug.LogWarning($"EdgeRaycaster GetHits reached more than {maxEdgeRayHitsCount} ray hits.");
      int edgeOverHits = edgeRayHitsCount + singleRayHitsCount - maxEdgeRayHitsCount;
      int maxSingleRayHitsCount = singleRayHitsCount - edgeOverHits;
      Array.Copy(singleRayHits, 0, edgeRayHits, edgeRayHitsCount, maxSingleRayHitsCount);
      edgeRayHitsCount += maxSingleRayHitsCount;
      return (edgeRayHits, edgeRayHitsCount);
    }
  }
}