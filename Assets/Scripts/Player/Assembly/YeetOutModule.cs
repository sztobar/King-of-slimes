using Kite;
using System;
using System.Collections;
using UnityEngine;

public class YeetOutModule : MonoBehaviour, IPlayerUnitComponent
{
  private static readonly int maxYeetDirection = 3;

  public Vector2 launchTileVelocity;
  public HorizontalFlipComponent flip;

  private int yeetDirection = 0;

  public Vector2 LaunchVelocity => TileHelpers.TileToWorld(launchTileVelocity) * new Vector2(flip.Value * YeetDirectionX, 1);
  public Vector2 LaunchPosition => transform.position;
  private float YeetDirectionX => yeetDirection == 0 ? 1 : Mathf.Lerp(0, -1, (yeetDirection - 1) / 2f);

  public void Inject(PlayerUnitDI di)
  {
  }

  internal void SetNextYeetDirection()
  {
    if (yeetDirection == maxYeetDirection)
      throw new Exception($"Cannot set next yeet direction. It has max value of {maxYeetDirection}. Did you forget to call .ResetDirection()?");

    yeetDirection++;
  }

  internal void ResetDirection()
  {
    yeetDirection = 0;
  }
}
