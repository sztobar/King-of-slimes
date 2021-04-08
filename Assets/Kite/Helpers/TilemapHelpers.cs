using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Kite
{
  public static class TilemapHelpers
  {
    public static Vector3Int ToTilemapPosition(Tilemap tilemap, Vector2 worldPosition)
    {
      Vector3 cellSize = tilemap.cellSize;
      int x = Mathf.FloorToInt(worldPosition.x / cellSize.x);
      int y = Mathf.FloorToInt(worldPosition.y / cellSize.y);
      return new Vector3Int(x, y, 0);
    }

    // Needed for tilemap PhyisicsCollidable not for "just" every tilemap collider
    public static bool IsColliderTilemap(Collider2D collider) => collider.gameObject.CompareTag("Tilemap");
  }
}