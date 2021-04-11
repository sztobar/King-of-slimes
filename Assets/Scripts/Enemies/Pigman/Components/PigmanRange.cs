using System;
using System.Collections;
using UnityEngine;

public class PigmanRange : MonoBehaviour, IPigmanComponent {

  public EnemyPlayerDetect wanderRange;
  public EnemyPlayerDetect fieldOfViewRange;
  public EnemyPlayerDetect attackRange;
  public EnemyPlayerDetect prepareAttackRange;

  public void Inject(PigmanController controller) {
  }

  internal PlayerUnitController GetPlayerToFollow() {
    PlayerUnitController seenPlayer = fieldOfViewRange.GetPlayer();
    if (wanderRange.HasPlayer(seenPlayer)) {
      return seenPlayer;
    }
    return null;
  }

  internal PlayerUnitController GetSeenPlayerOutOfWander() {
    PlayerUnitController seenPlayer = fieldOfViewRange.GetPlayer();
    if (!wanderRange.HasPlayer(seenPlayer)) {
      return seenPlayer;
    }
    return null;
  }

  internal bool CanAttackPlayer() =>
    attackRange.HasAnyPlayer();
    
}
