using System.Collections;
using UnityEngine;

using AssemblyAnimation = UnityAnimatorStates.Chungus_Body;

public class FullAssemblyAnimator : BasePlayerAnimator
{
  public Animator animator;
  public SpriteRenderer spriteRenderer;
  public EasyAnimator easyAnimator;
  public RuntimeAnimatorController normal;
  public RuntimeAnimatorController guarding;

  private BasePlayerActionAbility action;

  private bool isGuarding;

  public override void Inject(PlayerUnitDI di)
  {
    action = di.abilities.action;
    action.OnIsGuardingChange += OnIsGuardingChange;
  }

  private void OnIsGuardingChange(bool isCurrentlyGuarding)
  {
    isGuarding = isCurrentlyGuarding;
    UpdateAnimator();
  }

  void UpdateAnimator()
  {
    int currentAnimation = GetCurrentAnimation();

    if (isGuarding)
      animator.runtimeAnimatorController = guarding;
    else
      animator.runtimeAnimatorController = normal;

    PlayAnimation(currentAnimation);
  }

  public override int GetCurrentAnimation() => easyAnimator.State;
  public override void PlayAnimation(int animation) => easyAnimator.Play(animation);

  public override void PlayIdle() => PlayAnimation(AssemblyAnimation.Idle);
  public override void PlayWalk() => PlayAnimation(AssemblyAnimation.Run);
  public override void PlayJump() => PlayAnimation(AssemblyAnimation.Jump);
  public override void PlayFall() => PlayAnimation(AssemblyAnimation.Fall);
  public override void PlaySlide() { }// assembly cannot slide
  public override void PlayAttack() => PlayAnimation(AssemblyAnimation.Attack);
  public override void PlayPushBlock() => PlayWalk();

  public override void Hide() =>
    spriteRenderer.enabled = false;

  public override void Show() =>
    spriteRenderer.enabled = true;
}