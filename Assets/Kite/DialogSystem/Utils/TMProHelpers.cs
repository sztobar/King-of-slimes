using UnityEngine;
using System.Collections;

public static class TMProHelpers {

  public static void ApplyMatrixToChar(Vector3[] vertices, int index, Matrix4x4 matrix) {
    Vector3 centerOffset = (vertices[index + 0] + vertices[index + 2]) / 2;

    vertices[index + 0] = ApplyMatrixToVertex(vertices[index + 0], matrix, centerOffset);
    vertices[index + 1] = ApplyMatrixToVertex(vertices[index + 1], matrix, centerOffset);
    vertices[index + 2] = ApplyMatrixToVertex(vertices[index + 2], matrix, centerOffset);
    vertices[index + 3] = ApplyMatrixToVertex(vertices[index + 3], matrix, centerOffset);
  }

  public static Vector3 ApplyMatrixToVertex(Vector3 vertex, Matrix4x4 matrix, Vector3 centerOffset) {
    Vector3 centered = vertex - centerOffset;
    Vector3 rotated = matrix.MultiplyPoint(centered);
    Vector3 rotatedWithOriginalPivot = rotated + centerOffset;
    return rotatedWithOriginalPivot;
  }
}
