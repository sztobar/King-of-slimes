using System.Collections;
using UnityEngine;
using Bt = BtStatus;

public class BtStateMachine {

  public IBtState currentState;

  public Bt StateUpdate(IBtState state) {
    if (currentState != state) {
      if (currentState != null) {
        currentState.StateExit();
      }
      currentState = state;
      currentState.StateStart();
      return currentState.StateUpdate();
    } else {
      return currentState.StateUpdate();
    }
  }
}
