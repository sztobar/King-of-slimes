using System.Collections;
using UnityEngine;
using Kite;
using System;

public class RockbatStateMachine : MonoBehaviour, IRockbatComponent
{
  public RockbatSleepState sleep;
  public RockbatFlyState fly;
  public RockbatHitState hit;

  private readonly StateMachine fsm = new StateMachine();

  private void FixedUpdate()
  {
    fsm.UpdateState();
  }

  public void Inject(Rockbat rockbat)
  {
    foreach (var state in GetStates())
      state.Inject(rockbat);
    Play(sleep);
  }

  public void PlayFly() => Play(fly);
  public void PlayHit() => Play(hit);
  public bool IsHit() => fsm.IsState(hit);

  private void Play(IState state) => fsm.TransitionToState(state);

  private IRockbatState[] GetStates() =>
    new IRockbatState[]
    {
      sleep,
      fly,
      hit
    };
}
