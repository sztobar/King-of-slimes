using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoisonModule : MonoBehaviour, IPlayerUnitComponent {

  [SerializeField] private float timeToFirstPoisonHit;
  [SerializeField] private float poisonHitInterval;
  [SerializeField] private float timeToClearPoison;

  private PoisonState state = PoisonState.None;
  private float timeLeft;
  private bool immune;
  private readonly HashSet<PoisonField> insidePoisonFields = new HashSet<PoisonField>();

  private PlayerDamageModule damage;
  private PlayerHPModule hp;

  public PoisonState State => state;

  public void RemovePoisonField(PoisonField poisonField) {
    insidePoisonFields.Remove(poisonField);
  }

  public void AddPoisonField(PoisonField poisonField) {
    insidePoisonFields.Add(poisonField);
  }

  public void Inject(PlayerUnitDI di) {
    hp = di.hp;
    damage = di.damage;
    PlayerBaseStats stats = di.stats;
    OnStatsChange(stats);
    if (stats.OnChange != null)
      stats.OnChange += OnStatsChange;
  }

  private void OnStatsChange(PlayerBaseStats stats) {
    immune = stats.IsPoisonImmune;
    if (immune && state != PoisonState.None) {
      state = PoisonState.None;
    }
  }

  private void FixedUpdate() {
    if (immune) {
      return;
    }
    float poisonIntensity = GetPoisonIntensity();
    if (poisonIntensity > 0) {
      UpdateInPoison(poisonIntensity);
    } else {
      UpdateOutOfPoison();
    }
  }

  private float GetPoisonIntensity() {
    float intensity = 0;
    foreach(PoisonField field in insidePoisonFields) {
      intensity = Mathf.Max(intensity, field.Intensity);
    }
    return intensity;
  }

  private void UpdateOutOfPoison() {
    if (state == PoisonState.None) {
      return;
    }
    if (state == PoisonState.InPoisonBeforeHit || state == PoisonState.InPoisonInterval) {
      if (state == PoisonState.InPoisonInterval) {
        timeLeft = timeToClearPoison;
      } else {
        float timePercent = 1 - (timeLeft / timeToFirstPoisonHit);
        timeLeft = timePercent * timeToClearPoison;
      }
      state = PoisonState.OutOfPoison;
    } else if (state == PoisonState.OutOfPoison) {
      timeLeft -= Time.deltaTime;
      if (timeLeft <= 0) {
        hp.PoisonUnlockRegen();
        state = PoisonState.None;
      }
    }
  }

  public float GetBreathNormal() {
    switch (state) {
      case PoisonState.None:
        return 0;
      case PoisonState.InPoisonBeforeHit:
        return 1 - (timeLeft / timeToFirstPoisonHit);
      case PoisonState.OutOfPoison:
        return timeLeft / timeToClearPoison;
      case PoisonState.InPoisonInterval:
      default:
        return 1 - (timeLeft / poisonHitInterval);
    }
  }

  private void UpdateInPoison(float poisonIntensity) {
    if (state == PoisonState.None || state == PoisonState.OutOfPoison) {
      state = PoisonState.InPoisonBeforeHit;
      timeLeft = timeToFirstPoisonHit;
      hp.PoisonBlockRegen();
    } else if (state == PoisonState.InPoisonBeforeHit || state == PoisonState.InPoisonInterval) {
      timeLeft -= Time.deltaTime * poisonIntensity;
      if (timeLeft <= 0) {
        damage.TakePoisonDamage();
        state = PoisonState.InPoisonInterval;
        timeLeft = poisonHitInterval;
      }
    }
  }

  public enum PoisonState {
    None,
    InPoisonBeforeHit,
    InPoisonInterval,
    OutOfPoison
  }
}
