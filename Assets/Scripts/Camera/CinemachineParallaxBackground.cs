using Cinemachine;
using UnityEngine;

public class CinemachineParallaxBackground : CinemachineExtension
{
  public CameraSegmentCamera segmentCamera;

  protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref Cinemachine.CameraState state, float deltaTime)
  {
    if (!Application.isPlaying || stage != CinemachineCore.Stage.Finalize)
      return;

    if (segmentCamera)
      segmentCamera.background.UpdateBackgroundPosition(state.CorrectedPosition);
  }
}