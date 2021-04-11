using Kite;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapCollidable : PhysicsCollidable
{
  public Tilemap tilemap;
  public SpikesTilemap spikes;
  public AbyssTilemap abyss;
  public PlatformTilemap platforms;

  private Vector2Int tileExtents;
  private Vector2Int tileSize;

  private void Awake()
  {
    tileSize = Vector2Int.RoundToInt(tilemap.cellSize);
    tileExtents = tileSize / 2;
  }

  public override float GetAllowedMoveInto(PhysicsMove move)
  {
    Vector2 hitPoint = move.hit.point;
    Vector2 skinInCollisionDirection = (Vector2)move.dir * RaycastHelpers.skinWidth;
    Vector3Int tilemapPosition = ToTileMapPosition(hitPoint + skinInCollisionDirection);
    TileBase tile = tilemap.GetTile(tilemapPosition);
    if (!tile)
      return move.collideDistance;

    DebugTile(tilemapPosition);

    float allTilesAllowedMove = 0;
    while (true)
    {
      float allowedMove;
      if (spikes.MatchForCollidable(tile))
        allowedMove = spikes.TileGetAllowedMoveInto(move);
      else if (abyss.MatchForCollidable(tile))
        allowedMove = abyss.TileGetAllowedMoveInto(move);
      else if (platforms.MatchForCollidable(tile))
        allowedMove = platforms.TileGetAllowedMoveInto(move);
      else
        break;

      float safeAllowedMove = Mathf.Min(allowedMove, tileSize.x);
      Vector2 safeAllowedMoveInDir = (Vector2)move.dir * (safeAllowedMove + RaycastHelpers.skinWidth);
      Vector3Int tilemapPositionAfterAllowedMove = ToTileMapPosition(hitPoint + safeAllowedMoveInDir);
      TileBase tileAfterAllowedMove = tilemap.GetTile(tilemapPositionAfterAllowedMove);
      if (tileAfterAllowedMove && tileAfterAllowedMove != tile)
      {
        tile = tileAfterAllowedMove;
        Vector2 newHitPoint = GetNewHitPoint(hitPoint, tilemapPositionAfterAllowedMove, move.dir);
        allowedMove = Vector2.Distance(newHitPoint, hitPoint);
        hitPoint = newHitPoint;
        allTilesAllowedMove += allowedMove;
      }
      else
      {
        allTilesAllowedMove += allowedMove;
        break;
      }
    }

    return allTilesAllowedMove;
  }

  public override void OnMoveInto(PhysicsMove move)
  {
    Vector2 hitPoint = move.hit.point;
    Vector2 skinInCollisionDirection = (Vector2)move.dir * RaycastHelpers.skinWidth;
    Vector3Int tilemapPosition = ToTileMapPosition(hitPoint + skinInCollisionDirection);
    TileBase tile = tilemap.GetTile(tilemapPosition);

    if (tile)
    {
      if (spikes.MatchForCollidable(tile))
        spikes.TileOnMoveInto(move);
      else if (abyss.MatchForCollidable(tile))
        abyss.TileOnMoveInto(move);
      else if (platforms.MatchForCollidable(tile))
        platforms.TileOnMoveInto(move);
    }
  }

  private Vector3Int ToTileMapPosition(Vector3 worldPosition)
  {
    int x = Mathf.FloorToInt(worldPosition.x / tileSize.x);
    int y = Mathf.FloorToInt(worldPosition.y / tileSize.y);
    return new Vector3Int(x, y, 0);
  }

  private Vector2 GetNewHitPoint(Vector2 hitPoint, Vector3Int tilePosition, Dir4 dir)
  {
    Vector2 tileWorldPosition = ToWorldPosition(tilePosition, dir);
    int axis = dir.Axis;
    hitPoint[axis] = tileWorldPosition[axis];
    return hitPoint;
  }

  private Vector2 ToWorldPosition(Vector3Int tilePosition, Dir4 dir)
  {
    float x = tilePosition.x * tileSize.x;
    float y = tilePosition.y * tileSize.y;

    if (dir == Dir4.left)
      x += tileSize.x;

    if (dir == Dir4.down)
      y += tileSize.y;

    return new Vector2(x, y);
  }

  private void DebugTile(Vector3Int tilemapPosition)
  {
    float tileWorldX = tilemapPosition.x * tileSize.x + (tilemap.tileAnchor.x * tileSize.x);
    float tileWorldY = tilemapPosition.y * tileSize.y + (tilemap.tileAnchor.y * tileSize.y);
    Vector3 tileWorldPos = new Vector3(tileWorldX, tileWorldY);

    Vector3 tileWorldSize = new Vector3(tileSize.x, tileSize.y);
    Bounds tileBounds = new Bounds(tileWorldPos, tileWorldSize);

    DebugHelpers.DrawBox(tileBounds, Color.green);
  }
}