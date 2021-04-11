using UnityEngine;

using HeartAnimation = UnityAnimatorStates.HeartUI;

public class HeartUI : MonoBehaviour
{
  public EasyAnimator easyAnimator;
  public Animator animator;
  public SpriteRenderer spriteRenderer;

  private void Awake()
  {
    easyAnimator.OnAnimationEnd += OnAnimationEnd;
  }

  private void OnAnimationEnd(int animatorStateHash)
  {
    if (animatorStateHash == HeartAnimation.LooseHP)
      easyAnimator.Play(HeartAnimation.EmptyHP);
  }

  public void Show()
  {
    spriteRenderer.enabled = true;
  }

  public void PlayRegen()
  {
    easyAnimator.Play(HeartAnimation.Regenerating);
    //animator.SetBool(isRegenHash, isRegen);
  }

  public void PlayFull()
  {
    easyAnimator.Play(HeartAnimation.Full);
    //animator.SetBool(isEmptyHash, false);
    //spriteRenderer.enabled = true;
  }

  public void Hide()
  {
    spriteRenderer.enabled = false;
  }

  public void PlayEmpty()
  {
    easyAnimator.Play(HeartAnimation.EmptyHP);
    //animator.SetBool(isEmptyHash, true);
    //spriteRenderer.enabled = true;
  }

  public void PlayLost()
  {
    easyAnimator.Play(HeartAnimation.LooseHP);
  }

}
