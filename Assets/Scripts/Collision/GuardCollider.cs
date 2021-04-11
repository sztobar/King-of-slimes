using System.Collections.Generic;
using UnityEngine;

public class GuardCollider : MonoBehaviour
{
  public new BoxCollider2D collider;
  public float tolerance;

  public bool IsGuardingAgainst(float directionSign, Collider2D other)
  {
    Vector2 guardingCenter = collider.bounds.center;
    Vector2 enemyCenter = other.bounds.center;
    Vector2 playerToEnemyDistance = enemyCenter - guardingCenter;
    return (directionSign * playerToEnemyDistance.x) + tolerance >= 0;
  }
}
