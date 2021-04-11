using Kite;
using System.Collections;
using UnityEngine;

public class RockbatCollider : EnemyCollider, IRockbatComponent
{
  private Rockbat controller;

  public void Inject(Rockbat rockbat) => controller = rockbat;

  protected override void OnGuarded(PlayerUnitController player)
  {
    controller.fsm.hit.Direction = RecoilHelpers.GetRecoilNormalFromTo(controller.transform, player.transform);
    controller.fsm.PlayHit();
  }

  protected override void OnStomped(PlayerUnitController player) =>
    controller.destroyable.DestroyEnemyWithPoof();

  protected override void OnTakeDamage(PlayerUnitController player) =>
    controller.destroyable.DestroyEnemyWithPoof();
}