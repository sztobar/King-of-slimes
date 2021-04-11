using System.Collections;
using UnityEngine;

public class PlayerSelectUnitAbility : MonoBehaviour, IPlayerAbility {

  private PlayerInput input;
  private PlayerUnitHandler unitHandler;
  private BasePlayerActionAbility action;

  public void ControlUpdate() {
    CheckForSlimeChange();
  }

  public void WallSlideUpdate() {
    CheckForSlimeChange();
  }

  public void InactiveUpdate() {}

  public void Inject(PlayerUnitDI di) {
    unitHandler = di.mainDi.unitHandler;
    input = di.mainDi.controller.input;
    action = di.abilities.action;
  }

  public void CheckForSlimeChange() {
    if (action.IsAttacking) {
      return;
    }
    if (input.selectSlime.IsPressed()) {
      input.selectSlime.Use();
      SlimeType pressedType = input.selectSlime.GetPressedSlime();
      unitHandler.SelectSlime(pressedType);
    }
  }
}