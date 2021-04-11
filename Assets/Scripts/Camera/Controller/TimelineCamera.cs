using Kite;
using System.Collections;
using UnityEngine;

public class TimelineCamera : MonoBehaviour
{
  public CameraController controller;
  public CameraFadeTransition cameraFadeTransition;

  public void SetState(CameraSegment segment, Vector3 position)
  {
    transform.position = position;
    CameraState newState = new CameraState
    {
      segment = segment,
      target = transform
    };
    controller.SetTransition(null);
    controller.SetState(newState);
  }

  public void SetFadeState(CameraState from, CameraState to, float t)
  {
    controller.SetTransition(cameraFadeTransition.Init(from, to, t));
  }

  public void ResetState()
  {
    if (!Application.isPlaying)
    {
      controller.SetTransition(null);
      controller.ResetState();
    }
  }
}