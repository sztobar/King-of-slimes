using UnityEngine;
using System.Collections;

namespace Kite {
  /// <summary>
  /// MonoBehavior responsible for starting and stopping <see cref="ICoroutineState"/>s.<br/>
  /// It holds only one <see cref="Coroutine"/>, and whenever a new one is started, old one will be stopped
  /// </summary>
  public abstract class CoroutineStateController : MonoBehaviour {

    private readonly CoroutineStateMachine stateMachine = new CoroutineStateMachine();
    private IEnumerator runningEnumerator;
    private ICoroutineState awaitingStateTransition;

    /// <summary>
    /// Transition to new <see cref="ICoroutineState"/> and returns <see cref="IEnumerator"/> from starting it
    /// </summary>
    /// <param name="newState"></param>
    /// <returns></returns>
    protected void SetState(ICoroutineState newState) {
      // if SetState is called from inside of coroutine it won't stop it from running
      // awaitingStateTransition variable is set, so that transition can happen in next FixedUpdate
      awaitingStateTransition = newState;
    }

    /// <summary>
    /// Call Update for current State
    /// </summary>
    protected void FixedUpdate() {
      if (awaitingStateTransition != null) {
        ApplyAwaitingStateTransition();
      }
      stateMachine.UpdateState();
    }

    private void ApplyAwaitingStateTransition() {
      SetCoroutine(stateMachine.TransitionToState(awaitingStateTransition));
      awaitingStateTransition = null;
    }

    /// <summary>
    /// Starts a <see cref="Coroutine"/>, stopping the previous one
    /// </summary>
    /// <param name="enumerator">New coroutine Enumerator</param>
    private void SetCoroutine(IEnumerator enumerator) {
      if (runningEnumerator != null) {
        StopCoroutine(runningEnumerator);
      }
      if (enumerator != null) {
        StartCoroutine(enumerator);
        runningEnumerator = enumerator;
      }
    }
  }
}