using System;
using UnityEngine;

using TransitionAnimation = UnityAnimatorStates.Transition;

public class GameplayFadeState : GameplayState
{
  public EasyAnimator fadeAnimator;
  private GameplayStateMachine stateMachine;

  public Action betweenFadeCallback;
  public bool stopTime;

  private float timeScaleToRestore;

  public bool InProgress => fadeAnimator.gameObject.activeSelf;

  public override void StateStart()
  {
    if (stopTime)
    {
      timeScaleToRestore = Time.timeScale;
      Time.timeScale = 0f;
    }
    //fadeAnimator.gameObject.SetActive(true);

    AnimatorStateInfo currentStateInfo = fadeAnimator.animator.GetCurrentAnimatorStateInfo(0);
    if (currentStateInfo.shortNameHash == TransitionAnimation.None)
    {
      fadeAnimator.Play(TransitionAnimation.FadeIn);
      fadeAnimator.OnAnimationEnd += OnAnimationEnd;
    }
    else
    {

    }
  }

  private void OnAnimationEnd(int animatorStateHash)
  {
    switch (animatorStateHash)
    {
      case TransitionAnimation.FadeIn:
        betweenFadeCallback?.Invoke();
        fadeAnimator.Play(TransitionAnimation.FadeOut);
        break;
      case TransitionAnimation.FadeOut:
        stateMachine.PopState();
        betweenFadeCallback = null;
        break;
    }
  }

  public override void StateExit()
  {
    if (stopTime)
      Time.timeScale = timeScaleToRestore;
    fadeAnimator.OnAnimationEnd -= OnAnimationEnd;
    //fadeAnimator.gameObject.SetActive(false);
  }

  public override void Inject(GameplayManager gameplayManager)
  {
    stateMachine = gameplayManager.fsm;
    //fadeAnimator.gameObject.SetActive(false);
  }
}