using Kite;
using System.Collections;
using UnityEngine;

public class PlayerStompCollider : MonoBehaviour, IPlayerUnitComponent
{
  public StompCollider stompCollider;

  private PlayerUnitController controller;
  private bool dealDamageOnStomp;

  public bool StompOn(EnemyCollider enemy)
  {
    if (dealDamageOnStomp && stompCollider.CanStomp(enemy.boxCollider))
    {
      Vector2 stompRecoil = enemy.stompable.GetStompRecoil();
      // controller.di.sfx.PlayGuard();
      //AudioSingleton.PlaySound(AudioSingleton.Instance.clips.guard);
      controller.di.physics.velocity.Value = new Vector2(
        stompRecoil.x,
        controller.di.physics.gravity.JumpVelocity(stompRecoil.y)
      );
      enemy.stompable.HandleStomped(controller);
      return true;
    }
    return false;
  }

  public void Inject(PlayerUnitDI di)
  {
    controller = di.controller;
    dealDamageOnStomp = di.stats.SlimeType == SlimeType.Sword;
  }
}