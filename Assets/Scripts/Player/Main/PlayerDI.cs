using Kite;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDI : MonoBehaviour, IPlayerComponent
{
  [HideInInspector]
  public PlayerController controller;

  public SlimeMap<PlayerUnitController> units;
  public PlayerUnitController partialAssemblyUnit;
  public PlayerUnitController fullAssemblyUnit;
  public PlayerActiveAssemblySelectable assemblySelectable;

  public PlayerUnitHandler unitHandler;
  public PlayerPlayInput input;
  public PlayerRespawnHandler respawnHandler;
  public PlayerSpawnables spawnables;

  public void Inject(PlayerController controller)
  {
    this.controller = controller;
    foreach(IPlayerComponent component in GetComponents())
      component.Inject(controller);
  }

  public IPlayerComponent[] GetComponents() =>
    new IPlayerComponent[]
    {
      unitHandler,
      units[SlimeType.King],
      units[SlimeType.Heart],
      units[SlimeType.Sword],
      units[SlimeType.Shield],
      partialAssemblyUnit,
      fullAssemblyUnit,
      assemblySelectable,
      input,
      //respawnHandler,
      spawnables
    };
}