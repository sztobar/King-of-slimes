using Kite;
using System;
using System.Collections;
using UnityEngine;

public class PlayCamera : MonoBehaviour
{
  public CameraController controller;
  private CameraState state;

  public CameraState GetState() => state;

  public void SetState(CameraState newState)
  {
    state = newState;
    controller.SetState(state);
  }

  public void TransitionTo(CameraState newState)
  {
    state = newState;
    controller.TransitionTo(state);
    
    if (controller.transition.HasTransition())
    {
      GameplayManager.instance.fsm.PushCameraTransition();
    }
  }

  public void TransitionToSelf()
  {
    TransitionTo(state);
  }
}