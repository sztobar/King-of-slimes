using Kite;
using System;
using UnityEngine;

public class PlayerWallSlideAbility : MonoBehaviour, IPlayerAbility
{

  public bool wallSlideEnabled;
  
  [Tooltip("Time to leap after player is no longer clicking arrow key")]
  public float noInputUnstickTime = 0.2f;
  
  [Tooltip("Time to leap after player is moving in opposite direction")]
  public float oppositeInputUnstickTime = 0.1f;

  public WallSlideRaycaster wallSlideRaycaster;
  public PlayerWallSlideJump jump;

  private PlayerPhysics physics;
  private PlayerInput input;
  private PlayerUnitStateMachine stateMachine;

  private Direction2H? wallSlideDirection;
  private float unstickTimeLeft;

  public void WallSlideUpdate()
  {
    wallSlideRaycaster.CheckWallCollision();
    float moveInput = input.movement.Value;

    if (input.jump.IsPressed())
    {
      input.jump.Use();
      jump.PerformJump(wallSlideDirection.Value, moveInput);
      stateMachine.SetControlState();
      wallSlideDirection = null;
      unstickTimeLeft = 0;
      return;
    }
    else
    {
      bool noMoveInput = moveInput == 0;
      Direction2H moveInputDirection = Direction2HHelpers.FromFloat(moveInput);

      if (noMoveInput)
      {
        unstickTimeLeft = noInputUnstickTime;
        stateMachine.SetControlState();
        return;
      }
      else if (!wallSlideRaycaster.IsTouchingWall(wallSlideDirection.Value))
      {
        unstickTimeLeft = oppositeInputUnstickTime;
        stateMachine.SetControlState();
        return;
      }
      else if (wallSlideDirection != moveInputDirection)
      {
        unstickTimeLeft = oppositeInputUnstickTime;
        stateMachine.SetControlState();
        return;
      }
    }
  }

  public void ControlUpdate()
  {
    if (!wallSlideEnabled)
    {
      return;
    }

    wallSlideRaycaster.CheckWallCollision();
    float moveInput = input.movement.Value;
    Direction2H moveInputDirection = Direction2HHelpers.FromFloat(moveInput);
    bool isFalling = !physics.IsGrounded && physics.velocity.Y < 0;
    bool isTouchingAnyWall = wallSlideRaycaster.IsTouchingAnyWall();
    bool isOrWasWallSliding = isTouchingAnyWall || unstickTimeLeft > 0;

    if (isFalling && isOrWasWallSliding && input.jump.IsPressed())
    {
      input.jump.Use();
      Direction2H wallSlideDirectionForJump = GetWallSlideDirectionForJump();
      jump.PerformJump(wallSlideDirectionForJump, moveInput);
      wallSlideDirection = null;
      unstickTimeLeft = 0;
      return;
    }
    else if (isFalling && moveInput != 0 && wallSlideRaycaster.IsTouchingWall(moveInputDirection))
    {
      wallSlideDirection = moveInputDirection;
      stateMachine.SetWallSlideState();
    }

    if (unstickTimeLeft > 0)
    {
      unstickTimeLeft -= Time.deltaTime;
      if (unstickTimeLeft <= 0)
      {
        wallSlideDirection = null;
      }
    }
  }

  public void InactiveUpdate()
  {
  }

  private Direction2H GetWallSlideDirectionForJump()
  {
    if (wallSlideDirection.HasValue)
    {
      return wallSlideDirection.Value;
    }
    if (wallSlideRaycaster.IsTouchingWall(Direction2H.Left))
    {
      return Direction2H.Left;
    }
    return Direction2H.Right;
  }

  public void Inject(PlayerUnitDI di)
  {
    physics = di.physics;
    input = di.mainDi.controller.input;
    stateMachine = di.stateMachine;
    jump.Inject(di, wallSlideRaycaster);
    ReadStats(di.stats.Data);
  }

  private void ReadStats(ScriptableUnit data)
  {
    wallSlideEnabled = data.CanWallSlide;
  }

  public interface IInjectable
  {
    void Inject(PlayerUnitDI di, WallSlideRaycaster wallSlideRaycaster);
  }
}
