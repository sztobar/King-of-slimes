using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kite
{
  public class PhysicsForce : MonoBehaviour
  {
    public PhysicsGravity gravity;
    public PhysicsVelocity velocity;
    
    private Vector2 value;
    private float duration;
    private float elapsedTime;
    private DerivativeHelpers.Curve curve;
    private bool hasForce;

    public void SetForce(Vector2 value, float duration, DerivativeHelpers.Curve curve)
    {
      this.value = value;
      this.duration = duration;
      this.curve = curve;
      elapsedTime = 0;
      hasForce = true;
      ApplyForce();
    }

    private void ApplyForce()
    {
      if (value.y > 0)
        velocity.Y = gravity.JumpVelocity(value.y);
      else
        velocity.Y = value.y;
    }

    public void PhysicsUpdate()
    {
      if (!hasForce)
        return;

      velocity.X = value.x * DerivativeHelpers.ForwardDerivative(elapsedTime, duration, curve);

      if (elapsedTime >= duration)
      {
        hasForce = false;
      }
      else
      {
        elapsedTime += Time.deltaTime;
      }
    }

    public bool HasForce() => hasForce;
  }
}