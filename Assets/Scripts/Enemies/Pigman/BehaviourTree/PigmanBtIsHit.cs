using System.Collections;
using UnityEngine;
using BT = BtStatus;

public class PigmanBtIsHit : MonoBehaviour, IPigmanBtNode {

  private PigmanStateMachine stateMachine;

  public BT BtUpdate() {
    if (stateMachine.hit.flashSprite.IsFlashing) {
      return BtHelpers.Not(stateMachine.HitUpdate());
    }
    return BT.Failure;
  }

  public void Inject(PigmanController controller) {
    stateMachine = controller.di.stateMachine;
  }
}
