using System;
using System.Collections;
using UnityEngine;

public class FullAssemblyStats : PlayerBaseStats
{
  public ScriptableSlimesConfig config;
  public ScriptableUnit data;

  public int strength;
  public int hearts;

  public override ScriptableUnit Data => data;

  public override SlimeType SlimeType => SlimeType.King;

  public override bool CanMerge => true;

  public override bool IsAssembly => true;

  public override int Strength => strength;

  public override int Hearts => hearts;

  public override bool IsPoisonImmune => false;

  public override bool IsFullAssembly => true;
  public override bool IsEmptyAssembly => false;

  public override bool HasType(SlimeType type) => true;

  private void AddStatsFrom(SlimeType type)
  {
    ScriptableSlime scriptableSlime = config.Data.Get(type);
    strength += scriptableSlime.Strength;
    hearts += scriptableSlime.Hearts;
  }

  public override void MergeInside(SlimeType type)
  {
    throw new Exception("Cannot merge to full assembly");
  }

  public override void Unmerge(SlimeType type)
  {
    throw new Exception("Cannot unmerge from full assembly");
  }

  public override void Inject(PlayerUnitDI di)
  {
    strength = 0;
    hearts = 0;
    foreach (SlimeType type in SlimeTypeHelpers.GetEnumerable())
      AddStatsFrom(type);
  }
}
