using System;
using System.Collections;
using UnityEngine;

public class AssemblySwordHand : MonoBehaviour, IPlayerAssemblyComponent
{

  [SerializeField] private Animator animator;
  [SerializeField] private RuntimeAnimatorController withSword;
  [SerializeField] private RuntimeAnimatorController noSword;

  public void Inject(PlayerUnitDI di) {
  }

  internal void SetStats(bool hasShield) {
    if (hasShield) {
      animator.runtimeAnimatorController = withSword;
    } else {
      animator.runtimeAnimatorController = noSword;
    }
  }
}