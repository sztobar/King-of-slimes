using Kite;
using System;
using System.Collections;
using UnityEngine;

public class EnemyCollisionAttack : MonoBehaviour
{
  public int damage;
  public WorldTileFloat recoilForce;

  public delegate void CollisionAttackHandler(PlayerUnitController player);
  public event CollisionAttackHandler OnCollisionAttack;

  public void HandleCollisionAttack(PlayerUnitController player) => OnCollisionAttack(player);

  public int GetDamage() => damage;

  public Vector2 GetRecoilFor(PlayerUnitController controller) =>
    RecoilHelpers.GetRecoilFromTo(controller.transform, transform, recoilForce);

}