using Kite;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpikesTilemap : MonoBehaviour
{
  public Vector2 pushTileForce;
  public List<TileBase> tiles;

  public bool MatchForCollidable(TileBase tile) => tiles.Contains(tile);

  public float TileGetAllowedMoveInto(PhysicsMove move) => move.collideDistance;

  public void TileOnMoveInto(PhysicsMove move)
  {
    // TODO: use PlayerCollider or move.movement.controller
    PlayerUnitController unit = move.hit.rigidbody.GetComponent<PlayerUnitController>();
    if (!unit)
      return;

    if (unit.di.vulnerability.IsVulnerable())
    {
      float randomSign = RandomHelpers.OneOrMinusOne();
      Vector2 randomPushForce = TileHelpers.TileToWorld(new Vector2(randomSign * pushTileForce.x, pushTileForce.y));
      unit.di.damage.TakeFullDamage(randomPushForce);
    }
  }
}