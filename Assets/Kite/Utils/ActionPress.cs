using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kite
{
  [Serializable]
  public class ActionPress
  {
    private float pressTime;
    private bool unscaledTime;

    private float elapsedTimeLeft;
    private bool actionHeld;
    private InputAction action;

    private float DeltaTime => unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

    public ActionPressState State { get; }

    public ActionPress(float pressTime = 0.1f, bool unscaledTime = false)
    {
      this.pressTime = pressTime;
      this.unscaledTime = unscaledTime;
      State = new ActionPressState(this);
    }

    public void Update()
    {
      if (IsPressed())
      {
        elapsedTimeLeft -= DeltaTime;
      }
    }

    public void Press()
    {
      elapsedTimeLeft = pressTime;
    }

    public bool IsPressed() => elapsedTimeLeft > 0;

    public void Cancel()
    {
      elapsedTimeLeft = 0;
    }

    public bool IsHeld() => actionHeld;

    public void AddActionListener(InputAction action)
    {
      if (this.action != null)
        RemoveActionListener();

      action.performed += OnInputAction;
      action.canceled += OnInputAction;
      this.action = action;
    }

    public void RemoveActionListener()
    {
      action.performed -= OnInputAction;
      action.canceled -= OnInputAction;
    }

    public void OnInputAction(InputAction.CallbackContext context)
    {
      if (context.performed)
      {
        actionHeld = true;
        Press();
      }
      else if (context.canceled)
      {
        actionHeld = false;
        Cancel();
      }
    }
  }
}