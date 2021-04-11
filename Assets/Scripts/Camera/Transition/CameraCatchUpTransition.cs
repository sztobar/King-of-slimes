using Kite;
using System.Collections;
using UnityEngine;

public class CameraCatchUpTransition : CameraTransition
{
  private static CameraCatchUpTransition instance;

  private GameplayCameraState state;

  private Vector2 previousCameraPosition;
  private int stayFramesLeft;

  public static CameraCatchUpTransition Create(GameplayCameraState state)
  {
    if (!instance)
    {
      instance = CreateInstance<CameraCatchUpTransition>();
    }
    instance.state = state;

    return instance;
  }

  public override bool IsFinished() => stayFramesLeft <= 0;

  public override void TransitionUpdate(float dt)
  {
    Vector2 currentCameraPosition = state.segment.GetCameraPosition();

    if (currentCameraPosition == previousCameraPosition)
    {
      stayFramesLeft--;
    }
    previousCameraPosition = currentCameraPosition;
  }

  public override void TransitionStart()
  {
    instance.stayFramesLeft = 1;
    instance.previousCameraPosition = Vector2.zero;
    state.segment.cam.virtualCam.Follow = state.target;
  }

  public override GameplayCameraState GetState() => state;
}