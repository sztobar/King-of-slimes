using Kite;
using System;
using System.Collections;
using UnityEngine;

public class RockbatPhysics : MonoBehaviour, IRockbatComponent
{
  public WorldTileFloat flyVelocity;
  public float flyTileVelocity;
  public float accelerationTime;
  public new Rigidbody2D rigidbody;

  private float dampVelocity;
  private float currentVelocity;

  public void Inject(Rockbat controller)
  {
  }

  internal void FlyToTarget(Vector2 moveDirection)
  {
    float targetVelocity = flyVelocity;
    if (currentVelocity != targetVelocity)
      currentVelocity = Mathf.SmoothDamp(currentVelocity, targetVelocity, ref dampVelocity, accelerationTime);

    Vector2 moveDelta = moveDirection * currentVelocity * Time.deltaTime;
    rigidbody.position += moveDelta;
  }
}
