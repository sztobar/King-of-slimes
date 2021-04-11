using Kite;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

// TODO: input freeze
// reset all press states & move input but don't disable
public class PlayerPlayInput : MonoBehaviour, InputActions.IGameplayActions, IPlayerComponent
{

  private InputActions inputActions;

  private InputActionPress jump = new InputActionPress();
  private InputActionPress jumpDown = new InputActionPress();
  private InputActionPress sword = new InputActionPress();
  private InputActionPress shield = new InputActionPress();
  private SelectSlimeActionPress selectSlime = new SelectSlimeActionPress();
  private InputActionPress yeet = new InputActionPress();
  private bool downHold;

  public float MoveInput { get; private set; }
  //public ActionPressState Jump => jump.State;
  //public ActionPressState JumpDown => jumpDown.State;
  //public ActionPressState Sword => sword.State;
  //public ActionPressState Shield => shield.State;
  //public SelectSlimeActionPress SelectSlime => selectSlime;
  //public ActionPressState Yeet => yeet.State;

  private void Update()
  {
    //jump.Update();
    //jumpDown.Update();
    //sword.Update();
    //shield.Update();
    //selectSlime.Update();
    //yeet.Update();
  }

  private void OnEnable()
  {
    if (inputActions == null)
    {
      inputActions = new InputActions();
      inputActions.Gameplay.SetCallbacks(this);
    }
    inputActions.Gameplay.Enable();
  }

  private void OnDisable()
  {
    if (inputActions != null)
    {
      inputActions.Gameplay.Disable();
    }
  }

  public void OnMovement(InputAction.CallbackContext context)
  {
    MoveInput = context.ReadValue<float>();
  }

  public void OnJump(InputAction.CallbackContext context)
  {
    jump.OnInputAction(context);
    if (jump.IsPressed() && downHold)
    {
      jumpDown.Press();
    }
  }

  public void OnSword(InputAction.CallbackContext context)
  {
    sword.OnInputAction(context);
  }

  public void OnShield(InputAction.CallbackContext context)
  {
    shield.OnInputAction(context);
  }

  public void OnYeet(InputAction.CallbackContext context)
  {
    yeet.OnInputAction(context);
  }

  public void OnDown(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      downHold = true;
      if (jump.IsPressed())
      {
        jumpDown.Press();
      }
    }
    else if (context.canceled)
    {
      downHold = false;
      if (jumpDown.IsPressed())
      {
        jumpDown.Use();
      }
    }
  }

  public void OnSelectKing(InputAction.CallbackContext context)
  {
    selectSlime.OnInputAction(context, SlimeType.King);
  }

  public void OnSelectHeart(InputAction.CallbackContext context)
  {
    selectSlime.OnInputAction(context, SlimeType.Heart);
  }

  public void OnSelectSword(InputAction.CallbackContext context)
  {
    selectSlime.OnInputAction(context, SlimeType.Sword);
  }

  public void OnSelectShield(InputAction.CallbackContext context)
  {
    selectSlime.OnInputAction(context, SlimeType.Shield);
  }

  public void Inject(PlayerController controller)
  {
  }

  public void OnJumpDown(InputAction.CallbackContext context)
  {

  }

  public void Pause()
  {
  }

  public void Resume()
  {
  }
}
