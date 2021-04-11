using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kite
{
  public class StackFSM<T> : MonoBehaviour, ISerializationCallbackReceiver where T : StackFSMState
  {
    public bool debug;
    public Stack<T> states = new Stack<T>();
    private List<T> statesList;

    public T Head => states.Peek();

    public bool IsHead(T state) => Head == state;

    public void PushState(T state)
    {
      if (debug)
        Debug.Log($"[StackFSM] PushState {state}");

      if (!IsHead(state))
      {
        Head.StatePause();
        states.Push(state);
        state.StateStart();
      }
    }

    public void PopState()
    {
      if (debug)
        Debug.Log($"[StackFSM] PopState {states.Peek()}");

      states.Pop().StateExit();
      Head.StateResume();
    }

    public void SetState(T state)
    {
      if (debug)
        Debug.Log($"[StackFSM] SetState {state}");

      if (states.Count > 0)
      {
        states.Pop().StateExit();
      }
      states.Push(state);
      state.StateStart();
    }


    public void OnBeforeSerialize()
    {
      statesList = new List<T>(states);
    }

    public void OnAfterDeserialize()
    {
      if (statesList != null)
      {
        states = new Stack<T>(statesList);
        statesList = null;
      }
      else
      {
        states = new Stack<T>();
      }
    }

    protected void Update() => Head.StateUpdate();

    protected void FixedUpdate() => Head.StateFixedUpdate();
  }
}