using System.Collections;
using UnityEngine;

public class OnEnterSetStateSMB : StateMachineBehaviour {

  private static readonly int stateHash = Animator.StringToHash("State");
  public int value;

  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    animator.SetInteger(stateHash, value);
  }
}
