using System;
using UnityEngine;
using TMPro;

[Serializable]
public class SwingEffectAnimation : ITextEffectAnimation {

  private readonly float ONE_SECOND_FREQUENCY = 2 * Mathf.PI;

  [SerializeField] private float frequency = 1f;
  [SerializeField] private float amplitude = 15f;

  private readonly TMP_Text textMesh;
  private readonly int startIndex;
  private readonly int endIndex;

  public SwingEffectAnimation(TMP_Text textMesh, EffectData data) {
    frequency = data.ReadFloat("f", frequency);
    amplitude = data.ReadFloat("a", amplitude);
    this.textMesh = textMesh;
    startIndex = data.startIndex;
    endIndex = data.endIndex;
  }

  public void Update() {
    Mesh mesh = textMesh.mesh;
    Vector3[] vertices = mesh.vertices;
    TMP_TextInfo textInfo = textMesh.textInfo;
    float time = Time.unscaledTime;

    float charsCount = endIndex - startIndex;
    float cos = Mathf.Cos(time * ONE_SECOND_FREQUENCY * frequency);
    float angle = cos * amplitude;
    Matrix4x4 matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 0, angle));

    for (int i = 0; i < charsCount; i++) {
      int characterIndex = i + startIndex;
      TMP_CharacterInfo charInfo = textInfo.characterInfo[characterIndex];

      if (!charInfo.isVisible) {
        continue;
      }
      int vertexIndex = charInfo.vertexIndex;
      TMProHelpers.ApplyMatrixToChar(vertices, vertexIndex, matrix);
    }

    mesh.vertices = vertices;
    textMesh.canvasRenderer.SetMesh(mesh);
  }
}
