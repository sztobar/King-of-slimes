using System;
using UnityEngine;

public class EasyAnimator : MonoBehaviour
{
  private static readonly int animatorLayerIndex = 0;

  public delegate void AnimatorStateEnd(int animatorStateHash);

  public Animator animator;

  private bool isPlaying;
  private int currentState;
  private int entryState;
  private bool isPending;
  private AnimationClip currentClip;
  private bool hasStateToPlay;
  private bool emitEventAtEnd;
  private EasyAnimatorPlayMode playMode = EasyAnimatorPlayMode.Default;

  public bool IsAnimationPending => isPending;
  public int State => currentState;
  public AnimatorStateEnd OnAnimationEnd { get; set; } = delegate { };

  private void Awake()
  {
    AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(animatorLayerIndex);
    currentState = entryState = currentStateInfo.shortNameHash;
  }

  public void Stop()
  {
    isPlaying = false;
    animator.enabled = false;
  }

  public void Play() => Play(currentState);

  public void Play(int stateHash, EasyAnimatorPlayMode playMode = EasyAnimatorPlayMode.Default)
  {
    CheckForIsPlaying();
    currentState = stateHash;
    isPending = true;
    hasStateToPlay = true;
    currentClip = null;
    this.playMode = playMode;
  }

  public void PlayImmediate(int stateHash, EasyAnimatorPlayMode playMode = EasyAnimatorPlayMode.Default)
  {
    Play(stateHash, playMode);
    PendingStateUpdate();
  }

  private void CheckForIsPlaying()
  {
    if (!isPlaying)
    {
      isPlaying = true;
      animator.enabled = true;
      animator.Play(currentState, animatorLayerIndex, 0);
    }
  }

  private void Update()
  {
    if (!isPlaying)
      StoppedUpdate();
    else if (isPending)
      PendingStateUpdate();
    else
      CurrentStateUpdate();
  }

  private void StoppedUpdate()
  {
  }

  private void CurrentStateUpdate()
  {
    if (emitEventAtEnd)
    {
      AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(animatorLayerIndex);
      if (currentStateInfo.normalizedTime >= 1)
      {
        bool playEntryState = playMode == EasyAnimatorPlayMode.End ||
          (playMode == EasyAnimatorPlayMode.Default && !currentClip.isLooping);

        OnAnimationEnd(currentStateInfo.shortNameHash);
        emitEventAtEnd = false;

        if (!isPending && isPlaying && playEntryState)
        {
          Play(entryState);
        }
      }
    }
  }

  private void PendingStateUpdate()
  {
    if (hasStateToPlay)
    {
      animator.Play(currentState);
      hasStateToPlay = false;
    }
    else
    {
      AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(animatorLayerIndex);
      if (currentStateInfo.shortNameHash == currentState)
      {
        isPending = false;
        currentClip = animator.GetCurrentAnimatorClipInfo(animatorLayerIndex)[0].clip;

        bool isLooping = playMode == EasyAnimatorPlayMode.Loop ||
          (playMode == EasyAnimatorPlayMode.Default && currentClip.isLooping);
        emitEventAtEnd = !isLooping;
      }
    }
  }

  //void OnBecameVisible()
  //{
  //  enabled = true;
  //}

  //void OnBecameInvisible()
  //{
  //  enabled = false;
  //}
}