using System.Collections;
using UnityEngine;

namespace Kite
{
  public class FSM<T> : MonoBehaviour where T : FSMState
  {
    public T currentState;

    public void SetState(T state)
    {
      if (currentState)
      {
        currentState.StateExit();
      }
      currentState = state;
      currentState.StateStart();
    }

    private void Update() => currentState.StateUpdate();

    private void FixedUpdate() => currentState.StateFixedUpdate();
  }
}