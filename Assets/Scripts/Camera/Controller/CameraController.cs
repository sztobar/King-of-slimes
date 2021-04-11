using Kite;
using System;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public GameplayCameraState state;
  public CameraTransitionManager transition;

  public void SetState(GameplayCameraState newState)
  {
    if (state.segment && state.segment != newState.segment)
    {
      state.segment.SetCameraInactive();
      newState.segment.SetCameraActive();
      newState.segment.SetCameraTarget(newState.target);
      newState.segment.SetCameraPosition(newState.target.position);
    }
    else if (state.target != newState.target)
    {
      newState.segment.SetCameraTarget(newState.target);
    }
    state = newState;
  }

  public void ResetState()
  {
    if (state.segment)
    {
      state.segment.SetCameraInactive();
      state.segment.SetCameraTarget(null);
    }
    state.segment = null;
    state.target = null;
  }

  public void TransitionTo(GameplayCameraState newState)
  {
    transition.TransitionFromTo(state, newState);
    if (!transition.HasTransition())
    {
      SetState(newState);
    }
    else
    {
      StartTransition();
    }
  }

  public bool HasTransition() => transition.HasTransition();

  public void TransitionUpdate(float dt)
  {
    transition.TransitionUpdate(dt);
    SetState(transition.GetState());
  }

  public void SetTransition(CameraTransition newTransition)
  {
    transition.SetTransition(newTransition);
    if (transition.HasTransition())
    {
      StartTransition();
    }
  }

  private void StartTransition()
  {
    transition.TransitionStart();
    SetState(transition.GetState());
  }
}