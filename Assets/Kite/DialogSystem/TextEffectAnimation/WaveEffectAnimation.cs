using UnityEngine;
using TMPro;
using System;

[Serializable]
public class WaveEffectAnimation : ITextEffectAnimation {

  [SerializeField] private float frequency = 4f;
  [SerializeField] private float amplitude = 2f;
  [SerializeField] private float length = 1f;

  private readonly int startIndex;
  private readonly int endIndex;
  private readonly TMP_Text textMesh;

  public WaveEffectAnimation(TMP_Text textMesh, EffectData data) {
    this.textMesh = textMesh;
    frequency = data.ReadFloat("f", frequency);
    amplitude = data.ReadFloat("a", amplitude);
    length = data.ReadFloat("l", length);
    startIndex = data.startIndex;
    endIndex = data.endIndex;
  }

  public void Update() {
    Mesh mesh = textMesh.mesh;
    Vector3[] vertices = mesh.vertices;
    float time = Time.unscaledTime;

    float charsCount = endIndex - startIndex;
    float cycleTime = (2 * Mathf.PI) * length;
    float cycleTimePerChild = cycleTime / charsCount;
    for (int i = 0; i < charsCount; i++) {
      int characterIndex = i + startIndex;
      TMP_CharacterInfo charInfo = textMesh.textInfo.characterInfo[characterIndex];

      float childOffset = cycleTimePerChild * i;
      float t = (time * frequency) + childOffset;
      Vector2 newTranslate = new Vector2(0, amplitude * Mathf.Cos(t));
      Vector3 sumTranslate = newTranslate;

      if (!charInfo.isVisible) {
        continue;
      }
      int vertexIndex = charInfo.vertexIndex;
      vertices[vertexIndex + 0] += sumTranslate;
      vertices[vertexIndex + 1] += sumTranslate;
      vertices[vertexIndex + 2] += sumTranslate;
      vertices[vertexIndex + 3] += sumTranslate;
    }

    mesh.vertices = vertices;
    textMesh.canvasRenderer.SetMesh(mesh);
  }

}
