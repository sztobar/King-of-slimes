using Kite;
using System;
using UnityEngine;

public class PlayerUnitController : MonoBehaviour, IPlayerComponent
{
  public PlayerUnitDI di;
  
  [HideInInspector]
  public PlayerController mainController;

  [HideInInspector]
  public PlayerSelectable selectable;

  public void Yeet(YeetOutModule yeetModule)
  {
    transform.position = yeetModule.LaunchPosition;
    di.stateMachine.SetYeetState(yeetModule.LaunchVelocity);
  }

  public void DeselectUnit()
  {
    di.stateMachine.SetInactiveState();
    di.camera.TurnOffCamera();
  }

  public void SelectUnit()
  {
    di.stateMachine.SetControlState();
    di.camera.OnUnitSelect();
  }

  public void Inject(PlayerController controller)
  {
    mainController = controller;
    di.Init(this);

    if (di.stats.IsAssembly)
      di.selectable.SetInactive();
  }

  internal void SetInactive()
  {
    di.physics.PhysicsReset();
  }

  internal void SetActive()
  {
    di.camera.OnUnitActive();
    di.physics.unstuck.FixPositionIfStuck();
  }
}
