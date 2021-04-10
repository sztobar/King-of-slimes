using UnityEngine;
using System.Collections;
using System;

namespace Kite {

  [Serializable]
  public class ActionPressState {

    private readonly InputActionPress actionPress;

    public ActionPressState(InputActionPress actionPress) {
      this.actionPress = actionPress;
    }

    public bool IsPressed() =>
      actionPress.IsPressed();

    public void Use() =>
      actionPress.Use();

    public bool IsHeld() =>
      actionPress.IsHeld();
  }
}