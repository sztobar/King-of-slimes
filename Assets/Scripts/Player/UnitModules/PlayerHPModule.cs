using System;
using UnityEngine;

public class PlayerHPModule : MonoBehaviour, IPlayerUnitComponent
{
  public float combatModeExitTime = 1f;
  public float hpRegenTime = 5f;

  private float regenTimeElapsed;
  private float combatModeExitTimeLeft;

  private bool poisonBlockingRegen;
  private int maxHP;
  private int currentHP;
  private PlayerUnitController controller;

  public bool IsFull() => currentHP == maxHP;
  public bool IsDead => currentHP <= 0;
  public int CurrentHP => currentHP;
  public int MaxHP => maxHP;
  public bool IsInCombatMode() => combatModeExitTimeLeft > 0 && currentHP != maxHP;
  public Action<HpChange> OnHpUpdate { get; set; } = _ => { };

  public void Inject(PlayerUnitDI di)
  {
    controller = di.controller;

    PlayerBaseStats stats = di.stats;
    maxHP = stats.Hearts;
    currentHP = maxHP;
    OnHpUpdate(HpChange.Update);
    if (stats.OnChange != null)
      stats.OnChange += OnStatsChange;
  }

  public void OnRespawn()
  {
    currentHP = maxHP;
    combatModeExitTimeLeft = 0;

    OnHpUpdate(HpChange.Update);
  }

  private void OnStatsChange(PlayerBaseStats stats)
  {
    int maxHPDifference = stats.Hearts - maxHP;
    currentHP = Mathf.Clamp(currentHP + maxHPDifference, 0, stats.Hearts);
    maxHP = stats.Hearts;

    OnHpUpdate(HpChange.Update);
  }

  private void Update()
  {
    if (poisonBlockingRegen)
      return;

    if (combatModeExitTimeLeft > 0)
    {
      combatModeExitTimeLeft -= Time.deltaTime;
      if (combatModeExitTimeLeft <= 0)
      {
        regenTimeElapsed = 0;
        OnHpUpdate(HpChange.Update);
      }

      return;
    }

    if (currentHP != maxHP)
    {
      regenTimeElapsed += Time.deltaTime;
      if (regenTimeElapsed >= hpRegenTime)
      {
        currentHP = maxHP;
        regenTimeElapsed -= hpRegenTime;

        OnHpUpdate(HpChange.Update);
      }
    }
  }

  public void PoisonUnlockRegen()
  {
    poisonBlockingRegen = false;
    regenTimeElapsed = 0;
  }

  public void PoisonBlockRegen()
  {
    poisonBlockingRegen = true;
  }

  public void TakeDamage(int damage)
  {
    if (controller.mainController.godMode)
      return;

    if (currentHP > 0)
    {
      combatModeExitTimeLeft = combatModeExitTime;
      currentHP = Mathf.Max(currentHP - damage, 0);

      OnHpUpdate(HpChange.Damage);
    }
  }

  public enum HpChange
  {
    Update,
    Damage
  }
}