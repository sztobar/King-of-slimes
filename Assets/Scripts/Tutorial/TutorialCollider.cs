using System.Collections;
using UnityEngine;

public class TutorialCollider : MonoBehaviour
{
  public ScriptableTutorial data;
  public SlimeInteractionPredicate predicate;

  private void OnTriggerEnter2D(Collider2D collider)
  {
    PlayerUnitController unit = InteractiveHelpers.GetPlayer(collider);
    if (unit && predicate.CanInteract(unit))
    {
      GameplayManager.instance.fsm.PushTutorial(data);
      Destroy(gameObject);
    }
  }
}