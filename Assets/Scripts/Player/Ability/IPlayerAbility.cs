using UnityEngine;
using System.Collections;

public interface IPlayerAbility :
  PlayerUnitControlState.IAbility,
  PlayerUnitInactiveState.IAbility,
  PlayerUnitWallSlideState.IAbility,
  IPlayerUnitComponent { }
