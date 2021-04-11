using Kite;
using System;
using System.Collections;
using UnityEngine;

public class EnemyPhysics : MonoBehaviour {

  public PhysicsMovement movement;
  public PhysicsVelocity velocity;
  public PhysicsGravity gravity;

  internal void MoveUpdate(Vector2 moveVelocity) {
    velocity.Value = moveVelocity;
    Vector2 deltaPosition = velocity.DeltaPosition;
    Vector2 moveAmount = movement.TryToMove(deltaPosition);
    velocity.ResolveCollisionY(moveAmount.y);
    gravity.ApplyGravity();
  }
}
