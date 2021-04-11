using Kite;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class TestEasyAnimator : MonoBehaviour
{
  [EasyAnimatorState(typeof(PigmanEasyAnimatorState))]
  public int state;

  public EasyAnimator easyAnimator;

  private void Awake()
  {
    GetComponent<Animator>().SetInteger("State", -1);
  }

  [Button]
  public void Stop()
  {
    easyAnimator.Stop();
  }

  [Button]
  public void Play()
  {
    easyAnimator.Play();
  }

  [Button]
  public void TestWalk()
  {
    easyAnimator.Play(UnityAnimatorStates.Pigman.Walk);
  }

  [Button]
  public void TestRiseAndWalk()
  {
    easyAnimator.Play(UnityAnimatorStates.Pigman.Rise);
    easyAnimator.Play(UnityAnimatorStates.Pigman.Walk);
  }

  [Button]
  public void TestRunAndWalk()
  {
    easyAnimator.Play(UnityAnimatorStates.Pigman.Run);
    easyAnimator.Play(UnityAnimatorStates.Pigman.Walk);
  }

  [Button]
  public void TestRun()
  {
    easyAnimator.Play(UnityAnimatorStates.Pigman.Run);
  }

  [Button]
  public void TestRise()
  {
    easyAnimator.Play(UnityAnimatorStates.Pigman.Rise);
  }

  public void EmitStandUpEnd() { }
}