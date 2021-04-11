using System;
using System.Collections;
using UnityEngine;

public class SlimeActionAbility : BasePlayerActionAbility {

  public override bool IsGuarding => false;
  public override bool IsAttacking => false;
  public override Action<bool> OnIsAttackingChange { get; set; }
  public override Action<bool> OnIsGuardingChange { get; set; }

  public override void ControlUpdate() {}
  public override void OnControlExit() { }

  public override void Inject(PlayerUnitDI di) {
  }
}
