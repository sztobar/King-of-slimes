using System.Collections;
using UnityEngine;

public class HeartsInteractionPredicate : InteractionPredicate {

  [SerializeField, Range(1, 5)] int heartsRequired;

  public override bool CanInteract(PlayerUnitController controller) {
    return controller.di.stats.Hearts >= heartsRequired;
  }
}
