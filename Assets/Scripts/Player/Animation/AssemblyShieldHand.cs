using System;
using System.Collections;
using UnityEngine;

public class AssemblyShieldHand : MonoBehaviour, IPlayerAssemblyComponent
{

  [SerializeField] private Animator animator;
  [SerializeField] private RuntimeAnimatorController withShield;
  [SerializeField] private RuntimeAnimatorController noShield;
  [SerializeField] private RuntimeAnimatorController guard;
  [SerializeField] private SpriteRenderer spriteRenderer;
  [SerializeField] private int noGuardOrderInLayer = -1;
  [SerializeField] private int guardOrderInLayer = 2;

  private BasePlayerAnimator unitAnimator;
  private BasePlayerActionAbility action;

  private bool hasShield;
  private bool isGuarding;

  private void UpdateSpriteRenderer() {
    if (hasShield && isGuarding) {
      spriteRenderer.sortingOrder = guardOrderInLayer;
    } else {
      spriteRenderer.sortingOrder = noGuardOrderInLayer;
    }
  }

  void UpdateAnimator() {
    int currentAnimation = unitAnimator.GetCurrentAnimation();
    //PlayerAnimatorState state = unitAnimator.GetCurrentState();
    if (hasShield) {
      if (isGuarding) {
        animator.runtimeAnimatorController = guard;
      } else {
        animator.runtimeAnimatorController = withShield;
      }
    } else {
      animator.runtimeAnimatorController = noShield;
    }
    unitAnimator.PlayAnimation(currentAnimation);
    //unitAnimator.SetState(state);
  }

  internal void SetHasShield(bool hasShield) {
    this.hasShield = hasShield;
    UpdateAnimator();
    UpdateSpriteRenderer();
  }

  public void Inject(PlayerUnitDI di) {
    action = di.abilities.action;
    unitAnimator = di.animator;
    action.OnIsGuardingChange += OnIsGuardingChange;
  }

  private void OnIsGuardingChange(bool isCurrentlyGuarding) {
    isGuarding = isCurrentlyGuarding;
    UpdateAnimator();
    UpdateSpriteRenderer();
  }
}