using System;
using System.Collections;
using UnityEngine;

public abstract class BasePlayerAnimator : MonoBehaviour, IPlayerUnitComponent
{
  public abstract void Show();
  public abstract void Hide();
  public abstract void Inject(PlayerUnitDI di);

  public abstract void PlayIdle();
  public abstract void PlayWalk();
  public abstract void PlayJump();
  public abstract void PlayFall();
  public abstract void PlaySlide();
  public abstract void PlayAttack();
  public abstract void PlayPushBlock();
  public abstract int GetCurrentAnimation();
  public abstract void PlayAnimation(int animatorStateHash);
}
