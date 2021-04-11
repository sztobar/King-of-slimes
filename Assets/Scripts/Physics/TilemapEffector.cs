using Kite;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapEffector : PhysicsEffector<PhysicsEffectable>
{
  public Tilemap tilemap;
  public PlatformTilemap platforms;

  public override bool Match(CollisionMoveHit hit)
  {
    Vector3Int tilemapPosition = ToTileMapPosition(hit.point + hit.rayDirection.ToVector2(RaycastHelpers.skinWidth));
    TileBase tile = tilemap.GetTile(tilemapPosition);
    if (!tile)
      return false;

    if (platforms.MatchForEffector(tile))
      return true;

    return false;
  }

  public override void RegisterEffectable(EffectableBehaviour effectable)
  {
    PlatformSkippable platformSkippable = effectable.GetComponent<PlatformSkippable>();
    if (platformSkippable)
      platformSkippable.IsStandingOnPlatform = true;

    base.RegisterEffectable(effectable);
  }

  private Vector3Int ToTileMapPosition(Vector3 worldPosition)
  {
    Vector2 tileSize = Vector2Int.RoundToInt(tilemap.cellSize);
    int x = Mathf.FloorToInt(worldPosition.x / tileSize.x);
    int y = Mathf.FloorToInt(worldPosition.y / tileSize.y);
    return new Vector3Int(x, y, 0);
  }
}
