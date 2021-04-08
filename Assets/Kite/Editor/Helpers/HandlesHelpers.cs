using Kite;
using System;
using UnityEditor;
using UnityEngine;

namespace KiteEditor
{
  public static class HandlesHelpers
  {
    public static void FatArrow(Vector3 pos, Vector3 direction, float width = 1f)
    {
      Vector3 arrowWidth = direction.normalized * width;
      Vector3 arrowLength = direction - arrowWidth;
      Handles.DrawLine(pos, pos + arrowLength);

      Vector3 trunkDirection = Vector2Helpers.Rotate(arrowWidth, 90);
      Handles.DrawLine(pos, pos + trunkDirection);
      Handles.DrawLine(pos + trunkDirection, pos + trunkDirection + arrowLength);

      Vector3 arrowBottomWing = Vector2Helpers.Rotate(arrowWidth / 2, -90);
      Handles.DrawLine(pos + arrowLength, pos + arrowLength + arrowBottomWing);

      Vector3 arrowTopWing = trunkDirection / 2;
      Handles.DrawLine(pos + trunkDirection + arrowLength, pos + trunkDirection + arrowLength + arrowTopWing);

      Vector3 arrowEnd = pos + (trunkDirection / 2) + arrowLength + arrowWidth;
      Handles.DrawLine(pos + arrowLength + arrowBottomWing, arrowEnd);
      Handles.DrawLine(pos + trunkDirection + arrowLength + arrowTopWing, arrowEnd);
    }

    public static void Arrow(Vector3 pos, Vector3 direction, float width = 1f)
    {
      Vector3 arrowTip = pos + direction;
      Handles.DrawLine(pos, arrowTip);
      Handles.DrawLine(arrowTip, arrowTip + (Vector3)Vector2Helpers.Rotate(-direction.normalized * width, 45));
      Handles.DrawLine(arrowTip, arrowTip + (Vector3)Vector2Helpers.Rotate(-direction.normalized * width, -45));
    }

    public static void Cross(Vector3 position, int size)
    {
      Handles.DrawLine(position + new Vector3(-size, size), position + new Vector3(size, -size));
      Handles.DrawLine(position + new Vector3(-size, -size), position + new Vector3(size, size));
    }

    public static bool ColliderButton(Collider2D collider)
    {
      Vector3 extents = collider.bounds.extents;
      Vector3 scale = GetScale(extents);
      Vector3 offset = collider.offset;
      Matrix4x4 orginalMatrix = Handles.matrix;
      Handles.matrix = Matrix4x4.Scale(scale);
      Vector3 buttonPosition = Vector3.Scale(collider.transform.position + offset, new Vector3(1 / scale.x, 1 / scale.y, 1));
      float handleSize = Mathf.Min(extents.x, extents.y);
      bool clicked = Handles.Button(buttonPosition, Quaternion.identity, handleSize, handleSize, Handles.RectangleHandleCap);
      Handles.matrix = orginalMatrix;
      return clicked;
    }

    public static Vector3 GetScale(Vector2 size)
    {
      if (size.x > size.y)
      {
        float xScale = size.x / size.y;
        return new Vector3(xScale, 1, 1);
      }
      else if (size.y > size.x)
      {
        float yScale = size.y / size.x;
        return new Vector3(1, yScale, 1);
      }
      return Vector3.one;
    }

    public static void Collider(Collider2D collider)
    {
      Bounds bounds = collider.bounds;
      Color originalColor = Handles.color;
      Handles.color = Color.green;
      Handles.DrawLines(new Vector3[] {
        new Vector3(bounds.min.x, bounds.max.y),
        new Vector3(bounds.max.x, bounds.max.y),

        new Vector3(bounds.max.x, bounds.max.y),
        new Vector3(bounds.max.x, bounds.min.y),

        new Vector3(bounds.max.x, bounds.min.y),
        new Vector3(bounds.min.x, bounds.min.y),

        new Vector3(bounds.min.x, bounds.min.y),
        new Vector3(bounds.min.x, bounds.max.y),
      });
      Handles.color = originalColor;
    }
  }
}
