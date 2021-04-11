using System;
using System.Collections;
using UnityEngine;

public class PlayerUnitStats : PlayerBaseStats
{
  public ScriptableSlime data;

  public override ScriptableUnit Data => data;

  public override SlimeType SlimeType => data.Type;

  public override bool HasType(SlimeType type) => type == data.Type;

  public override bool CanMerge => data.Type.IsKing();

  public override bool IsAssembly => false;

  public override bool IsFullAssembly => false;
  public override bool IsEmptyAssembly => false;

  public override int Strength => data.Strength;

  public override int Hearts => data.Hearts;

  public override bool IsPoisonImmune => data.IsPoisonImmune;

  public override void MergeInside(SlimeType type)
  {
    throw new Exception("Cannot merge to slime unit stats");
  }
  public override void Unmerge(SlimeType type)
  {
    throw new Exception("Cannot unmerge from slime unit stats");
  }

  public override void Inject(PlayerUnitDI di)
  {
  }
}
