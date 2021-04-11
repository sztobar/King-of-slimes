using System;
using System.Collections;
using UnityEngine;

public class PlayerAssemblyStats : PlayerBaseStats
{
  public ScriptableSlimesConfig config;
  public ScriptableUnit assemblyData;
  public SlimeMap<bool> mergedSlimes;

  public int strength;
  public int hearts;
  public bool isFullAssembly;
  public bool isEmptyAssembly;

  public override OnStatsChange OnChange { get; set; } = _ => { };

  public override ScriptableUnit Data => assemblyData;

  public override SlimeType SlimeType => SlimeType.King;

  public override bool HasType(SlimeType type) => mergedSlimes.Get(type);

  public override void MergeInside(SlimeType type)
  {
    if (type.IsKing())
    {
      throw new Exception("Cannot Merge King inside");
    }
    mergedSlimes.Set(type, true);
    RecalculateState();
  }

  public override void Unmerge(SlimeType type)
  {
    if (type.IsKing())
    {
      throw new Exception("Cannot Yeet King outside");
    }
    mergedSlimes.Set(type, false);
    RecalculateState();
  }

  private void RecalculateState()
  {
    strength = 0;
    hearts = 0;
    isFullAssembly = true;
    isEmptyAssembly = true;
    foreach (SlimeType type in SlimeTypeHelpers.GetEnumerable())
    {
      if (mergedSlimes.Get(type))
      {
        AddStatsFrom(type);
        if (!type.IsKing())
        {
          isEmptyAssembly = false;
        }
      }
      else
      {
        isFullAssembly = false;
      }
    }
    OnChange(this);
  }

  private void AddStatsFrom(SlimeType type)
  {
    ScriptableSlime scriptableSlime = config.Data.Get(type);
    strength += scriptableSlime.Strength;
    hearts += scriptableSlime.Hearts;
  }

  public bool IsEmpty()
  {
    bool hasHeart = mergedSlimes.Get(SlimeType.Heart);
    bool hasSword = mergedSlimes.Get(SlimeType.Sword);
    bool hasShield = mergedSlimes.Get(SlimeType.Shield);
    return !hasHeart && !hasSword && !hasShield;
  }

  public override bool CanMerge => true;

  public override bool IsAssembly => true;

  public override bool IsFullAssembly => isFullAssembly;
  public override bool IsEmptyAssembly => isEmptyAssembly;

  public override int Strength => strength;

  public override int Hearts => hearts;

  public override bool IsPoisonImmune => false;

  public override void Inject(PlayerUnitDI di)
  {
  }
}
