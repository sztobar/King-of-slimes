using Kite;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SliceGizmos : MonoBehaviour
{
  public Tilemap tilemap;
  [HideInInspector] public SliceBrush activeBrush;

  private void OnDrawGizmos()
  {
    if (!activeBrush)
      return;

    if (activeBrush.sliceTiles.Length == 0)
      return;

    Vector2 tileSize = tilemap.cellSize;
    Vector2 offset = tilemap.tileAnchor * tileSize;
    Vector2 pos = new Vector2(
      activeBrush.slicePosition.x,
      activeBrush.slicePosition.y
    );
    Gizmos.color = new Color(1, 1, 1, 0.15f);
    Vector2 repeatCenter = new Vector3(
      activeBrush.repeatTilesIndex.x,
      activeBrush.repeatTilesIndex.y
    );
    int width = activeBrush.sliceTiles.GetLength(0);
    int height = activeBrush.sliceTiles.GetLength(1);

    if (height > 1)
      Gizmos.DrawCube((pos + repeatCenter) * tileSize + offset, new Vector3(width, 1) * tileSize);

    if (width > 1)
      Gizmos.DrawCube((pos + repeatCenter) * tileSize + offset, new Vector3(1, height) * tileSize);
  }
}