using Kite;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

[Serializable]
public class SelectSlimeActionPress
{
  public InputActionPress actionPress;

  private InputActions inputActions;
  private SlimeType pressedType = SlimeType.King;

  public void Update(float dt) =>
    actionPress.Update(dt);

  public bool IsPressed() =>
    actionPress.IsPressed();

  public void Use() =>
    actionPress.Use();

  public bool IsHeld() =>
    actionPress.IsHeld();

  public SlimeType GetPressedSlime() =>
    pressedType;

  public void OnInputAction(InputAction.CallbackContext context, SlimeType type)
  {
    if (context.performed)
    {
      float value = context.ReadValue<float>();
      if (value > 0.75)
      {
        pressedType = type;
        actionPress.Press();
      }
    }
    else if (context.canceled)
    {
      if (pressedType == type)
      {
        actionPress.Use();
      }
    }
  }

  public void AddActionListener(InputActions inputActions)
  {
    if (this.inputActions != null)
      RemoveActionListener();

    inputActions.Gameplay.SelectKing.performed += OnKingInputAction;
    inputActions.Gameplay.SelectKing.canceled += OnKingInputAction;
    
    inputActions.Gameplay.SelectHeart.performed += OnHeartInputAction;
    inputActions.Gameplay.SelectHeart.canceled += OnHeartInputAction;
    
    inputActions.Gameplay.SelectSword.performed += OnSwordInputAction;
    inputActions.Gameplay.SelectSword.canceled += OnSwordInputAction;
    
    inputActions.Gameplay.SelectShield.performed += OnShieldInputAction;
    inputActions.Gameplay.SelectShield.canceled += OnShieldInputAction;

    this.inputActions = inputActions;
  }

  public void RemoveActionListener()
  {
    if (inputActions == null)
      return;

    inputActions.Gameplay.SelectKing.performed -= OnKingInputAction;
    inputActions.Gameplay.SelectKing.canceled -= OnKingInputAction;

    inputActions.Gameplay.SelectHeart.performed -= OnHeartInputAction;
    inputActions.Gameplay.SelectHeart.canceled -= OnHeartInputAction;

    inputActions.Gameplay.SelectSword.performed -= OnSwordInputAction;
    inputActions.Gameplay.SelectSword.canceled -= OnSwordInputAction;

    inputActions.Gameplay.SelectShield.performed -= OnShieldInputAction;
    inputActions.Gameplay.SelectShield.canceled -= OnShieldInputAction;

    inputActions = null;

  }

  private void OnKingInputAction(InputAction.CallbackContext context) =>
    OnInputAction(context, SlimeType.King);

  private void OnHeartInputAction(InputAction.CallbackContext context) =>
    OnInputAction(context, SlimeType.Heart);

  private void OnSwordInputAction(InputAction.CallbackContext context) =>
    OnInputAction(context, SlimeType.Sword);

  private void OnShieldInputAction(InputAction.CallbackContext context) =>
    OnInputAction(context, SlimeType.Shield);
}
