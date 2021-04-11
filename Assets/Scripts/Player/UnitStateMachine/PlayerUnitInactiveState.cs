using UnityEngine;
using System.Collections;
using Kite;

public class PlayerUnitInactiveState : MonoBehaviour, IPlayerUnitState {

  private PlayerMovementAbility movement;
  private PlayerJumpAbility jump;
  private PlayerWallSlideAbility wallSlide;
  private PlayerPhysics physics;

  public void Inject(PlayerUnitDI di) {
    PlayerAbilities abilities = di.abilities;
    movement = abilities.movement;
    jump = abilities.jump;
    wallSlide = abilities.wallSlide;
    physics = di.physics;
  }

  public void StartState() {}

  public void UpdateState() {
    movement.InactiveUpdate();
    jump.InactiveUpdate();
    wallSlide.InactiveUpdate();
    physics.InactiveUpdate();
  }

  public void ExitState() {}

  public interface IAbility {
    void InactiveUpdate();
  }
}
