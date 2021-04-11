using Kite;
using System;
using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  public CameraState state;
  public CameraTransitionManager transition;

  public CameraSegment CurrentSegment => state.segment;
  public event Action OnSegmentChange;

  public void SetState(CameraState newState)
  {
    bool segmentChanged = false;

    if (!state.segment || state.segment != newState.segment)
    {
      if (state.segment)
        state.segment.SetCameraInactive();

      newState.segment.SetCameraActive();
      newState.segment.SetCameraTarget(newState.target);
      newState.segment.SetCameraPosition(newState.target.position);
      segmentChanged = true;
    }
    else if (state.target != newState.target)
    {
      newState.segment.SetCameraTarget(newState.target);
    }
    state = newState;

    if (segmentChanged)
      OnSegmentChange?.Invoke();
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

  public void TransitionTo(CameraState newState)
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