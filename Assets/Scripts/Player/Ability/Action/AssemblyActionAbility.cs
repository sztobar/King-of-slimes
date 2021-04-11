using System;
using System.Collections;
using UnityEngine;

public class AssemblyActionAbility : BasePlayerActionAbility
{
  private BasePlayerAnimator animator;
  private PlayerInput input;

  private bool hasSword;
  private bool hasShield;
  private bool isAttacking;
  private bool isGuarding;

  public override bool IsGuarding => isGuarding;
  public override bool IsAttacking => isAttacking;
  public override Action<bool> OnIsAttackingChange { get; set; } = _ => { };
  public override Action<bool> OnIsGuardingChange { get; set; } = _ => { };

  public override void ControlUpdate()
  {
    if (isAttacking)
      return;

    if (hasSword && input.sword.IsPressed())
    {
      AudioSingleton.PlaySound(AudioSingleton.Instance.clips.swordSwing);
      input.sword.Use();
      animator.PlayAttack();
      isAttacking = true;
      OnIsAttackingChange(isAttacking);
      isGuarding = false;
      OnIsGuardingChange(isGuarding);
    }
    else
    {
      bool currentlyIsGuarding = hasShield && input.shield.IsHeld();
      if (currentlyIsGuarding != isGuarding)
      {
        isGuarding = currentlyIsGuarding;
        OnIsGuardingChange(isGuarding);
      }
    }
  }

  public override void OnControlExit()
  {
    if (isAttacking)
      OnSwordEnded();
  }

  public override void Inject(PlayerUnitDI di)
  {
    animator = di.animator;
    PlayerBaseStats stats = di.stats;
    input = di.mainDi.controller.input;
    di.animationEvents.OnSwordEnded += OnSwordEnded;
    OnStatsChange(stats);
    if (stats.OnChange != null)
      stats.OnChange += OnStatsChange;
  }

  private void OnSwordEnded()
  {
    isAttacking = false;
    OnIsAttackingChange(isAttacking);
  }

  private void OnStatsChange(PlayerBaseStats stats)
  {
    hasSword = stats.HasType(SlimeType.Sword);
    hasShield = stats.HasType(SlimeType.Shield);
  }
}
