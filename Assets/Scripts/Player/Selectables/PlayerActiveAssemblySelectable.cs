using System;
using System.Collections;
using UnityEngine;

public class PlayerActiveAssemblySelectable : MonoBehaviour, IPlayerSelectable, IPlayerComponent
{
  private PlayerAssemblySelectable partialAssemblySelectable;
  private PlayerAssemblySelectable fullAssemblySelectable;

  private PlayerAssemblySelectable currentAssembly;
  private PlayerUnitSelectable kingSelectable;

  public PlayerUnitController UnitController => currentAssembly.UnitController;
  public PlayerBaseStats Stats => currentAssembly.Stats;

  public bool IsSelected
  {
    get => currentAssembly.IsSelected;
    protected set
    {
      if (value)
      {
        currentAssembly.Select();
      }
      else
      {
        currentAssembly.Deselect();
      }
    }
  }

  public bool IsActive
  {
    get => currentAssembly.IsActive;
    protected set
    {
      if (value)
      {
        currentAssembly.SetActive();
      }
      else
      {
        currentAssembly.SetInactive();
      }
    }
  }

  public void Deselect() => currentAssembly.Deselect();

  public void Inject(PlayerController controller)
  {
    partialAssemblySelectable = (PlayerAssemblySelectable)controller.di.partialAssemblyUnit.di.selectable;
    fullAssemblySelectable = (PlayerAssemblySelectable)controller.di.fullAssemblyUnit.di.selectable;
    currentAssembly = partialAssemblySelectable;

    kingSelectable = (PlayerUnitSelectable)controller.di.units[SlimeType.King].di.selectable;
  }

  public void Select() => currentAssembly.Select();

  public void SetInactive() => currentAssembly.SetInactive();

  public void SetActiveAndSelect()
  {
    SetActive();
    Select();
  }

  // REAL STUFF BELOW:

  public void SetActive()
  {
    if (currentAssembly == fullAssemblySelectable)
    {
      ActivateInPosition(
        target: fullAssemblySelectable,
        source: partialAssemblySelectable
      );
    }
    else
    {
      ActivateInPosition(
        target: partialAssemblySelectable,
        source: kingSelectable
      );
    }
  }

  public void Unmerge(SlimeType type)
  {
    if (currentAssembly == fullAssemblySelectable)
    {
      ActivateInPosition(
        target: partialAssemblySelectable,
        source: fullAssemblySelectable
      );
      SetCurrentAssembly(partialAssemblySelectable);
      partialAssemblySelectable.OnUnmerge(type);
    }
    else
    {
      partialAssemblySelectable.OnUnmerge(type);
      if (IsEmpty(partialAssemblySelectable))
      {
        ActivateInPosition(
          target: kingSelectable,
          source: partialAssemblySelectable
        );
      }
    }
  }

  public void MergeInside(SlimeType type)
  {
    if (currentAssembly == fullAssemblySelectable)
    {
      //throw new Exception("Cannot merge to full assembly");
      // double collision call (controller & otherController)
      // don't know why, no need to throw
      return;
    }
    currentAssembly.MergeInside(type);
    if (IsFull(currentAssembly))
    {
      ActivateInPosition(
        target: fullAssemblySelectable,
        source: partialAssemblySelectable
      );
      SetCurrentAssembly(fullAssemblySelectable);
    }
  }

  public void YeetKingOutside()
  {
    if (currentAssembly == fullAssemblySelectable)
    {
      ActivateInPosition(
        target: partialAssemblySelectable,
        source: fullAssemblySelectable
      );
      SetCurrentAssembly(partialAssemblySelectable);
    }
    YeetOutModule assemblyYeet = currentAssembly.UnitController.di.yeetModule;
    currentAssembly.YeetOutside(SlimeType.King);
    foreach (SlimeType type in SlimeTypeHelpers.GetWithoutKingEnumerable())
    {
      if (currentAssembly.Stats.HasType(type))
      {
        assemblyYeet.SetNextYeetDirection();
        currentAssembly.OnUnmerge(type);
        currentAssembly.YeetOutside(type);
      }
    }
    assemblyYeet.ResetDirection();
    currentAssembly.SetInactive();
  }

  public void YeetOutsideAndDeselect(SlimeType type)
  {
    if (currentAssembly == fullAssemblySelectable)
    {
      fullAssemblySelectable.Deselect();
      ActivateInPosition(
        target: partialAssemblySelectable,
        source: fullAssemblySelectable
      );
      SetCurrentAssembly(partialAssemblySelectable);
      partialAssemblySelectable.YeetOutsideAndDeselect(type);
    }
    else
    {
      partialAssemblySelectable.YeetOutsideAndDeselect(type);
      if (IsEmpty(partialAssemblySelectable))
      {
        ActivateInPosition(
          target: kingSelectable,
          source: partialAssemblySelectable
        );
      }
    }
  }

  private void ActivateInPosition(PlayerSelectable target, PlayerSelectable source)
  {
    if (target.IsActive)
      return;

    target.UnitController.transform.position = source.UnitController.transform.position;
    target.UnitController.di.flip.Direction = source.UnitController.di.flip.Direction;
    target.UnitController.di.camera.CameraSegment = source.UnitController.di.camera.CameraSegment;

    source.SetInactive();
    target.SetActive();
    if (source.IsSelected)
    {
      source.Deselect();
      target.Select();
    }

    target.UnitController.di.physics.unstuck.FixPositionIfStuck();
  }

  private void SetCurrentAssembly(PlayerAssemblySelectable newAssembly)
  {
    PlayerAssemblySelectable oldAssembly = currentAssembly;

    if (oldAssembly.IsSelected)
    {
      oldAssembly.Deselect();
      newAssembly.Select();
    }

    currentAssembly = newAssembly;
  }

  private bool IsEmpty(PlayerAssemblySelectable assemblySelectable) =>
    assemblySelectable.UnitController.di.stats.IsEmptyAssembly;

  private bool IsFull(PlayerAssemblySelectable assemblySelectable) =>
    assemblySelectable.UnitController.di.stats.IsFullAssembly;
}