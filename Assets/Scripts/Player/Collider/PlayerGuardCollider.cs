using Kite;
using System;
using System.Collections;
using UnityEngine;

public class PlayerGuardCollider : MonoBehaviour, IPlayerUnitComponent
{
  public GuardCollider guardCollider;

  private PlayerUnitController controller;

  public bool GuardAgainst(EnemyCollider enemy)
  {
    if (enemy.guardable && IsGuardingAgainst(enemy.boxCollider))
    {
      Vector2 recoilForce = enemy.guardable.GetRecoilFor(controller);
      // controller.di.sfx.PlayGuard();
      //AudioSingleton.PlaySound(AudioSingleton.Instance.clips.guard);
      controller.di.stateMachine.SetRecoilState(recoilForce);
      enemy.guardable.HandleGuarded(controller);
      return true;
    }
    return false;
  }

  public bool IsGuardingAgainst(BoxCollider2D enemyCollider)
  {
    bool isGuarding = controller.di.abilities.action.IsGuarding;
    if (isGuarding)
    {
      float directionSign = controller.di.flip.Direction.ToFloat();
      return guardCollider.IsGuardingAgainst(directionSign, enemyCollider);
    }
    return false;
  }

  public void Inject(PlayerUnitDI di)
  {
    controller = di.controller;
  }
}