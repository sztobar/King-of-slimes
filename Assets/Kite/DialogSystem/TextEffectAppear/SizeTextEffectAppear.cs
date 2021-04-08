using UnityEngine;
using System;
using TMPro;
using System.Collections.Generic;

[Serializable]
public class SizeTextEffectAppear : ITextEffectAppear {

  [SerializeField] private float frequency = 0.05f;
  [SerializeField] private float amplitude = 2f;
  [SerializeField] private float length = 2f;

  private readonly List<SizeCharEffect> sizeCharEffects = new List<SizeCharEffect>();
  private readonly int startIndex;
  private readonly int charslength;
  private readonly TMP_Text textMesh;
  private float frequencyTimeElapsed;
  private float effectTimeElapsed;
  private int charactersShown;
  private bool isEffectFinished;

  public SizeTextEffectAppear(TMP_Text textMesh, EffectData data) {
    startIndex = data.startIndex;
    charslength = data.endIndex - startIndex;
    frequency = data.ReadFloat("f", frequency);
    amplitude = data.ReadFloat("a", amplitude);
    length = data.ReadFloat("l", length);
    this.textMesh = textMesh;
  }

  public void Update(float deltaTime) {
    if (isEffectFinished) {
      return;
    }
    effectTimeElapsed += deltaTime;
    if (charactersShown < charslength) {
      frequencyTimeElapsed += deltaTime;
      if (frequencyTimeElapsed >= frequency) {
        frequencyTimeElapsed -= frequency;
        IncreaseCharactersShown();
      }
    }
  }

  public void ForceUpdate() {
    charactersShown = charslength;
    textMesh.maxVisibleCharacters = charactersShown + startIndex;
    sizeCharEffects.Clear();
    isEffectFinished = true;
  }

  public bool IsEffectEnded() {
    return isEffectFinished;
  }

  public void AnimationUpdate() {
    Mesh mesh = textMesh.mesh;
    Vector3[] vertices = mesh.vertices;
    float duration = frequency * length;
    Vector3 minVector = Vector3.one;
    Vector3 maxVector = new Vector3(amplitude, amplitude, 1);

    for (int i = sizeCharEffects.Count -1; i >= 0; i--) {
      int vertexIndex = sizeCharEffects[i].vertexIndex;
      float timeDiff = sizeCharEffects[i].GetTimeDiff(effectTimeElapsed);
      float normalizedTime = timeDiff / duration;
      Matrix4x4 matrix = Matrix4x4.Scale(Vector3.Lerp(maxVector, minVector, normalizedTime));

      TMProHelpers.ApplyMatrixToChar(vertices, vertexIndex, matrix);

      if (normalizedTime >= 1) {
        sizeCharEffects.RemoveAt(i);
      }
    }

    mesh.vertices = vertices;
    textMesh.canvasRenderer.SetMesh(mesh);

    if (sizeCharEffects.Count == 0 && charactersShown >= charslength) {
      isEffectFinished = true;
    }
  }

  private void IncreaseCharactersShown() {
    charactersShown++;
    int showCharacters = charactersShown + startIndex;
    textMesh.maxVisibleCharacters = showCharacters;

    TMP_TextInfo textInfo = textMesh.textInfo;
    int shownCharacterIndex = showCharacters - 1;
    TMP_CharacterInfo charInfo = textInfo.characterInfo[shownCharacterIndex];
    sizeCharEffects.Add(new SizeCharEffect(
      index: charInfo.vertexIndex,
      startTime: effectTimeElapsed
    ));
  }

  readonly struct SizeCharEffect {

    private readonly float startTime;
    public readonly int vertexIndex;

    public SizeCharEffect(int index, float startTime) {
      vertexIndex = index;
      this.startTime = startTime;
    }

    public float GetTimeDiff(float timeElapsed) {
      return timeElapsed - startTime;
    }
  }
}
