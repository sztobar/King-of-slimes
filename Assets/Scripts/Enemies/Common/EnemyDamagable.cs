using Kite;
using System;
using System.Collections;
using UnityEngine;

public class EnemyDamagable : MonoBehaviour
{
  public WorldTileFloat recoilForce;
  public delegate void TakeDamageHandler(PlayerUnitController player);
  public event TakeDamageHandler OnTakeDamage;

  public virtual Vector2 GetRecoilFor(PlayerUnitController player) =>
    RecoilHelpers.GetRecoilFromTo(player.transform, transform, recoilForce);
  public void HandleTakeDamage(PlayerUnitController player) => OnTakeDamage(player);

  // Center of attached collider
  [Obsolete] public virtual Vector2 ColliderCenter { get; }

  // On enemy collision with player attack
  [Obsolete] public virtual void TakeDamageFrom(BoxCollider2D damageCollider, PlayerUnitController player) { }

  // Is there a recoil for player after a successful damage dealt
  [Obsolete] public virtual bool HasRecoilAfterDamage() => false;

  // Recoil for player after successful damage dealt
  [Obsolete] public virtual Vector2 GetRecoilAfterDamage() => Vector2.zero;

  // Recoil for player on collision when successfully guarded 
  [Obsolete] public virtual Vector2 GetGuardedRecoil(PlayerUnitController player) => Vector2.zero;

  // Recoil for player on collision
  [Obsolete] public virtual Vector2 GetCollisionRecoil(PlayerUnitController player) => Vector2.zero;

  // Damage for player on collision
  [Obsolete] public virtual int CollisionDamage { get; }

  // Check if enemy collision can be guarded
  [Obsolete] public virtual bool IsGuardingWorks(PlayerUnitController player) => true;

  // Is there a damage on collision with this enemy
  [Obsolete] public virtual bool DealDamageOnCollision() => true;

  // After collision with a player
  [Obsolete] public virtual void OnPlayerDamageCollision(PlayerUnitController player) { }

  // After collision with a player on successful guard
  [Obsolete] public virtual void OnPlayerGuardCollision(PlayerUnitController player) { }
}