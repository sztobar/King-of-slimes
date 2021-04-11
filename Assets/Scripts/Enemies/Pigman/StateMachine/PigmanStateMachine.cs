using System;
using UnityEngine;

using Bt = BtStatus;

public class PigmanStateMachine : MonoBehaviour, IPigmanComponent {

  public PigmanHitState hit;
  public PigmanAttackState attack;
  public PigmanRunState run;
  public PigmanRoarState roar;
  public PigmanGoBackState goBack;
  public PigmanSitState sit;

  private readonly BtStateMachine stateMachine = new BtStateMachine();

  internal bool IsRunning(IBtState component) =>
    component == stateMachine.currentState;

  internal Bt FollowPlayerUpdate(PlayerUnitController unitToFollow) {
    run.SetTarget(unitToFollow);
    return stateMachine.StateUpdate(run);
  }

  public Bt HitUpdate() => stateMachine.StateUpdate(hit);

  internal Bt RoarUpdate() => stateMachine.StateUpdate(roar);

  public Bt AttackUpdate() => stateMachine.StateUpdate(attack);

  public Bt GoBackUpdate() => stateMachine.StateUpdate(goBack);

  public Bt SitUpdate() => stateMachine.StateUpdate(sit);

  public void Inject(PigmanController controller) {
    foreach(IPigmanState state in GetStates()) {
      state.Inject(controller);
    }
  }

  private IPigmanState[] GetStates() =>
    new IPigmanState[] {
      hit,
      attack,
      run,
      roar,
      goBack,
      sit
    };
}
