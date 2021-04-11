using Kite;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, InputActions.IGameplayActions
{
  public InputActionFloat movement;
  public InputActionPress jump;
  public InputActionPress jumpDown;
  public InputActionPress sword;
  public InputActionPress shield;
  public SelectSlimeActionPress selectSlime;
  public InputActionPress yeet;

  private InputActions inputActions;

  protected void UpdateActionPress()
  {
    float dt = Time.deltaTime;
    jump.Update(dt);
    jumpDown.Update(dt);
    sword.Update(dt);
    shield.Update(dt);
    selectSlime.Update(dt);
    yeet.Update(dt);
  }

  public void Pause()
  {
    DeregisterActions();
  }

  public void Resume()
  {
    RegisterActions();
  }

  private void OnEnable()
  {
    if (inputActions == null)
    {
      inputActions = new InputActions();
    }
    inputActions.Gameplay.Enable();
    RegisterActions();
  }

  private void OnDisable()
  {
    if (inputActions != null)
    {
      inputActions.Gameplay.Disable();
      DeregisterActions();
    }
  }

  private void RegisterActions()
  {
    inputActions.Gameplay.SetCallbacks(this);
    //movement.AddActionListener(inputActions.Gameplay.Movement);
    //jump.AddActionListener(inputActions.Gameplay.Jump);
    //jumpDown.AddActionListener(inputActions.Gameplay.JumpDown);
    //sword.AddActionListener(inputActions.Gameplay.Sword);
    //shield.AddActionListener(inputActions.Gameplay.Shield);
    //selectSlime.AddActionListener(inputActions);
    //yeet.AddActionListener(inputActions.Gameplay.Yeet);
  }

  private void DeregisterActions()
  {
    movement.Reset();
    inputActions.Gameplay.SetCallbacks(null);
    //movement.RemoveActionListener();
    //jump.RemoveActionListener();
    //jumpDown.RemoveActionListener();
    //sword.RemoveActionListener();
    //shield.RemoveActionListener();
    //selectSlime.RemoveActionListener();
    //yeet.RemoveActionListener();
  }

  void InputActions.IGameplayActions.OnMovement(InputAction.CallbackContext context) =>
    movement.OnInputAction(context);

  void InputActions.IGameplayActions.OnJump(InputAction.CallbackContext context) =>
    jump.OnInputAction(context);

  void InputActions.IGameplayActions.OnSword(InputAction.CallbackContext context) =>
    sword.OnInputAction(context);

  void InputActions.IGameplayActions.OnShield(InputAction.CallbackContext context) =>
    shield.OnInputAction(context);

  void InputActions.IGameplayActions.OnYeet(InputAction.CallbackContext context) =>
    yeet.OnInputAction(context);

  void InputActions.IGameplayActions.OnDown(InputAction.CallbackContext context)
  {
  }

  void InputActions.IGameplayActions.OnSelectKing(InputAction.CallbackContext context) =>
    selectSlime.OnInputAction(context, SlimeType.King);

  void InputActions.IGameplayActions.OnSelectHeart(InputAction.CallbackContext context) =>
    selectSlime.OnInputAction(context, SlimeType.Heart);

  void InputActions.IGameplayActions.OnSelectSword(InputAction.CallbackContext context) =>
    selectSlime.OnInputAction(context, SlimeType.Sword);

  void InputActions.IGameplayActions.OnSelectShield(InputAction.CallbackContext context) =>
    selectSlime.OnInputAction(context, SlimeType.Shield);

  void InputActions.IGameplayActions.OnJumpDown(InputAction.CallbackContext context) =>
    jumpDown.OnInputAction(context);
}