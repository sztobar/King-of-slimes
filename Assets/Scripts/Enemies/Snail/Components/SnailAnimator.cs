using UnityEngine;

public class SnailAnimator : MonoBehaviour, ISnailComponent {

  private readonly int stateHash = Animator.StringToHash("State");

  public Animator animator;

  public void SetState(SnailAnimatorState state) {
    animator.SetInteger(stateHash, (int)state);
  }

  public bool IsState(SnailAnimatorState state) =>
    animator.GetInteger(stateHash) == (int)state;
  
  public void Inject(SnailController controller) {
  }
}