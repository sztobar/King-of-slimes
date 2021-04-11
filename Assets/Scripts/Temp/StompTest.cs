using Kite;
using System.Collections;
using UnityEngine;

public class StompTest : MonoBehaviour
{
  public StompCollider stompCollider;

  [Header("Debug")]
  public bool isStomping;
  public bool isCollision;
  public Vector2 collideDistance;

  public void OnTriggerEnter2D(Collider2D collider)
  {
    isCollision = true;
    isStomping = IsGuardingAgainst(collider);
    collideDistance = collider.bounds.center - stompCollider.collider.bounds.center;
  }

  public void OnTriggerStay2D(Collider2D collider)
  {
    isStomping = IsGuardingAgainst(collider);
    collideDistance = collider.bounds.center - stompCollider.collider.bounds.center;
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    isCollision = false;
    collideDistance = Vector2.zero;
  }

  private bool IsGuardingAgainst(Collider2D collider)
  {
    return stompCollider.CanStomp(collider);
  }

  private void OnDrawGizmos()
  {
    if (isCollision)
    {
      Gizmos.color = isStomping ? Color.blue : Color.red;
      Gizmos.DrawCube(stompCollider.collider.bounds.center, stompCollider.collider.bounds.size);
    }
  }
}