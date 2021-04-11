using Kite;
using System;
using System.Collections;
using UnityEngine;

public class RockbatRange : MonoBehaviour, IRockbatComponent
{
  public EnemyPlayerDetect detect;

  [Header("Debug")]
  public bool hasTarget;
  public Vector2 directionToTarget;

  private PlayerUnitController trackedPlayer;

  public PlayerUnitController Target => trackedPlayer;

  public void Inject(Rockbat rockbat) { }

  public void TrackForPlayerUpdate()
  {
    if (!hasTarget)
    {
      hasTarget = detect.HasAnyPlayer();
      if (hasTarget)
      {
        trackedPlayer = detect.GetPlayer();
      }
    }
  }

  public void DisableRange()
  {
    Destroy(detect);
  }
}
