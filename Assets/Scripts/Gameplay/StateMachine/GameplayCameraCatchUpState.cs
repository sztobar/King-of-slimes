using Cinemachine;
using UnityEngine;

public class GameplayCameraCatchUpState : GameplayState
{
  public int cameraStayFrames = 1;
  private GameplayCamera gameplayCamera;
  private GameplayStateMachine fsm;

  private Vector2 previousCameraPosition;
  private int stayFramesLeft;

  public override void StateStart()
  {
    stayFramesLeft = cameraStayFrames;
    previousCameraPosition = Vector2.zero;
  }

  public override void StateUpdate()
  {
    CinemachineVirtualCamera vcam = gameplayCamera.ActiveSegment.cam.virtualCam;
    CameraState state = vcam.State;
    Vector2 currentCameraPosition = state.CorrectedPosition;

    if (currentCameraPosition == previousCameraPosition)
    {
      stayFramesLeft--;

      if (stayFramesLeft <= 0)
        fsm.PopState();
    }
    previousCameraPosition = currentCameraPosition;
  }

  public override void Inject(GameplayManager gameplayManager)
  {
    gameplayCamera = gameplayManager.camera;
    fsm = gameplayManager.fsm;
  }
}