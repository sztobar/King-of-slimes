using UnityEngine;

public class SnailPuffCollider : MonoBehaviour, ISnailComponent
{
  public EnemyPlayerDetect detect;

  private SnailAnimator animator;
  private SnailPhysics physics;
  private bool hasPuffed;
  private float puffTimeLeft;

  private void Update()
  {
    if (detect.HasAnyPlayer())
    {
      if (!hasPuffed)
      {
        hasPuffed = true;
        puffTimeLeft = physics.puffDelay;
        AudioSingleton.PlaySound(AudioSingleton.Instance.clips.puff);
        animator.SetState(SnailAnimatorState.Puff);
      }
    }
    else if (puffTimeLeft > 0)
    {
      puffTimeLeft -= Time.deltaTime;
    }
    else
    {
      hasPuffed = false;
      animator.SetState(SnailAnimatorState.Walk);
    }
  }

  public void Inject(SnailController controller)
  {
    animator = controller.di.animator;
    physics = controller.di.physics;
    if (!controller.di.physics.canPuff)
    {
      gameObject.SetActive(false);
    }
  }
}