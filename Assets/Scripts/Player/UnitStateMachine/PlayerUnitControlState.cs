using UnityEngine;
using System.Collections;
using Kite;

public class PlayerUnitControlState : MonoBehaviour, IPlayerUnitState {

  private PlayerUnitStateMachine stateMachine;

  private PlayerSelectUnitAbility selectUnit;
  private PlayerMovementAbility movement;
  private PlayerJumpAbility jump;
  private PlayerYeetAbility yeet;
  private PlayerWallSlideAbility wallSlide;
  private PlayerPhysics physics;
  private BasePlayerActionAbility action;

  private IPlayerAbility[] abilities;

  public void Inject(PlayerUnitDI di) {
    stateMachine = di.stateMachine;

    PlayerAbilities abilities = di.abilities;
    selectUnit = abilities.selectUnit;
    movement = abilities.movement;
    jump = abilities.jump;
    wallSlide = abilities.wallSlide;
    yeet = abilities.yeet;
    action = abilities.action;
    physics = di.physics;

    this.abilities = new IPlayerAbility[] {
      action,
      selectUnit,
      yeet,
      wallSlide,
      movement,
      jump
    };
  }

  public void StartState() {
    movement.ResetVelocity();
  }

  public void UpdateState() {
    for (int i = 0; i < abilities.Length; i++) {
      abilities[i].ControlUpdate();
      if (stateMachine.ChangedState) {
        break;
      }
    }
    physics.ControlUpdate();
  }

  public void ExitState() {
    action.OnControlExit();
  }

  public interface IAbility {
    void ControlUpdate();
  }
}
