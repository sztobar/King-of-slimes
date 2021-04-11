using System;
using System.Collections;
using UnityEngine;

using SlimeAnimation = UnityAnimatorStates.King_Slime;

public class PlayerUnitAnimator : BasePlayerAnimator
{
  public EasyAnimator easyAnimator;
  public SpriteRenderer spriteRenderer;

  public override int GetCurrentAnimation() => easyAnimator.State;
  public override void PlayAnimation(int animation) => easyAnimator.Play(animation);

  public override void PlayIdle() => easyAnimator.Play(SlimeAnimation.Idle);
  public override void PlayWalk() => easyAnimator.Play(SlimeAnimation.Walk);
  public override void PlayJump() => easyAnimator.Play(SlimeAnimation.Jump);
  public override void PlayFall() => easyAnimator.Play(SlimeAnimation.Fall);
  public override void PlaySlide() => easyAnimator.Play(SlimeAnimation.Slide);
  public override void PlayAttack() { }// unit doesn't have attack
  public override void PlayPushBlock() => PlayWalk();
  
  public override void Hide() =>
    spriteRenderer.enabled = false;

  public override void Show() =>
    spriteRenderer.enabled = true;


  public override void Inject(PlayerUnitDI di)
  {
  }
}