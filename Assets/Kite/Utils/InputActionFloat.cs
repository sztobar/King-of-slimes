using System;
using UnityEngine.InputSystem;

[Serializable]
public class InputActionFloat
{
  private InputAction action;
  private float value;

  public float Value => value;

  public void AddActionListener(InputAction action)
  {
    if (this.action != null)
      RemoveActionListener();

    this.action = action;
    action.performed += OnInputAction;
    action.canceled += OnInputAction;
  }

  public void RemoveActionListener()
  {
    if (action == null)
      return;

    value = 0;
    action.performed -= OnInputAction;
    action.canceled -= OnInputAction;
  }

  public void OnInputAction(InputAction.CallbackContext context)
  {
    value = context.ReadValue<float>();
  }
}