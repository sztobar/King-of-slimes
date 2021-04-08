using System;
using System.Collections;
using UnityEngine;

namespace Kite
{
  public class GizmosHelpers
  {
    public static void FatArrow(Vector3 pos, Vector3 direction, float width = 1f)
    {
      Vector3 arrowWidth = direction.normalized * width;
      Vector3 arrowLength = direction - arrowWidth;
      Gizmos.DrawRay(pos, arrowLength);

      Vector3 trunkDirection = Vector2Helpers.Rotate(arrowWidth, 90);
      Gizmos.DrawRay(pos, trunkDirection);
      Gizmos.DrawRay(pos + trunkDirection, arrowLength);

      Vector3 arrowBottomWing = Vector2Helpers.Rotate(arrowWidth / 2, -90);
      Gizmos.DrawRay(pos + arrowLength, arrowBottomWing);

      Vector3 arrowTopWing = trunkDirection / 2;
      Gizmos.DrawRay(pos + trunkDirection + arrowLength, arrowTopWing);

      Vector3 arrowEnd = pos + (trunkDirection / 2) + arrowLength + arrowWidth;
      Gizmos.DrawLine(pos + arrowLength + arrowBottomWing, arrowEnd);
      Gizmos.DrawLine(pos + trunkDirection + arrowLength + arrowTopWing, arrowEnd);
    }

    public static void Arrow(Vector3 pos, Vector3 direction, float width = 1f)
    {
      Gizmos.DrawRay(pos, direction);
      Gizmos.DrawRay(pos + direction, Vector2Helpers.Rotate(-direction.normalized * width, 45));
      Gizmos.DrawRay(pos + direction, Vector2Helpers.Rotate(-direction.normalized * width, -45));
    }

    public static void Collider(Collider2D collider)
    {
      Bounds bounds = collider.bounds;
      Gizmos.DrawWireCube(bounds.center, bounds.size);
    }
  }
}