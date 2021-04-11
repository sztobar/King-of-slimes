using Kite;
using System.Collections;
using UnityEngine;

public class GuardTest : MonoBehaviour
{
  public GuardCollider guardCollider;
  public DirX dirX;

  [Header("Debug")]
  public bool isGuarding;
  public bool isCollision;
  public Vector2 collideDistance;

  public void OnTriggerEnter2D(Collider2D collider)
  {
    isCollision = true;
    isGuarding = IsGuardingAgainst(collider);
    collideDistance = collider.bounds.center - guardCollider.collider.bounds.center;
  }

  public void OnTriggerStay2D(Collider2D collider)
  {
    isGuarding = IsGuardingAgainst(collider);
    collideDistance = collider.bounds.center - guardCollider.collider.bounds.center;
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    isCollision = false;
    collideDistance = Vector2.zero;
  }

  private bool IsGuardingAgainst(Collider2D collider)
  {
    return guardCollider.IsGuardingAgainst(transform.right.x, collider);
  }

  private void OnDrawGizmos()
  {
    if (isCollision)
    {
      Gizmos.color = isGuarding ? Color.blue : Color.red;
      Gizmos.DrawCube(guardCollider.collider.bounds.center, guardCollider.collider.bounds.size);
    }
  }
}