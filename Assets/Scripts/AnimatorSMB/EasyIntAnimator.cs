using Kite;
using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EasyIntAnimator : MonoBehaviour
{
  public Animator animator;

  private int pendingAnimation;
  private int currentAnimation;
  private readonly int defaultAnimation = UnityAnimatorStates.Pigman.Entry;
  private bool isPending;
  private AnimationClip currentClip;
  private int stateToPlay;

  public bool IsAnimationPending => isPending;
  public Action<int> OnAnimationEnd { get; set; } = delegate { };

  private void Awake()
  {
    animator.SetInteger("State", -1);
    Play(defaultAnimation);
    //AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
    //currentAnimation = currentState.shortNameHash;
    OnAnimationEnd += (int i) => Debug.Log("Animation eneded!");
  }

  public void Play(int animationHash)
  {
    stateToPlay = animationHash;
    pendingAnimation = animationHash;
    isPending = true;
    AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
    currentAnimation = currentState.shortNameHash;
    currentClip = null;
  }

  private void FixedUpdate()
  {
    if (stateToPlay != 0)
    {
      animator.Play(stateToPlay);
      stateToPlay = 0;
    }
    AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
    if (isPending)
    {
      if (currentState.shortNameHash == pendingAnimation)
      {
        currentAnimation = pendingAnimation;
        pendingAnimation = 0;
        isPending = false;
        currentClip = animator.GetCurrentAnimatorClipInfo(0)[0].clip;
      }
    }
    if (currentClip && !currentClip.isLooping)
    {
      if (currentState.normalizedTime >= 1)
      {
        OnAnimationEnd(currentState.shortNameHash);
        currentClip = null;
        if (!isPending)
        {
          Play(defaultAnimation);
        }
      }
    }
  }
}