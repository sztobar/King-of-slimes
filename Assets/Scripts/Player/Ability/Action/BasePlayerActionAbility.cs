using System;
using System.Collections;
using UnityEngine;

public abstract class BasePlayerActionAbility : MonoBehaviour, IPlayerAbility {

  public abstract bool IsGuarding { get; }
  public abstract bool IsAttacking { get; }

  public abstract Action<bool> OnIsAttackingChange { get; set; }
  public abstract Action<bool> OnIsGuardingChange { get; set; }

  public abstract void ControlUpdate();
  public abstract void OnControlExit();

  public void InactiveUpdate() {}

  public abstract void Inject(PlayerUnitDI di);

  public void WallSlideUpdate() {}
}
