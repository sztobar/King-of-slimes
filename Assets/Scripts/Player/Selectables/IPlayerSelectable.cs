using System;

public interface IPlayerSelectable
{
  bool IsActive { get; }
  bool IsSelected { get; }
  PlayerBaseStats Stats { get; }

  void Deselect();
  void Select();
  void SetActive();
  void SetInactive();
}