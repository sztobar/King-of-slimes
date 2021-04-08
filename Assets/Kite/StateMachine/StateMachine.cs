using System;

namespace Kite
{
  public class StateMachine
  {
    private IState currentState = new EmptyState();
    private IState previousState;
    private bool changedState;

    public bool ChangedState => changedState;

    public void TransitionToState(IState newState)
    {
      if (currentState == newState)
      {
        return;
      }
      currentState.ExitState();
      previousState = currentState;
      currentState = newState;
      changedState = true;
      currentState.StartState();
    }

    public void TransitionToPreviousState()
    {
      TransitionToState(previousState);
    }

    public void UpdateState()
    {
      changedState = false;
      currentState.UpdateState();
    }

    public bool IsState(IState state) => currentState == state;

    public void ExitState()
    {
      currentState.ExitState();
    }

    public void Reset()
    {
      TransitionToState(new EmptyState());
    }
  }
}
