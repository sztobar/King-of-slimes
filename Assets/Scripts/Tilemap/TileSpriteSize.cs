using System;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

[ExecuteInEditMode]
public class TileSpriteSize : MonoBehaviour
{
  public Vector2Int spriteSizeInTiles = Vector2Int.one;
  public Vector2Int tileSize = new Vector2Int(16, 16);
  public SpriteRenderer spriteRenderer;

  public Vector2Int WorldSize => spriteSizeInTiles * tileSize;

  public void Update()
  {
    OnSizeChange();
  }

  public void OnSizeChange()
  {
    if (spriteRenderer && spriteRenderer.drawMode == SpriteDrawMode.Tiled && tileSize != Vector2Int.zero)
    {
      spriteRenderer.size = WorldSize;
    }
  }

  public void OnValidate()
  {
    spriteSizeInTiles.x = Math.Max(spriteSizeInTiles.x, 1);
    spriteSizeInTiles.y = Math.Max(spriteSizeInTiles.y, 1);
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawWireCube(transform.position, (Vector2)WorldSize);
  }
}
