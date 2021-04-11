using Kite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBlock : MonoBehaviour
{
  [Range(2, 5)]
  public int requiredStrength = 2;
  public PhysicsMovement movement;
  public PhysicsVelocity velocity;
  public PhysicsGravity gravity;
  public CarryEffector carryEffector;
  public SpriteRenderer spriteRenderer;
  public List<Sprite> sprites;
}