using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable]
public class ShakeEffectAnimation : ITextEffectAnimation {

  private static readonly float FRAME_TIME = 1 / 60f;

  [SerializeField] private float frequency = 3f;
  [SerializeField] private float amplitude = 1.5f;

  private readonly int startIndex;
  private readonly int endIndex;
  private readonly TMP_Text textMesh;

  private readonly List<Vector2> previousShakes;
  private float previousShakeTime;

  public ShakeEffectAnimation(TMP_Text textMesh, EffectData data) {
    this.textMesh = textMesh;
    frequency = data.ReadFloat("f", frequency);
    amplitude = data.ReadFloat("a", amplitude);
    startIndex = data.startIndex;
    endIndex = data.endIndex;
    int len = endIndex - startIndex;
    previousShakes = new List<Vector2>(len);
    for (int i = 0; i < len; i++) {
      previousShakes.Add(Vector2.zero);
    }
  }

  public void Update() {
    Mesh mesh = textMesh.mesh;
    Vector3[] vertices = mesh.vertices;
    float time = Time.unscaledTime;

    if (time - previousShakeTime > FRAME_TIME * frequency) {
      previousShakeTime = time;
      GenerateNewShakes(textMesh.textInfo, vertices);
    } else {
      ApplyPreviousShakes(textMesh.textInfo, vertices);
    }

    mesh.vertices = vertices;
    textMesh.canvasRenderer.SetMesh(mesh);
  }

  private void ApplyPreviousShakes(TMP_TextInfo textInfo, Vector3[] vertices) {
    float charsCount = endIndex - startIndex;

    for (int i = 0; i < charsCount; i++) {
      int characterIndex = i + startIndex;
      TMP_CharacterInfo charInfo = textInfo.characterInfo[characterIndex];

      if (!charInfo.isVisible) {
        continue;
      }
      Vector3 offset = previousShakes[i];
      int vertexIndex = charInfo.vertexIndex;
      vertices[vertexIndex + 0] += offset;
      vertices[vertexIndex + 1] += offset;
      vertices[vertexIndex + 2] += offset;
      vertices[vertexIndex + 3] += offset;
    }
  }

  private void GenerateNewShakes(TMP_TextInfo textInfo, Vector3[] vertices) {
    float charsCount = endIndex - startIndex;

    for (int i = 0; i < charsCount; i++) {
      int characterIndex = i + startIndex;
      TMP_CharacterInfo charInfo = textInfo.characterInfo[characterIndex];

      if (!charInfo.isVisible) {
        continue;
      }
      Vector3 offset = Random.insideUnitSphere * amplitude;
      int vertexIndex = charInfo.vertexIndex;
      vertices[vertexIndex + 0] += offset;
      vertices[vertexIndex + 1] += offset;
      vertices[vertexIndex + 2] += offset;
      vertices[vertexIndex + 3] += offset;

      previousShakes[i] = offset;
    }
  }
}
