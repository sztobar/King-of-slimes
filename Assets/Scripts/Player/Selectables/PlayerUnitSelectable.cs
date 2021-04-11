using System;
using UnityEngine;

public class PlayerUnitSelectable : PlayerSelectable
{
  public bool unlocked;

  private PlayerUnitController unitController;
  private PlayerBaseStats stats;

  public override bool IsSelected { get; protected set; }
  public override bool IsActive { get; protected set; } = true;

  public override PlayerUnitController UnitController => unitController;
  public override PlayerBaseStats Stats => stats;

  public bool IsUnlocked
  {
    get => unlocked;
    set => unlocked = value;
  }

  public override void Inject(PlayerUnitDI di)
  {
    unitController = di.controller;
    stats = di.stats;
  }

  public override void Deselect()
  {
    unitController.DeselectUnit();
    IsSelected = false;
    OnSelectChange(IsSelected);
  }

  public override void Select()
  {
    unitController.SelectUnit();
    IsSelected = true;
    OnSelectChange(IsSelected);
  }

  public void SetInactiveAndDeselect()
  {
    if (!unlocked)
    {
      unlocked = true;
    }
    SetInactive();
    Deselect();
  }

  public override void SetInactive()
  {
    unitController.SetInactive();
    gameObject.SetActive(false);
    IsActive = false;
  }

  public void SetActiveAndSelectByYeet()
  {
    SetActive();
    Select();
  }

  public void SetActiveAndSelectByRespawn()
  {
    Respawn();
    SetActive();
    Select();
  }

  public void SetActiveByRespawn()
  {
    Respawn();
    SetActive();
  }

  public override void SetActive()
  {
    if (IsActive)
    {
      Debug.LogWarning("Calling SetActive on active selectable.");
    }
    gameObject.SetActive(true);
    unitController.SetActive();
    IsActive = true;
  }

  public void GetYeeted(YeetOutModule yeet)
  {
    unitController.Yeet(yeet);
  }

  public void Respawn()
  {
    unitController.di.respawnHandler.Respawn();
  }
}
