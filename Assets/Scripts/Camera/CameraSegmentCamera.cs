using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraSegmentCamera : MonoBehaviour
{
  private static readonly int CAMERA_PRIORITY = 10;

  public CinemachineVirtualCamera virtualCam;
  public ParallaxBackgroundBase background;

  private void Awake()
  {
    Deactivate();
  }

  public void Activate()
  {
    virtualCam.Priority = CAMERA_PRIORITY;
    if (Application.isPlaying)
      background.Activate();
  }

  public void Deactivate()
  {
    virtualCam.Priority = 0;
    if (Application.isPlaying)
      background.Deactivate();
  }
}
