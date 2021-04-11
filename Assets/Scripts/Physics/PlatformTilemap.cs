using Kite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformTilemap : MonoBehaviour
{
  public List<TileBase> tiles;
  public PlatformEffector effector;

  public bool MatchForCollidable(TileBase tile) => Match(tile);
  public bool MatchForEffector(TileBase tile) => Match(tile);
  private bool Match(TileBase tile) => tiles.Contains(tile);

  public float TileGetAllowedMoveInto(PhysicsMove move) =>
    PlatformCollidableHelpers.GetGridAllowedMovementInto(move,effector);

  public void TileOnMoveInto(PhysicsMove move)
  {
    //PlatformSkippable platfromSkippable = wantsToMove.GetComponent<PlatformSkippable>();
    //if (!platfromSkippable)
    //  return;

    //float testMove = 1f;
    //float canMove = PlatformCollidableHelpers.GetGridAllowedMovementInto(wantsToMove, testMove, direction, hitpoint);
    //if (canMove == 0)
    //  platfromSkippable.IsStandingOnPlatform = true;
  }
}