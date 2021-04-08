using UnityEngine;

namespace Kite {
  /// <summary>
  /// Defines extension methods to Unity's Transform class.
  /// </summary>
  public static class TransformExtensions {
    public static void FlipHorizontally(this Transform transform) {
      transform.Rotate(0, 180f, 0);
    }

    public static void FlipVertically(this Transform transform) {
      transform.Rotate(180f, 0, 0);
    }

    /// <summary>
    /// Sets the x coordinate of this transform's position.
    /// </summary>
    /// <param name="x">The new x position.</param>
    public static void SetPositionX(this Transform transform, float x) {
      SetPositionElem(transform, 0, x);
    }

    /// <summary>
    /// Sets the y coordinate of this transform's position.
    /// </summary>
    /// <param name="y">The new y position.</param>
    public static void SetPositionY(this Transform transform, float y) {
      SetPositionElem(transform, 1, y);
    }

    /// <summary>
    /// Sets the z coordinate of this transform's position.
    /// </summary>
    /// <param name="z">The new z position.</param>
    public static void SetPositionZ(this Transform transform, float z) {
      SetPositionElem(transform, 2, z);
    }

    /// <summary>
    /// Sets the x coordinate of this transform's localPosition.
    /// </summary>
    /// <param name="x">The new local x position.</param>
    public static void SetLocalPositionX(this Transform transform, float x) {
      SetLocalPositionElem(transform, 0, x);
    }

    /// <summary>
    /// Sets the y coordinate of this transform's localPosition.
    /// </summary>
    /// <param name="y">The new local y position.</param>
    public static void SetLocalPositionY(this Transform transform, float y) {
      SetLocalPositionElem(transform, 1, y);
    }

    /// <summary>
    /// Sets the z coordinate of this transform's localPosition.
    /// </summary>
    /// <param name="z">The new local z position.</param>
    public static void SetLocalPositionZ(this Transform transform, float z) {
      SetLocalPositionElem(transform, 2, z);
    }

    /// <summary>
    /// Sets the x coordinate of this transform's localScale.
    /// </summary>
    /// <param name="x">The new local x scale.</param>
    public static void SetLocalScaleX(this Transform transform, float x) {
      SetLocalScaleElem(transform, 0, x);
    }

    /// <summary>
    /// Sets the y coordinate of this transform's localScale.
    /// </summary>
    /// <param name="y">The new local y scale.</param>
    public static void SetLocalScaleY(this Transform transform, float y) {
      SetLocalScaleElem(transform, 1, y);
    }

    /// <summary>
    /// Sets the z coordinate of this transform's localScale.
    /// </summary>
    /// <param name="z">The new local z scale.</param>
    public static void SetLocalScaleZ(this Transform transform, float z) {
      SetLocalScaleElem(transform, 2, z);
    }

    static Vector3 Vector3Copy(Vector3 vector) {
      return new Vector3(vector.x, vector.y, vector.z);
    }

    static void SetPositionElem(Transform transform, int index, float value) {
      Vector3 newVector = Vector3Copy(transform.position);
      newVector[index] = value;
      transform.position = newVector;
    }

    static void SetLocalPositionElem(Transform transform, int index, float value) {
      Vector3 newVector = Vector3Copy(transform.localPosition);
      newVector[index] = value;
      transform.localPosition = newVector;
    }

    static void SetLocalScaleElem(Transform transform, int index, float value) {
      Vector3 newVector = Vector3Copy(transform.localScale);
      newVector[index] = value;
      transform.localScale = newVector;
    }
  }
}