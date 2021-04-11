using Kite;
using System.Collections;
using UnityEngine;

public class PlayerUnitMergeable : MonoBehaviour, IPlayerUnitComponent
{
  private PlayerUnitController controller;

  public bool MergeWith(Collider2D collider)
  {
    PlayerUnitController otherController = InteractiveHelpers.GetPlayer(collider);
    if (otherController)
    {
      OnUnitCollision(otherController);
      return true;
    }
    return false;
  }

  public void OnUnitCollision(PlayerUnitController otherController)
  {
    bool thisAbleToMerge = AbleToMerge(controller);
    bool otherAbleToMerge = AbleToMerge(otherController);
    if (thisAbleToMerge && otherAbleToMerge)
    {
      if (CanMergeInside(controller))
      {
        MergeInside(otherController);
      }
      else if (CanMergeInside(otherController))
      {
        MergeInside(controller);
      }
    }
  }

  private bool CanMergeInside(PlayerUnitController controller)
  {
    bool statCanMerge = controller.di.stats.CanMerge;
    return statCanMerge;
  }

  private bool AbleToMerge(PlayerUnitController controller)
  {
    bool yeetingCanMerge = controller.di.stateMachine.yeetState.CanMerge();
    bool isVulnerable = !controller.di.vulnerability.flashSprite.IsFlashing;
    return yeetingCanMerge && isVulnerable;
  }

  private void MergeInside(PlayerUnitController controller)
  {
    controller.di.mainDi.unitHandler.OnSlimeMerge(controller.di.stats.SlimeType);
  }

  public void Inject(PlayerUnitDI di)
  {
    controller = di.controller;
  }
}