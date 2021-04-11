using System.Collections;
using UnityEngine;

public static class InteractiveHelpers
{
  public static PlayerUnitController GetPlayer(Collider2D collider)
  {
    if (!collider.CompareTag(UnityConstants.Tags.Player) || !collider.attachedRigidbody)
      return null;

    return collider.attachedRigidbody.GetComponent<PlayerUnitController>();
  }

  public static PushBlock GetBlock(Collider2D collider)
  {
    if (!collider.CompareTag(UnityConstants.Tags.Block))
      return null;

    return collider.transform.GetComponent<PushBlock>();
  }

  public static EnemyDamagable GetEnemy(Collider2D collider)
  {
    if (!collider.CompareTag(UnityConstants.Tags.Enemy))
      return null;

    return collider.transform.GetComponent<EnemyDamagable>();
  }
}