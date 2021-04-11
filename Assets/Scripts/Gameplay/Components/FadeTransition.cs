using Kite;
using System;
using UnityEngine;

public class FadeTransition : MonoBehaviour
{
  public CanvasGroup canvasGroup;

  public AnimationCurve fadeCurve;
  public float fadeTime;
  private float elapsedTime;

  public void SetNormalizedTime(float t)
  {
    elapsedTime = t * fadeTime * 2;
    canvasGroup.alpha = GetCanvasAlpha();
  }

  public void FadeUpdate(float dt)
  {
    elapsedTime += dt;
    canvasGroup.alpha = GetCanvasAlpha();
  }

  public float GetNormalizedTime() => Mathf.Clamp01(elapsedTime / (fadeTime * 2));

  public float GetCanvasAlpha()
  {
    if (elapsedTime < fadeTime)
      return fadeCurve.Evaluate(elapsedTime / fadeTime);
    else
      return 1 - fadeCurve.Evaluate(elapsedTime / (fadeTime * 2));
  }

  public void ResetFade() => SetNormalizedTime(0);
}