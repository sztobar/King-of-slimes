using Kite;
using System;
using System.Collections;
using UnityEngine;

public class PlayerDamageModule : MonoBehaviour, IPlayerUnitComponent {

  private PlayerPhysics physics;
  private PlayerHPModule hp;
  private PlayerUnitStateMachine stateMachine;
  private PlayerVulnerability vulnerability;

  public void Inject(PlayerUnitDI di) {
    physics = di.physics;
    stateMachine = di.stateMachine;
    hp = di.hp;
    vulnerability = di.vulnerability;
  }

  public void TakeDamage() => TakeDamage(1, Vector2.zero);
  public void TakeDamage(int damage) => TakeDamage(damage, Vector2.zero);
  public void TakeDamage(int damage, Vector2 push) {
    hp.TakeDamage(damage);
    //physics.Velocity.Value = push;
    if (hp.IsDead) {
      stateMachine.SetDeadState();
    } else
    {
      stateMachine.SetHitState(push);
    }
  }

  public void TakePoisonDamage() {
    hp.TakeDamage(1);
    if (hp.IsDead) {
      stateMachine.SetDeadState();
    }
    else
    {
      vulnerability.SetInvulnerable();
    }
  }

  public void TakeFullDamage() => TakeFullDamage(Vector2.zero);
  public void TakeFullDamage(Vector2 push) {
    int currentHp = hp.CurrentHP;
    TakeDamage(currentHp, push);
  }
}
