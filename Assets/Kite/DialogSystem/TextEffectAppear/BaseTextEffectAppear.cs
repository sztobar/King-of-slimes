using System;
using TMPro;
using UnityEngine;

[Serializable]
public class BaseTextEffectAppear : ITextEffectAppear {

  [SerializeField] private float frequency = 0.05f;

  private readonly int startIndex;
  private readonly int charslength;
  private readonly TMP_Text textMesh;
  private float timeElapsed;
  private int charactersShown;
  private bool isEffectFinished;

  public BaseTextEffectAppear(TMP_Text textMesh, EffectData data) {
    startIndex = data.startIndex;
    charslength = data.endIndex - startIndex;
    frequency = data.ReadFloat("f", frequency);
    this.textMesh = textMesh;
  }

  public void Update(float deltaTime) {
    if (isEffectFinished) {
      return;
    }

    timeElapsed += deltaTime;
    if (timeElapsed >= frequency) {
      timeElapsed -= frequency;
      charactersShown++;
      OnCharactersShownChange();
    }
  }

  public void ForceUpdate() {
    charactersShown = charslength;
    OnCharactersShownChange();
  }

  public bool IsEffectEnded() {
    return isEffectFinished;
  }

  public void AnimationUpdate() {
  }

  private void OnCharactersShownChange() {
    textMesh.maxVisibleCharacters = charactersShown + startIndex;
    if (charactersShown >= charslength) {
      isEffectFinished = true;
    }
  }
}
