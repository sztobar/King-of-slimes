using System;
using System.Collections;
using UnityEngine;

public class SlimeInteractionPredicate : InteractionPredicate
{
  public SlimeMap<bool> canInteract;
  public SlimeInteractionType interactionType;

  public override bool CanInteract(PlayerUnitController controller)
  {
    PlayerBaseStats stats = controller.di.stats;
    SlimeType type = stats.SlimeType;
    bool isAssembly = stats.IsAssembly;
    switch (interactionType)
    {
      case SlimeInteractionType.OnlyUnitNoAssembly:
        return !isAssembly && canInteract.Get(type);

      case SlimeInteractionType.UnitOrUnitInAssembly:
        return canInteract.Get(type) || HasMergedAny(stats);

      case SlimeInteractionType.UnitOrAnyInAssembly:
        return canInteract.Get(type) || isAssembly;

      case SlimeInteractionType.OnlyUnitInAssembly:
        return canInteract.Get(type) && isAssembly;

      case SlimeInteractionType.OnlyAllUnitsInAssembly:
        return isAssembly && HasMergedAll(stats);
    }
    return false;
  }

  private bool HasMergedAll(PlayerBaseStats stats)
  {
    foreach (SlimeType type in SlimeTypeHelpers.GetEnumerable())
    {
      if (canInteract.Get(type) && !stats.HasType(type))
      {
        return false;
      }
    }
    return true;
  }

  private bool HasMergedAny(PlayerBaseStats stats)
  {
    foreach (SlimeType type in SlimeTypeHelpers.GetEnumerable())
    {
      if (canInteract.Get(type) && stats.HasType(type))
      {
        return true;
      }
    }
    return false;
  }

  public enum SlimeInteractionType
  {
    OnlyUnitNoAssembly,
    UnitOrUnitInAssembly,
    UnitOrAnyInAssembly,
    OnlyUnitInAssembly,
    OnlyAllUnitsInAssembly
  }
}