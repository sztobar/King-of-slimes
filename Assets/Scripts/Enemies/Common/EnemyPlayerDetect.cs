using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayerDetect : MonoBehaviour
{
  public new Collider2D collider;
  public List<PlayerUnitController> playersInRange;

  internal bool HasAnyPlayer() =>
    playersInRange.Count > 0;

  internal bool HasPlayer(PlayerUnitController player) =>
    playersInRange.Contains(player);

  internal PlayerUnitController GetPlayer() =>
    playersInRange.Count > 0 ? playersInRange[0] : null;

  public List<PlayerUnitController> GetAllPlayers() =>
    playersInRange;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collision);
    if (unit)
    {
      playersInRange.Add(unit);
    }
  }


  private void OnTriggerExit2D(Collider2D collision)
  {
    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collision);
    if (unit)
    {
      playersInRange.Remove(unit);
    }
  }

  private void OnDisable()
  {
    Destroy(collider);
  }
}
