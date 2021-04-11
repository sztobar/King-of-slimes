using Kite;
using System.Collections;
using UnityEngine;

public class CameraFadeTransition : CameraTransition
{
  public FadeTransition fadeTransition;

  public CameraState from;
  public CameraState to;

  public float t;

  public CameraFadeTransition Init(CameraState from, CameraState to, float t = 0)
  {
    nextTransition = null;
    this.from = from;
    this.to = to;
    this.t = t;
    return this;
  }

  public override bool IsFinished() => t >= 1;

  public override void TransitionUpdate(float dt)
  {
    fadeTransition.FadeUpdate(dt);
    t = fadeTransition.GetNormalizedTime();
  }

  public override void TransitionStart()
  {
    fadeTransition.SetNormalizedTime(t);
  }

  public override void TransitionExit()
  {
    fadeTransition.ResetFade();
  }

  public override CameraState GetState()
  {
    if (t < 0.5)
      return from;
    else
      return to;
  }
}