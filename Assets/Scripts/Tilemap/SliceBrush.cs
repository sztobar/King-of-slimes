using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

[CustomGridBrush(false, false, false, "Slice Brush")]
public class SliceBrush : GridBrush
{
  public TileBase[,] sliceTiles;
  public Vector2Int repeatTilesIndex;
  public bool pickSliceTiles;

  public SliceGizmos sliceGizmos;
  public Vector3Int slicePosition;

  public override void BoxFill(GridLayout gridLayout, GameObject brushTarget, BoundsInt bounds)
  {
    if (sliceTiles.Length > 0)
    {
      if (brushTarget == null)
        return;

      var tilemap = brushTarget.GetComponent<Tilemap>();
      if (tilemap == null)
        return;

      TileBase[,] tileArray = GetTilesArrayFor(bounds);
      Vector3Int boundsSize = bounds.size;
      TileBase[] flatTilesArray = new TileBase[bounds.size.x * bounds.size.y];

      for (int y = 0; y < boundsSize.y; y++)
      {
        for (int x = 0; x < boundsSize.x; x++)
        {
          int i = y * bounds.size.x + x;
          flatTilesArray[i] = tileArray[x, y];
        }
      }
      tilemap.SetTilesBlock(bounds, flatTilesArray);

    }
    else
    {
      base.BoxFill(gridLayout, brushTarget, bounds);
    }
  }

  public TileBase[,] GetTilesArrayFor(BoundsInt bounds)
  {
    Vector2Int sliceSize = new Vector2Int(
      sliceTiles.GetLength(0),
      sliceTiles.GetLength(1)
    );
    Vector3Int boundsSize = bounds.size;
    bool useSliceRepeat_x = boundsSize.x >= sliceSize.x;
    bool useSliceRepeat_y = boundsSize.y >= sliceSize.y;
    Vector2Int sliceRepeatStart = new Vector2Int(
      useSliceRepeat_x ? repeatTilesIndex.x : -1,
      useSliceRepeat_y ? repeatTilesIndex.y : -1
    );
    Vector2Int repeatTilesIndexEnd = repeatTilesIndex;

    if (repeatTilesIndex.x < sliceSize.x / 2)
      repeatTilesIndexEnd.x = sliceSize.x - 1 - repeatTilesIndex.x;

    if (repeatTilesIndex.y < sliceSize.y / 2)
      repeatTilesIndexEnd.y = sliceSize.y - 1 - repeatTilesIndex.y;

    Vector2Int sliceRepeatEnd = new Vector2Int(
      useSliceRepeat_x ? boundsSize.x - 1 - repeatTilesIndexEnd.x : -1,
      useSliceRepeat_y ? boundsSize.y - 1 - repeatTilesIndexEnd.y : -1
    );

    Vector3Int boundsExtents = bounds.size / 2;

    TileBase[,] tileArray = new TileBase[bounds.size.x, bounds.size.y];

    for (int x = 0; x < boundsSize.x; x++)
    {
      int slice_x = x;
      if (useSliceRepeat_x && x >= sliceRepeatStart.x && x <= sliceRepeatEnd.x)
      {
        slice_x = repeatTilesIndex.x;
      }
      else if (x >= boundsExtents.x)
      {
        int bounds_x_fromEnd = boundsSize.x - 1 - x;
        int slice_x_fromEnd = sliceSize.x - 1 - bounds_x_fromEnd;
        slice_x = slice_x_fromEnd;
      }

      for (int y = 0; y < boundsSize.y; y++)
      {
        int slice_y = y;
        if (useSliceRepeat_y && y >= sliceRepeatStart.y && y <= sliceRepeatEnd.y)
        {
          slice_y = repeatTilesIndex.y;
        }
        else if (y >= boundsExtents.y)
        {
          int bounds_y_fromEnd = boundsSize.y - 1 - y;
          int slice_y_fromEnd = sliceSize.y - 1 - bounds_y_fromEnd;
          slice_y = slice_y_fromEnd;
        }

        tileArray[x, y] = sliceTiles[slice_x, slice_y];
      }
    }
    return tileArray;
  }

