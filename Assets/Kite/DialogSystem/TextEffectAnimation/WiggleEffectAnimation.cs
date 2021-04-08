using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable]
public class WiggleEffectAnimation : ITextEffectAnimation {

  [SerializeField] private float frequency = 8f;
  [SerializeField] private float amplitude = 1f;

  private readonly List<CharWiggle> charactersWiggleList;
  private readonly TMP_Text textMesh;
  private readonly int startIndex;
  private readonly int endIndex;

  public WiggleEffectAnimation(TMP_Text textMesh, EffectData data) {
    frequency = data.ReadFloat("f", frequency);
    amplitude = data.ReadFloat("a", amplitude);
    this.textMesh = textMesh;
    startIndex = data.startIndex;
    endIndex = data.endIndex;
    int len = endIndex - startIndex;
    charactersWiggleList = new List<CharWiggle>(len);
    for (int i = 0; i < len; i++) {
      charactersWiggleList.Add(CreateCharWiggle());
    }
  }

  public void Update() {
    Mesh mesh = textMesh.mesh;
    Vector3[] vertices = mesh.vertices;
    float time = Time.unscaledTime;

    TMP_TextInfo textInfo = textMesh.textInfo;
    float charsCount = endIndex - startIndex;
    for (int i = 0; i < charsCount; i++) {
      int characterIndex = i + startIndex;
      TMP_CharacterInfo charInfo = textInfo.characterInfo[characterIndex];

      if (!charInfo.isVisible) {
        continue;
      }
      Vector3 offset = charactersWiggleList[i].GetWiggle(amplitude, frequency, time);
      int vertexIndex = charInfo.vertexIndex;
      vertices[vertexIndex + 0] += offset;
      vertices[vertexIndex + 1] += offset;
      vertices[vertexIndex + 2] += offset;
      vertices[vertexIndex + 3] += offset;
    }

    mesh.vertices = vertices;
    textMesh.canvasRenderer.SetMesh(mesh);
  }

  private CharWiggle CreateCharWiggle() {
    return new CharWiggle(
      angle: GetRandomWiggleAngle(),
      offset: Random.value * 2 * Mathf.PI
    );
  }

  private float GetRandomWiggleAngle() {
    int angle = Mathf.RoundToInt(Random.value * 45);
    float axis = Random.Range(1, 5);
    switch (axis) {
      case 1:
        return angle + 30;
      case 2:
        return angle + 90 + 15;
      case 3:
        return angle + 180 + 30;
      case 4:
      default:
        return angle + 270 + 15;
    }
  }

  readonly struct CharWiggle {
    private readonly Vector3 normal;
    private readonly float offset;

    public CharWiggle(float angle, float offset) {
      normal = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
      this.offset = offset;
    }

    public Vector3 GetWiggle(float amplitude, float frequency, float time) {
      return normal * amplitude * Mathf.Cos((time * frequency) + offset);
    }
  }
}
