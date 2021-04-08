using UnityEngine;

namespace Kite
{
  public static class CameraHelpers
  {
    public static readonly float CAMERA_HEIGHT = 180;
    public static readonly float CAMERA_WIDTH = 320;

    public static Vector2 GetCameraSize(this Camera camera)
    {
      float cameraHeight = 2f * camera.orthographicSize;
      float cameraWidth = cameraHeight * camera.aspect;
      return new Vector2(cameraWidth, cameraHeight);
    }

    public static Bounds GetBounds(this Camera camera)
    {
      Vector3 cameraSize = camera.GetCameraSize();
      Vector2 cameraPosition = camera.transform.position;
      return new Bounds(cameraPosition, cameraSize);
    }

    public static bool IsOutOfSight(this Camera camera, Bounds bounds, float scale = 1f)
    {
      Bounds cameraBounds = camera.GetBounds();
      cameraBounds.Expand(cameraBounds.size * (scale - 1));
      cameraBounds.Expand(bounds.size);
      Vector2 position = bounds.center; // reset z
      return !cameraBounds.Contains(position);
    }
  }
}