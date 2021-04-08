using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrottleAxis
{
  private readonly float waitTime;

  private float elapsedTime;
  private bool inputInProgress;
  private float value;

  public event Action<float> OnEmit = delegate { };

  public ThrottleAxis(float wait = 0.3f)
  {
    waitTime = wait;
  }

  public void OnActionCancel()
  {
    inputInProgress = false;
  }

  public void OnActionPerform(float value)
  {
    if (value == 0)
    {
      OnActionCancel();
      return;
    }
    if (inputInProgress)
    {
      this.value = value;
    }
    else
    {
      this.value = value;
      elapsedTime = 0;
      inputInProgress = true;
      OnEmit(value);
    }
  }

  public void Update()
  {
    if (inputInProgress)
    {
      if (elapsedTime >= waitTime)
      {
        elapsedTime -= waitTime;
        OnEmit(value);
      }
      elapsedTime += Time.unscaledDeltaTime;
    }
  }

  public void OnInputAction(InputAction.CallbackContext context)
  {
    float value = context.ReadValue<float>();
    if (context.started || context.performed)
    {
      OnActionPerform(value);
    }
    else if (context.canceled)
    {
      OnActionCancel();
    }
  }
}
