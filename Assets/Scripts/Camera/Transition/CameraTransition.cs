using Kite;
using System.Collections;
using UnityEngine;

public abstract class CameraTransition : MonoBehaviour
{
  [HideInInspector] public CameraTransition nextTransition;
  public abstract void TransitionStart();
  public abstract void TransitionUpdate(float dt);
  public virtual void TransitionExit() { }
  public abstract bool IsFinished();

  public abstract CameraState GetState();
}