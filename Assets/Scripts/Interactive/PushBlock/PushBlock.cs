using UnityEngine;
using Kite;
using System.Collections.Generic;

public class PushBlock : MonoBehaviour
{
  public PushBlockPhysics physics;
  [Range(2, 5)]
  public int requiredStrength = 2;
  public PhysicsMovement movement;
  public PhysicsVelocity velocity;
  public PhysicsGravity gravity;
  public CarryEffector carryEffector;
  public SpriteRenderer spriteRenderer;
  public List<Sprite> sprites;

  private bool isGrounded;

  private void FixedUpdate()
  {
    //if (Application.isPlaying)
    //{
      physics.PhysicsUpdate();
      //Vector2 moveAmount = movement.TryToMove(velocity.DeltaPosition);
      //velocity.ResolveCollision(moveAmount);
      //isGrounded = moveAmount.y == 0;
      //gravity.ApplyGravity();
    //}
  }

  //private void Update()
  //{
  //  if (!Application.isPlaying)
  //  {
  //    if (spriteRenderer && sprites.Count >= requiredStrength)
  //    {
  //      int spriteIndex = requiredStrength - 1;
  //      if (sprites[spriteIndex])
  //      {
  //        spriteRenderer.sprite = sprites[spriteIndex];
  //      }
  //    }
  //  }
  //}

  //public void OnPhysicsMoveInto(Transform moving, float collideDistance, Direction4 direction, Vector2 hitPoint)
  //{
  //  PlayerUnitController player = moving.GetComponent<PlayerUnitController>();
  //  if (PushConditionsPass(player, direction))
  //  {
  //    player.di.physics.pushHandler.SetIsPushing();

  //    if (!RaycastHelpers.IsValidDistance(collideDistance))
  //      return;
      
  //    if (player.di.physics.pushHandler.CanPush(requiredStrength))
  //    {
  //      movement.ForceMove(collideDistance, direction);
  //      carryEffector.Carry(collideDistance, direction);
  //    }
  //  }
  //}

  //public float GetAllowedMoveInto(Transform wantsToMove, float collideDistance, Direction4 direction, Vector2 hitPoint)
  //{
  //  PlayerUnitController player = wantsToMove.GetComponent<PlayerUnitController>();
  //  if (PushConditionsPass(player, direction) && player.di.physics.pushHandler.CanPush(requiredStrength))
  //  {
  //    float allowedMove = movement.GetAllowedMovement(collideDistance, direction);
  //    return allowedMove;
  //  }
  //  return 0;
  //}

  //private bool PushConditionsPass(PlayerUnitController player, Direction4 direction) =>
  //  isGrounded &&
  //  direction.IsHorizontal() &&
  //  player != null;
}
