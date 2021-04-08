using UnityEngine;
using System.Collections;
using System;

namespace Kite {
  [Serializable]
  public class ActionPressState {

    private readonly ActionPress actionPress;

    public ActionPressState(ActionPress actionPress) {
      this.actionPress = actionPress;
    }

    public bool IsPressed() =>
      actionPress.IsPressed();

    public void Cancel() =>
      actionPress.Cancel();

    public bool IsHeld() =>
      actionPress.IsHeld();

    public static ActionPressState Fake() => new ActionPress().State;
  }
}