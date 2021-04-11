using Kite;
using System.Collections;
using UnityEngine;

public abstract class CameraTransition : ScriptableObject
{
  public CameraTransition nextTransition;
  public abstract void TransitionStart();
  public abstract void TransitionUpdate(float dt);
  public virtual void TransitionExit() { }
  public abstract bool IsFinished();

  public abstract GameplayCameraState GetState();
}