using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kite {
  public class ActionPress {

    private readonly float pressTime;
    private readonly bool unscaledTime;
    private float elapsedTimeLeft;
    private bool actionHeld;

    private float DeltaTime => unscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

    public ActionPressState State { get; }

    public ActionPress(float pressTime = 0.1f, bool unscaledTime = false) {
      this.pressTime = pressTime;
      this.unscaledTime = unscaledTime;
      State = new ActionPressState(this);
    }

    public void Update() {
      if (IsPressed()) {
        elapsedTimeLeft -= DeltaTime;
      }
    }

    public void Press() {
      elapsedTimeLeft = pressTime;
    }

    public bool IsPressed() {
      return elapsedTimeLeft > 0;
    }

    public void Cancel() {
      elapsedTimeLeft = 0;
    }

    public bool IsHeld() {
      return actionHeld;
    }

    public void OnInputAction(InputAction.CallbackContext context) {
      if (context.performed) {
        actionHeld = true;
        Press();
      } else if (context.canceled) {
        actionHeld = false;
        Cancel();
      }
    }
  }
}