  public override void Pick(GridLayout gridLayout, GameObject brushTarget, BoundsInt bounds, Vector3Int pickStart)
  {
    base.Pick(gridLayout, brushTarget, bounds, pickStart);
    if (!pickSliceTiles)
      return;

    Tilemap tilePalette = brushTarget.GetComponent<Tilemap>();
    if (tilePalette == null)
      return;

    SliceGizmos sliceGizmos = tilePalette.GetComponent<SliceGizmos>();
    if (!sliceGizmos)
    {
      sliceGizmos = tilePalette.gameObject.AddComponent<SliceGizmos>();
      sliceGizmos.tilemap = tilePalette;
    }
    if (sliceGizmos)
    {
      this.sliceGizmos = sliceGizmos;
      sliceGizmos.activeBrush = this;
    }

    int cols = bounds.size.x;
    int rows = bounds.size.y;

    sliceTiles = new TileBase[cols, rows];
    repeatTilesIndex = new Vector2Int(cols / 2, rows / 2);

    int x = 0;
    int y = 0;
    foreach (Vector3Int pos in bounds.allPositionsWithin)
    {
      sliceTiles[x, y] = tilePalette.GetTile(pos);
      x++;
      if (x >= cols)
      {
        x = 0;
        y++;
      }
    }
    slicePosition = bounds.position;
  }
}

/// <summary>
/// The Brush Editor for a Random Brush.
/// </summary>
[CustomEditor(typeof(SliceBrush))]
public class SliceBrushEditor : GridBrushEditor
{
  private SliceBrush SliceBrush => target as SliceBrush;
  private GameObject lastBrushTarget;

  public override void BoxFillPreview(GridLayout grid, GameObject brushTarget, BoundsInt bounds)
  {
    if (SliceBrush.sliceTiles.Length > 0)
    {
      if (brushTarget == null)
        return;

      var tilemap = brushTarget.GetComponent<Tilemap>();
      if (tilemap == null)
        return;

      TileBase[,] tileArray = SliceBrush.GetTilesArrayFor(bounds);
      Vector3Int boundsSize = bounds.size;
      for (int y = 0; y < boundsSize.y; y++)
      {
        for (int x = 0; x < boundsSize.x; x++)
        {
          Vector3Int pos = bounds.position + new Vector3Int(x, y, 0);
          tilemap.SetEditorPreviewTile(pos, tileArray[x, y]);
        }
      }
      lastBrushTarget = brushTarget;
    }
    else
    {
      base.BoxFillPreview(grid, brushTarget, bounds);
    }
  }

  /// <summary>
  /// Clears all RandomTileSet previews.
  /// </summary>
  public override void ClearPreview()
  {
    if (lastBrushTarget != null)
    {
      var tilemap = lastBrushTarget.GetComponent<Tilemap>();
      if (tilemap == null)
        return;

      tilemap.ClearAllEditorPreviewTiles();

      lastBrushTarget = null;
    }
    else
    {
      base.ClearPreview();
    }
  }

  /// <summary>
  /// Callback for painting the inspector GUI for the RandomBrush in the Tile Palette.
  /// The RandomBrush Editor overrides this to have a custom inspector for this Brush.
  /// </summary>
  public override void OnPaintInspectorGUI()
  {
    EditorGUI.BeginChangeCheck();
    SliceBrush.pickSliceTiles = EditorGUILayout.Toggle("Pick Slice Tiles", SliceBrush.pickSliceTiles);

    int sliceTilesCount = EditorGUILayout.DelayedIntField("Number of Tiles", SliceBrush.sliceTiles != null ? SliceBrush.sliceTiles.Length : 0);

    if (sliceTilesCount > 0)
    {
      EditorGUI.BeginChangeCheck();
      SliceBrush.repeatTilesIndex = EditorGUILayout.Vector2IntField("Repeat tiles indexes", SliceBrush.repeatTilesIndex);
      if (EditorGUI.EndChangeCheck())
      {
        SliceBrush.repeatTilesIndex.x = Mathf.Clamp(SliceBrush.repeatTilesIndex.x, 0, SliceBrush.sliceTiles.GetLength(0) - 1);
        SliceBrush.repeatTilesIndex.y = Mathf.Clamp(SliceBrush.repeatTilesIndex.y, 0, SliceBrush.sliceTiles.GetLength(1) - 1);
      }

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Tiles:");

      int cols = SliceBrush.sliceTiles.GetLength(0);
      int rows = SliceBrush.sliceTiles.GetLength(1);
      for (int x = 0; x < cols; x++)
      {
        for (int y = 0; y < rows; y++)
        {
          SliceBrush.sliceTiles[x, y] = (TileBase)EditorGUILayout.ObjectField($"Tile [{x},{y}]", SliceBrush.sliceTiles[x, y], typeof(TileBase), false, null);
        }
      }
    }

    if (EditorGUI.EndChangeCheck())
      EditorUtility.SetDirty(SliceBrush);
  }

  protected override void OnDisable()
  {
    base.OnDisable();
    if (SliceBrush.sliceGizmos)
      SliceBrush.sliceGizmos = null;
  }
}