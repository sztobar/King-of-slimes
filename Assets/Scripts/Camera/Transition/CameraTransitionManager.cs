using Kite;
using System;
using System.Collections;
using UnityEngine;

public class CameraTransitionManager : MonoBehaviour
{
  public CameraTransition currentTransition;
  private GameplayCameraState lastState;

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

  public GameplayCameraState GetState() => lastState;

  public bool HasTransition() => currentTransition;

  public void TransitionFromTo(GameplayCameraState from, GameplayCameraState to)
  {
    if (!currentTransition)
    {
      if (to.segment == from.segment)
      {
        if (to.target != from.target)
        {
          currentTransition = CameraCatchUpTransition.Create(to);
        }
        else
        {
          // NoTransition
        }
      }
      else
      {
        currentTransition = CameraFadeTransition.Create(from, to);
      }
    }
    else
    {
      if (currentTransition is CameraCatchUpTransition cameraCatchUpTransition)
      {
        if (from.segment == to.segment)
        {
          currentTransition = CameraCatchUpTransition.Create(to);
        }
        else
        {
          currentTransition = CameraFadeTransition.Create(from, to);
        }
      }
      else if (currentTransition is CameraFadeTransition cameraFadeTransition)
      {
        if (from.segment == to.segment)
        {
          if (cameraFadeTransition.from == from) // is fading in
          {
            float reverseT = 1 - cameraFadeTransition.t;
            currentTransition = CameraFadeTransition.Create(from, to, reverseT);
          }
          if (from.target != to.target)
          {
            currentTransition.nextTransition = CameraCatchUpTransition.Create(to);
          }
        }
        else
        {
          if (cameraFadeTransition.from == from) // is fading in
          {
            float t = cameraFadeTransition.t;
            currentTransition = CameraFadeTransition.Create(from, to, t);
          }
          else // fading out
          {
            float reverseT = 1 - cameraFadeTransition.t;
            currentTransition = CameraFadeTransition.Create(from, to, reverseT);
          }
        }
      }
    }
  }
}