using UnityEngine;

namespace Kite
{
  public class PhysicsVelocity : MonoBehaviour
  {
    private static readonly float MIN_COLLISION_MAGNITUDE = 0.01f;

    public Vector2 velocity;
    public Vector2 bounciness;

    public Vector2 Value
    {
      get => velocity;
      set => velocity = value;
    }

    public float X
    {
      get => velocity.x;
      set => velocity.x = value;
    }

    public float Y
    {
      get => velocity.y;
      set => velocity.y = value;
    }

    public Vector2 DeltaPosition => velocity * Time.deltaTime;

    public void ResolveCollision(Vector2 moveAmount)
    {
      ResolveCollisionX(moveAmount.x);
      ResolveCollisionY(moveAmount.y);
    }

    public void ResolveCollisionX(float moveAmount)
    {
      ResolveCollision(moveAmount, 0);
    }

    public void ResolveCollisionY(float moveAmount)
    {
      ResolveCollision(moveAmount, 1);
    }

    private void ResolveCollision(float moveAmount, int axis)
    {
      float absEffectiveVelocity = Mathf.Abs(moveAmount / Time.deltaTime);
      float absVelocity = Mathf.Abs(velocity[axis]);
      if (absVelocity - absEffectiveVelocity >= MIN_COLLISION_MAGNITUDE)
      {
        if (bounciness[axis] != 0)
        {
          velocity[axis] *= -bounciness[axis];
        }
        else
        {
          float velocitySign = Mathf.Sign(velocity[axis]);
          velocity[axis] = velocitySign * absEffectiveVelocity;
        }
      }
    }
  }
}