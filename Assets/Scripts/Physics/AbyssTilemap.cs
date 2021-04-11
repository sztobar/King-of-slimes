using Kite;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AbyssTilemap : MonoBehaviour
{
  public List<TileBase> tiles;

  public bool MatchForCollidable(TileBase tile) => tiles.Contains(tile);


  public float TileGetAllowedMoveInto(PhysicsMove move) => move.collideDistance;

  // TODO: freeze camera and make unconditional fall below the collider
  public void TileOnMoveInto(PhysicsMove move)
  {
    // TODO: use PlayerCollider or move.movement.controller
    PlayerUnitController unit = move.hit.rigidbody.GetComponent<PlayerUnitController>();
    if (!unit)
      return;

    unit.di.damage.TakeFullDamage(Vector2.zero);
  }
}