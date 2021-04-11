using System.Collections;
using UnityEngine;
using Bt = BtStatus;

public class PigmanBtCloseCombat : MonoBehaviour, IPigmanBtNode {

  private PigmanStateMachine stateMachine;
  private PigmanRange range;

  public Bt BtUpdate() {
    if (stateMachine.attack.CanMove()) {
      if (range.CanAttackPlayer() && !stateMachine.run.IsRunning()) {
        return stateMachine.AttackUpdate();
      }
      return Bt.Failure;
    }
    return stateMachine.AttackUpdate();
  }

  public void Inject(PigmanController controller) {
    stateMachine = controller.di.stateMachine;
    range = controller.di.range;
  }
}
