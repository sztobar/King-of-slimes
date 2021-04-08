using System;
using UnityEditor;
using UnityEngine;

namespace Kite
{
  /// <summary>
  /// ADSR envelope struct
  /// </summary>
  [Serializable]
  public partial class Envelope
  {
    public float attackTime;
    public AnimationCurve attackCurve;

    public float decayTime;
    public AnimationCurve decayCurve;

    public float sustainTime;
    public float sustainValue;
    public bool holdSustain;

    public float releaseTime;
    public AnimationCurve releaseCurve;

    private EnvelopePhase phase;
    private float currentPhaseAmount;
    private float elapsedTime;

    public EnvelopePhase Phase => phase;
    public float Value => currentPhaseAmount;

    public void SetNormalizedTime(float normalizedTime, EnvelopePhase phase)
    {
      this.phase = phase;
      switch (phase)
      {
        case EnvelopePhase.Attack:
          elapsedTime = attackTime * normalizedTime;
          currentPhaseAmount = attackCurve.Evaluate(normalizedTime);
          break;
        case EnvelopePhase.Decay:
          elapsedTime = decayTime * normalizedTime;
          currentPhaseAmount = decayCurve.Evaluate(normalizedTime);
          break;
        case EnvelopePhase.Sustain:
          elapsedTime = sustainTime * normalizedTime;
          currentPhaseAmount = sustainValue;
          break;
        case EnvelopePhase.Release:
          elapsedTime = releaseTime * normalizedTime;
          currentPhaseAmount = releaseCurve.Evaluate(normalizedTime);
          break;
      }
    }

    public void Start()
    {
      if (phase == EnvelopePhase.None || phase == EnvelopePhase.Release)
        SetAttackPhase();
    }

    public void Stop()
    {
      if (phase == EnvelopePhase.Attack || phase == EnvelopePhase.Decay || phase == EnvelopePhase.Sustain)
        SetReleasePhase();
    }

    public void Reset() => SetNonePhase();

    public static Envelope CreateDefault() => new Envelope
    {
      phase = EnvelopePhase.None,
      attackCurve = AnimationCurve.Linear(0, 0, 1, 1),
      attackTime = 0.3f,
      decayCurve = AnimationCurve.Linear(0, 1, 1, 1),
      decayTime = 0f,
      sustainTime = 0.4f,
      sustainValue = 1f,
      releaseCurve = AnimationCurve.Linear(0, 1, 1, 0),
      releaseTime = 0.2f,
      holdSustain = false
    };

    public void Update(float dt) => InternalUpdate(dt);
    public void Update() => InternalUpdate(Time.deltaTime);
    public void FixedUpdate() => InternalUpdate(Time.fixedDeltaTime);

    private void InternalUpdate(float dt)
    {
      if (dt >= 0)
      {
        switch (phase)
        {
          case EnvelopePhase.Attack:
            AttackUpdate(dt);
            break;
          case EnvelopePhase.Decay:
            DecayUpdate(dt);
            break;
          case EnvelopePhase.Sustain:
            SustainUpdate(dt);
            break;
          case EnvelopePhase.Release:
            ReleaseUpdate(dt);
            break;
        }
      }
      else
      {
        NegativeUpdate(dt);
      }
    }

    private void AttackUpdate(float dt)
    {
      float timePercentage = elapsedTime / attackTime;
      float currentAmount = attackCurve.Evaluate(timePercentage);
      if (currentAmount > currentPhaseAmount)
      {
        currentPhaseAmount = currentAmount;
      }

      if (timePercentage >= 1)
      {
        SetDecayPhase();
      }
      else
      {
        elapsedTime += dt;
      }
    }

    private void DecayUpdate(float dt)
    {
      float timePercentage = elapsedTime / decayTime;
      float currentAmount = decayCurve.Evaluate(timePercentage);
      if (currentAmount < currentPhaseAmount)
      {
        currentPhaseAmount = currentAmount;
      }

      if (timePercentage >= 1)
      {
        SetSustainPhase();
      }
      else
      {
        elapsedTime += dt;
      }
    }

    private void SustainUpdate(float dt)
    {
      if (holdSustain)
        return;

      float timePercentage = elapsedTime / sustainTime;
      if (timePercentage >= 1)
      {
        SetReleasePhase();
      }
      else
      {
        elapsedTime += dt;
      }
    }

