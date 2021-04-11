using Kite;
using UnityEngine;

public class PlayerUnitWallSlideState : MonoBehaviour, IPlayerUnitState {

  [SerializeField]
  [Tooltip("Fall speed when wall sliding")]
  private float wallSlideYTileVelocity = 5;

  private PlayerWallSlideAbility wallSlide;
  private PlayerPhysics physics;
  private PlayerUnitStateMachine stateMachine;
  private PlayerSelectUnitAbility selectUnit;

  public void UpdateState() {
    selectUnit.WallSlideUpdate();
    if (stateMachine.ChangedState) {
      return;
    }
    if (physics.IsGrounded) {
      stateMachine.SetControlState();
      return;
    }
    float wallSlideYVelocity = TileHelpers.TileToWorld(wallSlideYTileVelocity);
    if (physics.velocity.Y < -wallSlideYVelocity) {
      physics.velocity.Y = -wallSlideYVelocity;
    }
    wallSlide.WallSlideUpdate();
    physics.WalSlideUpdate();
  }


  public void Inject(PlayerUnitDI di) {
    stateMachine = di.stateMachine;
    selectUnit = di.abilities.selectUnit;
    wallSlide = di.abilities.wallSlide;
    physics = di.physics;
  }

  public void StartState() {
  }

  public void ExitState() {}

  public interface IAbility {
    void WallSlideUpdate();
  }
}
