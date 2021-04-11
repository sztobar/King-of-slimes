using System;
using System.Collections;
using UnityEngine;

public interface IPlayerStats {
  ScriptableUnit Data { get; }
  SlimeType SlimeType { get; }

  Action OnChange { get; set; }

  bool HasType(SlimeType type);
  bool CanMerge { get; }
  bool IsAssembly { get; }
  int Strength { get; }
  int Hearts { get; }
  bool IsPoisonImmune { get; }
  bool IsFullAssembly { get; }
  bool IsEmptyAssembly { get; }
}
