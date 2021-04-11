using System;
using System.Collections;
using UnityEngine;

public abstract class PlayerBaseStats : MonoBehaviour, IPlayerUnitComponent
{
  public abstract ScriptableUnit Data { get; }
  public abstract SlimeType SlimeType { get; }
  public delegate void OnStatsChange(PlayerBaseStats stats);
  public virtual OnStatsChange OnChange { get; set; } = null;
  public abstract bool CanMerge { get; }
  public abstract bool IsAssembly { get; }
  public abstract int Strength { get; }
  public abstract int Hearts { get; }
  public abstract bool IsPoisonImmune { get; }
  public abstract bool IsFullAssembly { get; }
  public abstract bool IsEmptyAssembly { get; }

  public abstract bool HasType(SlimeType type);

  public abstract void MergeInside(SlimeType type);

  public abstract void Unmerge(SlimeType type);
  public abstract void Inject(PlayerUnitDI di);
}