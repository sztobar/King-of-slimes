using Kite;
using System.Collections;
using UnityEngine;

public abstract class EnemyCollider : MonoBehaviour
{
  public BoxCollider2D boxCollider;
  public EnemyDamagable damagable;
  public EnemyStompable stompable;
  public EnemyGuardable guardable;
  public EnemyCollisionAttack attack;

  protected virtual void OnGuarded(PlayerUnitController player) { }
  protected virtual void OnStomped(PlayerUnitController player) { }
  protected virtual void OnCollisionAttack(PlayerUnitController player) { }
  protected virtual void OnTakeDamage(PlayerUnitController player) { }

  private void Awake()
  {
    damagable.OnTakeDamage += OnTakeDamage;
    stompable.OnStomped += OnStomped;
    guardable.OnGuarded += OnGuarded;
    attack.OnCollisionAttack += OnCollisionAttack;
  }
}