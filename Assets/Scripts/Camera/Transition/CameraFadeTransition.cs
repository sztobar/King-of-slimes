using Kite;
using System.Collections;
using UnityEngine;

public class CameraFadeTransition : CameraTransition
{
  private static CameraFadeTransition instance;

  public FadeTransition fadeTransition;

  public GameplayCameraState from;
  public GameplayCameraState to;

  public float t;

  public static CameraFadeTransition Create(
    GameplayCameraState from,
    GameplayCameraState to,
    float t = 0f)
  {
    if (!instance)
    {
      instance = CreateInstance<CameraFadeTransition>();
      instance.fadeTransition = GameplayManager.instance.fadeTransition;
    }
    instance.from = from;
    instance.to = to;
    instance.t = t;

    return instance;
  }

  public override bool IsFinished() => t >= 1;

  public override void TransitionUpdate(float dt)
  {
    fadeTransition.FadeUpdate(dt);
    //fadeTransition.TransitionUpdate(dt);
    t = fadeTransition.GetNormalizedTime();
  }

  public override void TransitionStart()
  {
    fadeTransition.SetNormalizedTime(t);
    //if (t < 0.5)
    //  fadeTransition.SetFadeIn(t * 2);
    //else
    //  fadeTransition.SetFadeOut((t - 0.5f) * 2);
  }

  public override void TransitionExit()
  {
    fadeTransition.ResetFade();
  }

  public override GameplayCameraState GetState()
  {
    if (t < 0.5)
      return from;
    else
      return to;
  }
}