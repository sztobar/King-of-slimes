using System.Collections;
using UnityEngine;
using Bt = BtStatus;

public class PigmanBtFollowPlayer : MonoBehaviour, IPigmanBtNode {

  private PigmanRange range;
  private PigmanStateMachine stateMachine;

  public Bt BtUpdate() {
    PlayerUnitController unitToFollow = range.GetPlayerToFollow();
    if (unitToFollow) {
      return stateMachine.FollowPlayerUpdate(unitToFollow);
    }
    PlayerUnitController unitToRoar = range.GetSeenPlayerOutOfWander();
    if (unitToRoar) {
      return stateMachine.RoarUpdate();
    }
    return Bt.Failure;
  }

  public void Inject(PigmanController controller) {
    range = controller.di.range;
    stateMachine = controller.di.stateMachine;
  }
}