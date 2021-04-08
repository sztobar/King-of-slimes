using UnityEngine;

namespace Kite {
  public class AutoPhysicsComponent : MonoBehaviour {

    [SerializeField] private PhysicsVelocity velocity;
    [SerializeField] private PhysicsGravity gravity;
    [SerializeField] private PhysicsMovement movement;

    public void FixedUpdate() {
      Vector2 deltaPosition = velocity.DeltaPosition;
      Vector2 moveAmount = movement.TryToMove(deltaPosition);
      velocity.ResolveCollision(moveAmount);
      gravity.ApplyGravity();
    }
  }
}