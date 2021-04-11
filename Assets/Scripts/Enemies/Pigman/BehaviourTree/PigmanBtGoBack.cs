using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using Bt = BtStatus;

public class PigmanBtGoBack : MonoBehaviour, IPigmanBtNode {

  private PigmanStateMachine stateMachine;

  public Bt BtUpdate() {
    if (stateMachine.goBack.IsAtStart()) {
      return Bt.Success;
    }
    return stateMachine.GoBackUpdate();
  }

  public void Inject(PigmanController controller) {
    stateMachine = controller.di.stateMachine;
  }
}
