using System.Collections;
using UnityEngine;


public class PartialAssemblyModules : BaseAssemblyModules {

  public AssemblySwordHand swordHand;
  public AssemblyShieldHand shieldHand;
  public SpriteRenderer heart;
  public PlayerSwordCollider swordCollider;

  public override void Inject(PlayerUnitDI di) {
    PlayerBaseStats stats = di.stats;

    foreach (IPlayerAssemblyComponent component in GetComponents())
      component.Inject(di);

    stats.OnChange += OnStatsChange;
    OnStatsChange(stats);
  }

  public void OnStatsChange(PlayerBaseStats stats) {
    swordHand.SetStats(stats.HasType(SlimeType.Sword));
    shieldHand.SetHasShield(stats.HasType(SlimeType.Shield));
    heart.enabled = stats.HasType(SlimeType.Heart);
  }

  private IPlayerAssemblyComponent[] GetComponents() =>
    new IPlayerAssemblyComponent[]
    {
      swordHand,
      shieldHand,
      swordCollider
    };
}
