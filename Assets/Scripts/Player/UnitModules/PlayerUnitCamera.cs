using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System;

public class PlayerUnitCamera : MonoBehaviour, IPlayerUnitComponent
{
  public CameraVisible cameraVisible;

  private PlayerSelectable selectable;
  private PlayerUnitController controller;
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

  private CameraState GetCameraState() => new CameraState
  {
    segment = segment,
    target = controller.transform
  };

  public void OnUnitSelect()
  {
    if (segment)
    {
      if (cameraVisible.inSight)
      {
        playCamera.SetState(GetCameraState());
      }
      else
      {
        playCamera.TransitionTo(GetCameraState());
      }
    }
  }

  public void OnUnitActive()
  {
    if (selectable.IsSelected)
    {
      playCamera.TransitionTo(GetCameraState());
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
    }
  }

  public void Inject(PlayerUnitDI di)
  {
    controller = di.controller;
    selectable = di.selectable;
    playCamera = GameplayManager.instance.playCamera;
  }
}
