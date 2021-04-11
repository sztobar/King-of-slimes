using System.Collections;
using UnityEngine;

using AssemblyAnimation = UnityAnimatorStates.Chungus_Body;

public class PlayerAssemblyAnimator : BasePlayerAnimator
{
  public SlimeMap<EasyAnimator> animators;
  public SlimeMap<SpriteRenderer> spriteRenderers;

  public override void Inject(PlayerUnitDI di) { }

  public override int GetCurrentAnimation() => animators[SlimeType.King].State;
  public override void PlayAnimation(int animation)
  {
    foreach (EasyAnimator animator in animators)
      animator.Play(animation);
  }

  public override void PlayIdle() => PlayAnimation(AssemblyAnimation.Idle);
  public override void PlayWalk() => PlayAnimation(AssemblyAnimation.Run);
  public override void PlayJump() => PlayAnimation(AssemblyAnimation.Jump);
  public override void PlayFall() => PlayAnimation(AssemblyAnimation.Fall);
  public override void PlaySlide() { }// assembly cannot slide
  public override void PlayAttack() => PlayAnimation(AssemblyAnimation.Attack);
  public override void PlayPushBlock() => PlayAnimation(AssemblyAnimation.Push_Ch);


  public override void Hide()
  {
    foreach (SpriteRenderer spriteRenderer in spriteRenderers)
      spriteRenderer.enabled = false;
  }

  public override void Show()
  {
    foreach (SpriteRenderer spriteRenderer in spriteRenderers)
      spriteRenderer.enabled = true;
  }
}
