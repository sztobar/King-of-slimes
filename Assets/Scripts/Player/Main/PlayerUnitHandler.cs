using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitHandler : MonoBehaviour, IPlayerComponent
{
  private PlayerController controller;

  private PlayerActiveAssemblySelectable assemblySelectable;
  private SlimeMap<PlayerUnitSelectable> selectables;
  private SlimeType selectedType = SlimeType.King;

  public delegate void SelectChange(SlimeType type);
  public event SelectChange OnSelectChange = delegate { };
  public event Action OnMergeChange = delegate { };

  public PlayerController Controller => controller;
  public SlimeType SelectedType => selectedType;

  public PlayerUnitSelectable GetSelectable(SlimeType type) =>
    selectables.Get(type);

  public void Inject(PlayerController controller)
  {
    this.controller = controller;
    assemblySelectable = controller.di.assemblySelectable;

    selectables = new SlimeMap<PlayerUnitSelectable>();
    foreach (SlimeType slimeType in SlimeTypeHelpers.GetEnumerable())
      selectables[slimeType] = (PlayerUnitSelectable)controller.di.units[slimeType].di.selectable;
  }

  private void Start()
  {
    PlayerSelectable initialSelectable = selectables.Get(selectedType);
    initialSelectable.SetActive();
    initialSelectable.Select();
  }

  public void SelectSlime(SlimeType newActiveType)
  {
    if (selectedType == newActiveType)
      return;

    if (!selectables.Get(newActiveType).IsUnlocked)
      return;

    if (assemblySelectable.IsActive)
    {
      if (assemblySelectable.Stats.HasType(newActiveType))
      {
        if (!assemblySelectable.IsSelected)
        {
          selectables.Get(selectedType).Deselect();
          assemblySelectable.Select();
        }
      }
      else if (selectables.Get(newActiveType).IsUnlocked)
      {
        if (assemblySelectable.Stats.HasType(selectedType))
        {
          assemblySelectable.Deselect();
        }
        else
        {
          selectables.Get(selectedType).Deselect();
        }
        selectables.Get(newActiveType).Select();
      }
    }
    else if (selectables.Get(newActiveType).IsUnlocked)
    {
      selectables.Get(selectedType).Deselect();
      selectables.Get(newActiveType).Select();
    }
    selectedType = newActiveType;
    OnSelectChange(selectedType);
  }

  public void RespawnOutOfAssembly(IEnumerable<SlimeType> types)
  {
    foreach (SlimeType type in types)
    {
      RespawnUnitOutOfAssembly(type);
    }
    OnMergeChange();
  }

  private void RespawnUnitOutOfAssembly(SlimeType type)
  {
    if (type == SlimeType.King)
      throw new Exception("Cannot respawn type king out of assembly");

    if (!assemblySelectable.Stats.HasType(type))
      throw new Exception($"Cannot respawn type {type} out of assembly, its not merged");

    if (type == selectedType)
    {
      assemblySelectable.Deselect();
      assemblySelectable.Unmerge(type);
      selectables.Get(type).SetActiveAndSelectByRespawn();
    }
    else
    {
      assemblySelectable.Unmerge(type);
      selectables.Get(type).SetActiveByRespawn();
    }
  }

  public void OnSlimeMerge(SlimeType typeToMerge)
  {
    if (typeToMerge == SlimeType.King)
      throw new Exception("Cannot merge slime type king");

    if (assemblySelectable.IsActive)
    {
      if (assemblySelectable.Stats.HasType(typeToMerge))
      {
        // double collision call (controller & otherController)
        return;
      }

      PlayerUnitSelectable selectableToMerge = selectables.Get(typeToMerge);
      bool selectAssembly = selectableToMerge.IsSelected;
      selectableToMerge.SetInactiveAndDeselect();
      assemblySelectable.MergeInside(typeToMerge);
      assemblySelectable.SetActive();
      if (selectAssembly)
        assemblySelectable.Select();
    }
    else
    {
      PlayerUnitSelectable selectableToMerge = selectables.Get(typeToMerge);
      bool selectAssembly = selectableToMerge.IsSelected;
      selectableToMerge.SetInactiveAndDeselect();
      PlayerUnitSelectable kingSelectable = selectables.Get(SlimeType.King);
      selectAssembly = selectAssembly || kingSelectable.IsSelected;
      kingSelectable.SetInactiveAndDeselect();
      assemblySelectable.MergeInside(typeToMerge);
      assemblySelectable.SetActive();
      
      if (selectAssembly)
        assemblySelectable.Select();
    }
    AudioSingleton.PlaySound(AudioSingleton.Instance.clips.merge);
    OnMergeChange();
  }

  public void OnSlimeYeet(SlimeType gotYeetd)
  {
    if (gotYeetd == SlimeType.King)
    {
      assemblySelectable.YeetKingOutside();
    }
    else
    {
      assemblySelectable.YeetOutsideAndDeselect(gotYeetd);
    }
    selectables.Get(gotYeetd).Select();
    selectedType = gotYeetd;
    OnMergeChange();
  }

  public PlayerUnitController GetSelectedUnitController()
  {
    if (assemblySelectable.IsActive && assemblySelectable.IsSelected)
    {
      return assemblySelectable.UnitController;
    }
    return selectables.Get(selectedType).UnitController;
  }
}
