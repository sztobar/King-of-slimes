using UnityEngine;
using Kite;
using System.Collections.Generic;

public class PushBlock : MonoBehaviour
{
  public PushBlockPhysics physics;
  public PhysicsMovement movement;
  public PhysicsVelocity velocity;
  public PhysicsGravity gravity;
  public CarryEffector carryEffector;
  public SpriteRenderer spriteRenderer;
  public List<Sprite> sprites;

  private void FixedUpdate()
  {
    physics.PhysicsUpdate();
  }
}
