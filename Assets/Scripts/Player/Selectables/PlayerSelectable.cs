using System;
using System.Collections;
using UnityEngine;

public abstract class PlayerSelectable : MonoBehaviour, IPlayerUnitComponent, IPlayerSelectable
{
  public abstract PlayerUnitController UnitController { get; }
  public abstract PlayerBaseStats Stats { get; }
  public abstract bool IsSelected { get; protected set; }
  public abstract bool IsActive { get; protected set; }

  public delegate void SelectChange(bool isSelected);
  public SelectChange OnSelectChange { get; set; } = delegate { };

  public abstract void SetInactive();
  public abstract void SetActive();
  public abstract void Select();
  public abstract void Deselect();
  public abstract void Inject(PlayerUnitDI di);

}
