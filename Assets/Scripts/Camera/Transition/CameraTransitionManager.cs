using Kite;
using System;
using System.Collections;
using UnityEngine;

public class CameraTransitionManager : MonoBehaviour
{
  public CameraFadeTransition fadeTransition;
  public CameraCatchUpTransition catchUpTransition;
  public CameraTransition currentTransition;
  private CameraState lastState;

  public void TransitionUpdate(float dt)
  {
    if (currentTransition.IsFinished())
    {
      CameraTransition previousTransition = currentTransition;
      previousTransition.TransitionExit();
      currentTransition = previousTransition.nextTransition;
      if (currentTransition)
      {
        TransitionStart();
      }
      else
      {
        lastState = previousTransition.GetState();
      }
    }
    else
    {
      currentTransition.TransitionUpdate(dt);
      lastState = currentTransition.GetState();
    }
  }

  public void TransitionStart()
  {
    currentTransition.TransitionStart();
    lastState = currentTransition.GetState();
  }

  public void SetTransition(CameraTransition newTransition)
  {
    currentTransition = newTransition;
  }

  public CameraState GetState() => lastState;

  public bool HasTransition() => currentTransition;

  public void TransitionFromTo(CameraState from, CameraState to)
  {
    if (!currentTransition)
    {
      if (to.segment == from.segment)
      {
        if (to.target != from.target)
        {
          currentTransition = catchUpTransition.Init(to);
        }
        else
        {
          // NoTransition
        }
      }
      else
      {
        currentTransition = fadeTransition.Init(from, to);
      }
    }
    else
    {
      if (currentTransition is CameraCatchUpTransition)
      {
        if (from.segment == to.segment)
        {
          currentTransition = catchUpTransition.Init(to);
        }
        else
        {
          currentTransition = fadeTransition.Init(from, to);
        }
      }
      else if (currentTransition is CameraFadeTransition cameraFadeTransition)
      {
        if (from.segment == to.segment)
        {
          if (cameraFadeTransition.from == from) // is fading in
          {
            float reverseT = 1 - cameraFadeTransition.t;
            currentTransition = fadeTransition.Init(from, to, reverseT);
          }
          if (from.target != to.target)
          {
            currentTransition.nextTransition = catchUpTransition.Init(to);
          }
        }
        else
        {
          if (cameraFadeTransition.from == from) // is fading in
          {
            float t = cameraFadeTransition.t;
            currentTransition = fadeTransition.Init(from, to, t);
          }
          else // fading out
          {
            float reverseT = 1 - cameraFadeTransition.t;
            currentTransition = fadeTransition.Init(from, to, reverseT);
          }
        }
      }
    }
  }
}