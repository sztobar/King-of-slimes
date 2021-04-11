using System;
using System.Collections;
using UnityEngine;
using WispAnimation = UnityAnimatorStates.King_Wisp;

[ExecuteAlways]
public class SpawnWispRenderer : MonoBehaviour
{
  public SlimeType SlimeType = SlimeType.King;

  public Animator animator;
  public EasyAnimator easyAnimator;
  public SpriteRenderer spriteRenderer;

  public SlimeMap<RuntimeAnimatorController> animatorsMap;
  public SlimeMap<Sprite> spritesMap;

  private void Awake()
  {
    if (Application.isPlaying)
      easyAnimator.OnAnimationEnd += OnAnimationEnd;
  }

  private void OnAnimationEnd(int animatorStateHash)
  {
    if (animatorStateHash == WispAnimation.Appear)
      easyAnimator.Play(WispAnimation.Idle);
  }

  public void PlayIdle()
  {
    easyAnimator.Play(WispAnimation.Idle);
  }

  public void PlayAppear()
  {
    if (!spriteRenderer.enabled)
    {
      spriteRenderer.enabled = true;
      easyAnimator.Play(WispAnimation.Appear);
    }
  }

  public void SetOff()
  {
    spriteRenderer.enabled = false;
    easyAnimator.Stop();
  }

  public void Update()
  {
    if (!Application.isPlaying)
    {
      EditorUpdate();
    }
  }

  private void EditorUpdate()
  {
    if (spritesMap != null && spritesMap.Get(SlimeType))
    {
      spriteRenderer.sprite = spritesMap.Get(SlimeType);
    }
    if (animatorsMap != null && animatorsMap.Get(SlimeType))
    {
      animator.runtimeAnimatorController = animatorsMap.Get(SlimeType);
    }
  }
}
