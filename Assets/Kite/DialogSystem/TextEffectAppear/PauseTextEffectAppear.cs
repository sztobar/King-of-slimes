using System;
using UnityEngine;

[Serializable]
public class PauseTextEffectAppear : ITextEffectAppear {

  [SerializeField] private float length = 1f;

  private float timeElapsed;

  public PauseTextEffectAppear(EffectData data) {
    length = data.ReadFloat("l", length);
  }

  public void AnimationUpdate() {
  }

  public void ForceUpdate() {
    timeElapsed = length;
  }

  public bool IsEffectEnded() {
    return timeElapsed >= length;
  }

  public void Update(float deltaTime) {
    if (timeElapsed < length) {
      timeElapsed += deltaTime;
    }
  }
}
