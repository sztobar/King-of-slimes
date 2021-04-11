using Kite;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuInput : MonoBehaviour, InputActions.IMenuActions
{
  private InputActions inputActions;

  public InputActionPress confirm;// = new ActionPress(unscaledTime: true);
  public InputActionPress back;// = new ActionPress(unscaledTime: true);
  public ThrottleAxis upDown = new ThrottleAxis();
  public ThrottleAxis leftRight = new ThrottleAxis(wait: 0.2f);

  public void OnBack(InputAction.CallbackContext context) =>
    back.OnInputAction(context);

  public void OnConfirm(InputAction.CallbackContext context) =>
    confirm.OnInputAction(context);

  public void OnLeftRightMovement(InputAction.CallbackContext context) =>
    leftRight.OnInputAction(context);

  public void OnUpDownMovement(InputAction.CallbackContext context) =>
    upDown.OnInputAction(context);

  private void Update()
  {
    float dt = Time.unscaledDeltaTime;
    upDown.Update();
    leftRight.Update();
    confirm.Update(dt);
    back.Update(dt);
  }

  private void OnEnable()
  {
    if (inputActions == null)
    {
      inputActions = new InputActions();
      inputActions.Menu.SetCallbacks(this);
    }
    inputActions.Menu.Enable();
  }

  private void OnDisable()
  {
    if (inputActions != null)
    {
      inputActions.Menu.Disable();
    }
  }
}
