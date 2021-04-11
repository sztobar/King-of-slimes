using Kite;
using System.Collections;
using UnityEngine;

public class PushBlockPhysics : MonoBehaviour
{
  [Range(2, 5)] public int requiredStrength = 2;
  public PhysicsMovement movement;
  public PhysicsVelocity velocity;
  public PhysicsGravity gravity;
  public CarryEffector carryEffector;
  public PushEffector pushEffector;

  public bool IsGrounded { get; private set; }

  public float Move(float distance, Dir4 dir)
  {
    float moveAmount = movement.TryToMove(distance, dir);
    if (dir != Dir4.up)
      MoveEffectables(dir, moveAmount);
    return moveAmount;
  }

  public Vector2 Move(Vector2 moveAmount)
  {
    float x = Move(Mathf.Abs(moveAmount.x), Dir4.FromXFloat(moveAmount.x));
    float y = Move(Mathf.Abs(moveAmount.y), Dir4.FromYFloat(moveAmount.y));
    return new Vector2(x, y);
  }

  public void PhysicsUpdate()
  {
    Vector2 deltaPosition = velocity.DeltaPosition;
    Vector2 moveAmount = Move(deltaPosition);
    velocity.ResolveCollision(moveAmount);
    IsGrounded = velocity.Y == 0;
    gravity.ApplyGravity();
  }

  private void MoveEffectables(Dir4 dir, float moveAmount)
  {
    foreach (StandEffectable effectable in carryEffector.GetEffectables())
    {
      PhysicsMovement movement = effectable.GetComponent<PhysicsMovement>();
      movement.TryToMove(moveAmount, dir);
    }
  }
}