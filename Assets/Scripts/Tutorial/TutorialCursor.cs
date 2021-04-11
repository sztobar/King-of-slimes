using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialCursor : DialogFrameCursorNext
{
  public int appearOffset;
  public TMP_Text textMesh;

  public override void Show()
  {
    int charactesShown = textMesh.maxVisibleCharacters;
    var charInfo = textMesh.textInfo.characterInfo[charactesShown - 1];
    int vertexIndex = charInfo.vertexIndex;
    Mesh mesh = textMesh.mesh;
    Vector3[] vertices = mesh.vertices;
    Vector3 vertexPos = vertices[vertexIndex];
    Vector3 newPosition = new Vector3(transform.localPosition.x, vertexPos.y - appearOffset);
    transform.localPosition = newPosition;
    base.Show();
  }
}
