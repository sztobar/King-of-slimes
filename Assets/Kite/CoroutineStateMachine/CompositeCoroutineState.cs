using System.Collections;

namespace Kite {
  public abstract class CompositeCoroutineState<TController> : BaseCoroutineState<TController> where TController : CoroutineStateController {

    private readonly CoroutineStateMachine stateMachine = new CoroutineStateMachine();

    protected IEnumerator SetState(ICoroutineState newState) {
      return stateMachine.TransitionToState(newState);
    }

    public override void ExitState() {
      stateMachine.ExitState();
    }

    public override void UpdateState() {
      base.UpdateState();
      stateMachine.UpdateState();
    }
  }
}
