using Cinemachine;
using UnityEngine;

// TODO: remove?
public class CinemachineTransitionFromCutscene : CinemachineExtension
{
  CinemachineVirtualCamera cutsceneVCam;
  //public override bool OnTransitionFromCamera(ICinemachineCamera fromCam, Vector3 worldUp, float deltaTime)
  //{
  //  if (!Application.isPlaying)
  //    return base.OnTransitionFromCamera(fromCam, worldUp, deltaTime);

  //  if (!cutsceneVCam)
  //  {
  //    cutsceneVCam = GameplayManager.instance.fsm.cutscene.cutsceneCamera.vcam;
  //  }

  //  if (fromCam != null && fromCam is CinemachineVirtualCamera fromVCam && fromVCam == cutsceneVCam)
  //  {
  //    VirtualCamera.ForceCameraPosition(fromVCam.transform.position, Quaternion.identity);
  //    return true;
  //  }
  //  return base.OnTransitionFromCamera(fromCam, worldUp, deltaTime);
  //}

  protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
  {
  }
}