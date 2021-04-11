using System.Collections;
using UnityEngine;

public class PigmanBtSit : MonoBehaviour, IPigmanBtNode {

  private PigmanStateMachine stateMachine;
  public BtStatus BtUpdate() {
    return stateMachine.SitUpdate();
  }

  public void Inject(PigmanController controller) {
    stateMachine = controller.di.stateMachine;
  }
}