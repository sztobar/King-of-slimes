using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System;

public class PlayerUnitCamera : MonoBehaviour, IPlayerUnitComponent
{
  private PlayerSelectable selectable;
  private PlayerUnitController controller;
  //private GameplayCamera gameplayCamera;
  private PlayCamera playCamera;

  private CameraSegment segment;

  public CameraSegment CameraSegment {
    get => segment;
    set {
      CameraSegment oldSegment = segment;
      segment = value;
      if (segment && segment != oldSegment)
      {
        OnCurrentCameraSegmentChange();
      }
    }
  }

  private GameplayCameraState GetCameraState() => new GameplayCameraState
  {
    segment = segment,
    target = controller.transform
  };

  public void OnUnitSelect()
  {
    //cameraController.SetFollowUnit(controller, segment);
    if (segment)
    {
      playCamera.TransitionTo(GetCameraState());
      //gameplayCamera.TransitionToSegment(segment, controller.transform, fadeIfSegmentChange: true);
    }
  }

  public void OnUnitActive()
  {
    if (selectable.IsSelected)
    {
      playCamera.TransitionTo(GetCameraState());
      //cameraController.SetFollowUnit(controller, segment);
      //gameplayCamera.TransitionToSegment(segment, controller.transform, fadeIfSegmentChange: true);
    }
  }

  public void TurnOffCamera()
  {
  }

  private void OnCurrentCameraSegmentChange()
  {
    if (selectable.IsSelected)
    {
      playCamera.SetState(GetCameraState());
      //cameraController.SetCurrentSegment(segment);
      //gameplayCamera.TransitionToSegment(segment, controller.transform, fadeIfSegmentChange: false);
    }
  }

  public void Inject(PlayerUnitDI di)
  {
    controller = di.controller;
    selectable = di.selectable;
    playCamera = GameplayManager.instance.playCamera;
    //gameplayCamera = GameplayManager.instance.camera;
  }
}
