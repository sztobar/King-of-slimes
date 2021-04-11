using UnityEngine;
using System.Collections;

namespace Kite {
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
  }
}