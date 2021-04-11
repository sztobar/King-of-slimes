using System;
using System.Collections;
using UnityEngine;
using RockbatAnimation = UnityAnimatorStates.Rockbat;

public class RockbatAnimator : MonoBehaviour, IRockbatComponent
{
  public EasyAnimator easyAnimator;

  public void Inject(Rockbat rockbat) { }

  public void PlayFly() => easyAnimator.Play(RockbatAnimation.Fly);
  public void PlayHit() => easyAnimator.Play(RockbatAnimation.Hit);
}
