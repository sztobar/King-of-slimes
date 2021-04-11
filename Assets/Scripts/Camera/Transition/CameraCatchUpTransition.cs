using Kite;
using System.Collections;
using UnityEngine;

public class CameraCatchUpTransition : CameraTransition
{
  public int stayFrames = 1;
  public float minimalDelta = 0.1f;

  private CameraState state;

  private Vector2 previousCameraPosition;
  private int stayFramesLeft;
  private float positionDelta;

  public CameraCatchUpTransition Init(CameraState state)
  {
    nextTransition = null;
    this.state = state;
    return this;
  }

  public override bool IsFinished() => stayFramesLeft <= 0 || positionDelta < minimalDelta;

  public override void TransitionUpdate(float dt)
  {
    Vector2 currentCameraPosition = state.segment.GetCameraPosition();

    if (currentCameraPosition == previousCameraPosition)
    {
      stayFramesLeft--;
    }
    positionDelta = Vector2.Distance(currentCameraPosition, previousCameraPosition);
    previousCameraPosition = currentCameraPosition;
  }

  public override void TransitionStart()
  {
    positionDelta = minimalDelta;
    stayFramesLeft = stayFrames;
    previousCameraPosition = Vector2.zero;
    state.segment.cam.virtualCam.Follow = state.target;
  }

  public override CameraState GetState() => state;
}