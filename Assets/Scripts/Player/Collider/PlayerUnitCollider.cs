using Kite;
using System;
using UnityEngine;

public class PlayerUnitCollider : MonoBehaviour, IPlayerUnitComponent
{
  public BoxCollider2D boxCollider;
  public PlayerGuardCollider guard;
  public PlayerUnitMergeable merge;
  public PlayerStompCollider stomp;
  public PlayerDamageModule damage;
  public PlayerVulnerability vulnerability;

  private PlayerUnitController controller;

  private void OnTriggerEnter2D(Collider2D collision) =>
    CheckCollision(collision);

  private void OnTriggerStay2D(Collider2D collision) =>
    CheckCollision(collision);

  private void CheckCollision(Collider2D collider)
  {
    if (controller.di.hp.IsDead)
      return;

    if (merge.MergeWith(collider))
      return;

    EnemyCollider enemy = collider.GetComponent<EnemyCollider>();
    if (enemy)
    {
      if (guard.GuardAgainst(enemy))
        return;

      if (stomp.StompOn(enemy))
        return;

      if (enemy.attack)
        ReceieveDamageFrom(enemy);
    }
  }

  private void ReceieveDamageFrom(EnemyCollider enemy)
  {
    // TODO: move to player damage?
    Vector2 pushVector = enemy.attack.GetRecoilFor(controller);
    if (vulnerability.IsVulnerable())
      damage.TakeDamage(enemy.attack.GetDamage(), pushVector);
    else
      damage.TakeDamage(0, pushVector);

    enemy.attack.HandleCollisionAttack(controller);
  }

  public void Inject(PlayerUnitDI di)
  {
    controller = di.controller;

    merge.Inject(di);
    guard.Inject(di);
    stomp.Inject(di);
  }
}
