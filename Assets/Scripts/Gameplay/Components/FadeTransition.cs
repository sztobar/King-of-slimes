using Kite;
using System;
using UnityEngine;

public class FadeTransition : MonoBehaviour, IGameplayComponent
{
  public Envelope envelope;
  public CanvasGroup canvasGroup;

  public AnimationCurve fadeCurve;
  public float fadeTime;
  private float elapsedTime;

  [HideInInspector] public Phase phase;

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

  public void StartFadeIn()
  {
    if (phase == Phase.None)
    {
      //envelope.Start();
      elapsedTime = 0;
      phase = Phase.FadeIn;
    }
  }

  public void StartFadeOut()
  {
    //envelope.Stop();
    elapsedTime = 0;
    phase = Phase.FadeOut;
  }

  public bool HasFadedIn() => phase == Phase.Faded;
  public bool HasFadedOut() => phase == Phase.None;
  public bool IsPlaying() => phase != Phase.None;
  public bool IsFadeIn() => phase == Phase.FadeIn;
  public bool IsFadeOut() => phase == Phase.FadeOut;

  public void TransitionUpdate(float dt)
  {
    if (IsProgressingPhase())
    {
      if (elapsedTime >= fadeTime)
      {
        phase = GetNextPhase(phase);
        elapsedTime = 0;
      }
      else
      {
        elapsedTime += dt;
      }
    }
    canvasGroup.alpha = GetCanvasAlphaByPhase();
    //EnvelopeUpdate(dt);
  }

  public void ResetFadeByPhase()
  {
    phase = Phase.None;
    elapsedTime = 0;
    canvasGroup.alpha = 0;
  }

  private bool IsProgressingPhase() => phase == Phase.FadeIn || phase == Phase.FadeOut;

  public float GetTime() =>
    Mathf.Clamp01(phase switch
    {
      Phase.FadeIn => elapsedTime / (fadeTime * 2),
      Phase.Faded => 0.5f,
      Phase.FadeOut => (fadeTime + elapsedTime) / (fadeTime * 2),
      Phase.Finished => 1f,
      _ => 0,
    });

  private float GetCanvasAlphaByPhase() =>
    phase switch
    {
      Phase.FadeIn => fadeCurve.Evaluate(elapsedTime / fadeTime),
      Phase.Faded => 1,
      Phase.FadeOut => 1 - fadeCurve.Evaluate(elapsedTime / fadeTime),
      Phase.Finished => 0,
      _ => 0,
    };

  private static Phase GetNextPhase(Phase phase) =>
    phase switch
    {
      //Phase.None => Phase.FadeIn,
      //Phase.FadeIn => Phase.Faded,
      //Phase.Faded => Phase.FadeOut,
      Phase.FadeIn => Phase.FadeOut,
      Phase.FadeOut => Phase.Finished,
      _ => Phase.None
    };

  private void EnvelopeUpdate(float dt)
  {
    envelope.Update(dt);

    if (phase == Phase.FadeIn)
    {
      if (envelope.Phase == EnvelopePhase.Sustain)
        phase = Phase.Faded;
      else if (envelope.Phase == EnvelopePhase.None)
        phase = Phase.None;
    }
    else if (phase == Phase.FadeOut)
    {
      if (envelope.Phase == EnvelopePhase.None)
        phase = Phase.None;
      else if (envelope.Phase == EnvelopePhase.Sustain)
        phase = Phase.Faded;
    }
    canvasGroup.alpha = envelope.Value;
  }

  public void SetFadeIn(float amount)
  {
    phase = Phase.FadeIn;
    elapsedTime = amount * fadeTime;
    canvasGroup.alpha = GetCanvasAlphaByPhase();
    //envelope.SetNormalizedTime(amount, EnvelopePhase.Attack);
    //canvasGroup.alpha = envelope.Value;
  }

  public void SetFadeOut(float amount)
  {
    phase = Phase.FadeOut;
    elapsedTime = amount * fadeTime;
    canvasGroup.alpha = GetCanvasAlphaByPhase();
    //envelope.SetNormalizedTime(amount, EnvelopePhase.Release);
    //canvasGroup.alpha = envelope.Value;
  }

  public void Clear()
  {
    phase = Phase.None;
    elapsedTime = 0;
    //envelope.Reset();
    canvasGroup.alpha = 0;
  }

  public void Inject(GameplayManager gameplayManager)
  {
    //gameObject.SetActive(false);
  }

  public enum Phase
  {
    None,
    FadeIn,
    Faded,
    FadeOut,
    Finished
  }
}