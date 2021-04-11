using Kite;
using System;
using UnityEngine;

[ExecuteInEditMode]
public class CameraSegmentSize : MonoBehaviour
{
  public Vector2Int tileSize = new Vector2Int(16, 16);
  public Vector2Int resolution = new Vector2Int(320, 180);
  public Vector2Int segmentSizeInTiles = Vector2Int.one;
  public CameraSegmentType type = CameraSegmentType.Tunnel;
  public BoxCollider2D boxCollider2D;
  public GizmoType gizmoType;
  public Color gizmoColor = Color.green;

  public Vector2Int WorldSize => segmentSizeInTiles * tileSize;

  public void Update()
  {
    OnSizeChange();
  }

  public void OnSizeChange()
  {
    if (boxCollider2D)
    {
      boxCollider2D.offset = Vector2.zero;
      boxCollider2D.size = WorldSize;
    }
  }

  public void OnValidate()
  {
    Vector2Int minSize = new Vector2Int(
       resolution.x / tileSize.x,
       resolution.y / tileSize.y
    );
    switch (type)
    {
      case CameraSegmentType.Tunnel:
        segmentSizeInTiles.x = Math.Max(segmentSizeInTiles.x, minSize.x);
        segmentSizeInTiles.y = minSize.y;
        break;
      case CameraSegmentType.Well:
        segmentSizeInTiles.x = minSize.x;
        segmentSizeInTiles.y = Math.Max(segmentSizeInTiles.y, minSize.y);
        break;
      case CameraSegmentType.Area:
      default:
        segmentSizeInTiles.x = Math.Max(segmentSizeInTiles.x, minSize.x);
        segmentSizeInTiles.y = Math.Max(segmentSizeInTiles.y, minSize.y);
        break;
    }
  }

  private void OnDrawGizmos()
  {
    if (gizmoType == GizmoType.None)
      return;
    Gizmos.color = gizmoColor;

    if (gizmoType == GizmoType.Border)
      Gizmos.DrawWireCube(transform.position, (Vector2)WorldSize);
    else
      Gizmos.DrawCube(transform.position, (Vector2)WorldSize);
  }

  public enum CameraSegmentType
  {
    Tunnel,
    Well,
    Area
  }

  public enum GizmoType
  {
    None,
    Border,
    Fill
  }
}