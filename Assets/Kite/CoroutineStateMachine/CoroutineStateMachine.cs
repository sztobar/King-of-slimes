using System.Collections;

namespace Kite {
  public class CoroutineStateMachine {

    private ICoroutineState currentState = new EmptyCoroutineState();

    public IEnumerator TransitionToState(ICoroutineState newState) {
      currentState.ExitState();
      currentState = newState;
      return currentState.StartState();
    }

    public void UpdateState() {
      currentState.UpdateState();
    }

    public void ExitState() {
      currentState.ExitState();
    }
  }
}
