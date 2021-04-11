using UnityEngine;
using Kite;
using System;

public class PlayerUnitStateMachine : MonoBehaviour, IPlayerUnitComponent
{
  public PlayerUnitControlState controlState;
  public PlayerUnitInactiveState inactiveState;
  public PlayerUnitWallSlideState wallSlideState;
  public PlayerUnitYeetState yeetState;
  public PlayerUnitHitState hitState;
  public PlayerUnitDeadState deadState;
  public PlayerRecoilState recoilState;

  private PlayerSelectable selectable;
  private readonly StateMachine stateMachine = new StateMachine();

  public bool ChangedState => stateMachine.ChangedState;

  public void Inject(PlayerUnitDI di)
  {
    selectable = di.selectable;
    foreach (IPlayerUnitState state in GetStates())
      state.Inject(di);
  }

  private void Awake()
  {
    SetInactiveState();
  }

  private void FixedUpdate()
  {
    stateMachine.UpdateState();
  }

  public void SetDefaultState()
  {
    if (selectable.IsSelected)
    {
      SetControlState();
    }
    else
    {
      SetInactiveState();
    }
  }

  public void SetInactiveState()
  {
    stateMachine.TransitionToState(inactiveState);
  }

  public void SetControlState()
  {
    if (yeetState.IsYeeting())
    {
      return;
    }
    stateMachine.TransitionToState(controlState);
  }

  public void SetWallSlideState()
  {
    stateMachine.TransitionToState(wallSlideState);
  }

  public void SetYeetState(Vector2 launchVelocity)
  {
    yeetState.SetLaunchVelocity(launchVelocity);
    stateMachine.TransitionToState(yeetState);
  }

  public void SetHitState(Vector2 pushForce)
  {
    hitState.SetPushForce(pushForce);
    stateMachine.TransitionToState(hitState);
  }

  public void SetRecoilState(Vector2 recoilForce)
  {
    recoilState.SetForce(recoilForce);
    stateMachine.TransitionToState(recoilState);
  }

  public void SetDeadState()
  {
    stateMachine.TransitionToState(deadState);
  }

  private IPlayerUnitState[] GetStates() =>
    new IPlayerUnitState[]
    {
      controlState,
      inactiveState,
      wallSlideState,
      yeetState,
      hitState,
      deadState,
      recoilState,
    };
}
