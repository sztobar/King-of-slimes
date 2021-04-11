using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CutsceneInput : InputActions.IGameplayActions
{
  private InputActions inputActions;
  public event Action OnAnyAction;
  public event Action OnSkipAction;

  public void EnableInputActions()
  {
    if (inputActions == null)
      inputActions = new InputActions();
    
    if (!inputActions.Gameplay.enabled)
    {
      inputActions.Gameplay.Enable();
      inputActions.Gameplay.SetCallbacks(this);
    }
  }

  private void OnAnyActionInternal(InputAction.CallbackContext obj)
  {
    if (obj.performed)
      OnAnyAction();
  }

  private void OnSkipActionInternal(InputAction.CallbackContext obj)
  {
    if (obj.performed)
      OnSkipAction();
  }

  public void DisableInputActions()
  {
    if (inputActions != null && inputActions.Gameplay.enabled)
    {
      inputActions.Gameplay.Disable();
      inputActions.Gameplay.SetCallbacks(null);
    }
  }

  public void OnMovement(InputAction.CallbackContext context) =>
    OnAnyActionInternal(context);

  public void OnJump(InputAction.CallbackContext context) =>
    OnSkipActionInternal(context);

  public void OnSword(InputAction.CallbackContext context) =>
    OnAnyActionInternal(context);

  public void OnShield(InputAction.CallbackContext context) =>
    OnAnyActionInternal(context);

  public void OnYeet(InputAction.CallbackContext context) =>
    OnAnyActionInternal(context);

  public void OnDown(InputAction.CallbackContext context) { }

  public void OnSelectKing(InputAction.CallbackContext context) =>
    OnAnyActionInternal(context);

  public void OnSelectHeart(InputAction.CallbackContext context) =>
    OnAnyActionInternal(context);

  public void OnSelectSword(InputAction.CallbackContext context) =>
    OnAnyActionInternal(context);

  public void OnSelectShield(InputAction.CallbackContext context) =>
    OnAnyActionInternal(context);

  public void OnJumpDown(InputAction.CallbackContext context) { }
}