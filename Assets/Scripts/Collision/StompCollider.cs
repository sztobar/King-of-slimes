using Kite;
using System.Collections;
using UnityEngine;

public class StompCollider : MonoBehaviour
{
  public new BoxCollider2D collider;
  public float tolerance;

  public bool CanStomp(Collider2D other)
  {
    float otherTop = other.bounds.max.y;
    float thisBottom = collider.bounds.min.y;
    return otherTop - thisBottom <= tolerance;
  }
}