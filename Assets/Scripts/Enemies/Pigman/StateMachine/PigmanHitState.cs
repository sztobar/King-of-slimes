using Kite;
using UnityEngine;

using Bt = BtStatus;

public class PigmanHitState : MonoBehaviour, IPigmanState
{
  public FlashSprite flashSprite;

  private PigmanAnimator animator;

  public void StartHit()
  {
    flashSprite.StartFlash();
    animator.SetState(PigmanAnimatorState.Hit);
  }

  public void Inject(PigmanController controller)
  {
    animator = controller.di.animator;
  }

  public void StateExit()
  {
  }

  public void StateStart()
  {
    AudioSingleton.PlaySound(AudioSingleton.Instance.clips.pigmanHit);
  }

  public Bt StateUpdate()
  {
    if (flashSprite.IsFlashing)
    {
      return Bt.Running;
    }
    else
    {
      animator.SetState(PigmanAnimatorState.Idle);
      return Bt.Success;
    }
  }

}
