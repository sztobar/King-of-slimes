using System;
using UnityEngine;

public class PlayerAssemblySelectable : PlayerSelectable
{
  private PlayerUnitController unitController;

  private PlayerUnitHandler unitHandler;
  private PlayerBaseStats stats;

  public override bool IsSelected { get; protected set; }
  public override bool IsActive { get; protected set; }
  public override PlayerUnitController UnitController => unitController;
  public override PlayerBaseStats Stats => stats;

  public override void Inject(PlayerUnitDI di)
  {
    unitController = di.controller;
    stats = di.stats;
    unitHandler = di.mainDi.unitHandler;
  }

  public override void Select()
  {
    IsSelected = true;
    unitController.SelectUnit();
    OnSelectChange(IsSelected);
  }

  public override void Deselect()
  {
    IsSelected = false;
    unitController.DeselectUnit();
    OnSelectChange(IsSelected);
  }

  public void MergeInside(SlimeType type)
  {
    stats.MergeInside(type);
  }

  public void YeetOutsideAndDeselect(SlimeType type)
  {
    Deselect();
    OnUnmerge(type);
    YeetOutside(type);
  }

  public void YeetOutside(SlimeType type)
  {
    PlayerUnitSelectable selectable = unitHandler.GetSelectable(type);
    selectable.UnitController.di.camera.CameraSegment = unitController.di.camera.CameraSegment;
    selectable.GetYeeted(unitController.di.yeetModule);
    selectable.SetActive();
  }

  public override void SetInactive()
  {
    unitController.SetInactive();
    gameObject.SetActive(false);
    IsActive = false;
  }

  public void SetActiveAndSelect()
  {
    SetActive();
    Select();
  }

  public override void SetActive()
  {
    if (IsActive)
    {
      return;
    }
    unitController.SetActive();
    gameObject.SetActive(true);
    IsActive = true;
  }

  public void OnUnmerge(SlimeType type)
  {
    if (!type.IsKing())
      stats.Unmerge(type);
  }
}