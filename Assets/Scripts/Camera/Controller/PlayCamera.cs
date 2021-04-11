using Kite;
using System;
using System.Collections;
using UnityEngine;

public class PlayCamera : MonoBehaviour
{
  public CameraController controller;
  private GameplayCameraState state;

  public GameplayCameraState GetState() => state;

  public void SetState(GameplayCameraState newState)
  {
    state = newState;
    controller.SetState(state);
  }

  public void TransitionTo(GameplayCameraState newState)
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