    private void ReleaseUpdate(float dt)
    {
      float timePercentage = elapsedTime / releaseTime;
      float currentAmount = releaseCurve.Evaluate(timePercentage);
      if (currentAmount < currentPhaseAmount)
      {
        currentPhaseAmount = currentAmount;
      }

      if (timePercentage >= 1)
      {
        SetNonePhase();
      }
      else
      {
        elapsedTime += dt;
      }
    }

    private void SetAttackPhase()
    {
      if (attackTime > 0)
      {
        phase = EnvelopePhase.Attack;
        elapsedTime = 0;
      }
      else
      {
        SetDecayPhase();
      }
    }

    private void SetDecayPhase()
    {
      if (decayTime > 0)
      {
        phase = EnvelopePhase.Decay;
        elapsedTime = 0;
      }
      else
      {
        SetSustainPhase();
      }
    }

    private void SetSustainPhase()
    {
      if (sustainTime > 0 || holdSustain)
      {
        phase = EnvelopePhase.Sustain;
        elapsedTime = 0;
        currentPhaseAmount = sustainValue;
      }
      else
      {
        SetReleasePhase();
      }
    }

    private void SetReleasePhase()
    {
      if (releaseTime > 0)
      {
        phase = EnvelopePhase.Release;
        elapsedTime = 0;
      }
      else
        SetNonePhase();
    }

    private void SetNonePhase()
    {
      phase = EnvelopePhase.None;
      currentPhaseAmount = 0;
    }

    private void NegativeUpdate(float dt)
    {
      switch (phase)
      {
        case EnvelopePhase.Attack:
          AttackNegativeUpdate(dt);
          break;
        case EnvelopePhase.Decay:
          DecayNegativeUpdate(dt);
          break;
        case EnvelopePhase.Sustain:
          SustainNegativeUpdate(dt);
          break;
        case EnvelopePhase.Release:
          ReleaseNegativeUpdate(dt);
          break;
      }
    }

    private void AttackNegativeUpdate(float dt)
    {
      if (elapsedTime < 0)
      {
        SetNonePhase();
        return;
      }

      float timePercentage = elapsedTime / attackTime;
      float currentAmount = attackCurve.Evaluate(timePercentage);
      if (currentAmount < currentPhaseAmount)
      {
        currentPhaseAmount = currentAmount;
      }
      elapsedTime += dt;
    }

    private void DecayNegativeUpdate(float dt)
    {
      if (elapsedTime < 0)
      {
        SetAttackPhaseNegative();
        return;
      }
      float timePercentage = elapsedTime / decayTime;
      float currentAmount = decayCurve.Evaluate(timePercentage);
      if (currentAmount > currentPhaseAmount)
      {
        currentPhaseAmount = currentAmount;
      }
      elapsedTime += dt;
    }

    private void SustainNegativeUpdate(float dt)
    {
      if (holdSustain)
        return;

      if (elapsedTime < 0)
      {
        SetDecayPhaseNegative();
        return;
      }
      elapsedTime += dt;
    }

    private void ReleaseNegativeUpdate(float dt)
    {
      if (elapsedTime < 0)
      {
        SetSustainPhaseNegative();
        return;
      }

      float timePercentage = elapsedTime / releaseTime;
      float currentAmount = releaseCurve.Evaluate(timePercentage);
      if (currentAmount > currentPhaseAmount)
      {
        currentPhaseAmount = currentAmount;
      }
      elapsedTime += dt;
    }

    private void SetAttackPhaseNegative()
    {
      if (attackTime > 0)
      {
        phase = EnvelopePhase.Attack;
        elapsedTime = attackTime;
      }
      else
      {
        SetNonePhase();
      }
    }

    private void SetDecayPhaseNegative()
    {
      if (decayTime > 0)
      {
        phase = EnvelopePhase.Decay;
        elapsedTime = decayTime;
      }
      else
      {
        SetAttackPhaseNegative();
      }
    }

    private void SetSustainPhaseNegative()
    {
      if (sustainTime > 0 || holdSustain)
      {
        phase = EnvelopePhase.Sustain;
        elapsedTime = sustainTime;
        currentPhaseAmount = sustainValue;
      }
      else
      {
        SetDecayPhaseNegative();
      }
    }
  }
}