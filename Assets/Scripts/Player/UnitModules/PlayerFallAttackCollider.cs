using UnityEngine;
using System;

[Obsolete]
public class PlayerFallAttackCollider : MonoBehaviour, IPlayerUnitComponent
{
  public BoxCollider2D boxCollider;
  public float tileRecoilOnHit;

  private PlayerUnitController controller;
  private PlayerPhysics physics;
  private bool isSword;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    bool isFalling = physics.velocity.Y < 0;
    if (isFalling)
    {
      EnemyDamagable damagable = InteractiveHelpers.GetEnemy(collision);
      if (damagable)
      {
        damagable.TakeDamageFrom(boxCollider, controller);
        if (damagable.HasRecoilAfterDamage())
        {
          controller.di.stateMachine.SetRecoilState(damagable.GetRecoilAfterDamage());
        }
        else
        {
          Vector2 playerRecoil = RecoilHelpers.GetRecoilFromTo(controller.transform, controller.transform, tileRecoilOnHit);
          Debug.Log(playerRecoil);
          physics.velocity.Value = playerRecoil;
          //controller.StateMachine.SetRecoilState(playerRecoil);
        }
      }
    }
  }

  public bool IsSwordFalling(EnemyDamagable enemy)
  {
    if (!isSword)
    {
      return false;
    }
    Vector2 playerCenter = controller.di.boxCollider.bounds.center;
    Vector2 enemyCenter = enemy.ColliderCenter;
    Vector2 playerToEnemyDistance = enemyCenter - playerCenter;
    bool enemyBelow = playerToEnemyDistance.y < 0;
    bool isFalling = !physics.IsGrounded;
    return enemyBelow && isFalling;
  }

  public void Inject(PlayerUnitDI di)
  {
    controller = di.controller;
    physics = di.physics;
    isSword = di.stats.SlimeType == SlimeType.Sword;
  }
}