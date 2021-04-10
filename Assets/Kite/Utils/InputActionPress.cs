using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kite
{
  [Serializable]
  public class InputActionPress
  {
    public float pressTime = 0.1f;

    private float elapsedTimeLeft;
    private bool actionHeld;
    private InputAction action;

    public void Update(float dt)
    {
      if (IsPressed())
      {
        elapsedTimeLeft -= dt;
      }
    }

    public void Press()
    {
      elapsedTimeLeft = pressTime;
    }

    public bool IsPressed() => elapsedTimeLeft > 0;

    public void Use()
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
      if (action == null)
        return;

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
        Use();
      }
    }
  }
}