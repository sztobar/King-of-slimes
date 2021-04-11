using Kite;
using System;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour, IPlayerUnitComponent
{
  public PhysicsVelocity velocity;
  public PhysicsGravity gravity;
  public PhysicsMovement movement;
  public PhysicsForce force;
  public CornerCorrectionMovement cornerCorrection;
  public PhysicsUnstuck unstuck;
  public PlayerPhysicsCollidable collidable;
  public StandEffectable standEffectable;
  public PlayerPushEffectable pushEffectable;

  private bool isGrounded;
  public PlayerWalkState walkState;

  private BasePlayerAnimator animator;
  private PlayerUnitController controller;
  private PlayerInput input;
  private BasePlayerActionAbility action;

  public PlayerUnitController Controller => controller;

  public bool IsGrounded
  {
    get => isGrounded;
    set => isGrounded = value;
  }

  public void ControlUpdate()
  {
    BeforeMove();
    Vector2 deltaPosition = GetDeltaPosition();
    Vector2 correctedPosition = CorrectCorner(deltaPosition);
    if (correctedPosition.x != 0)
      deltaPosition.x = 0;
    Vector2 moveAmount = movement.TryToMove(deltaPosition);
    ControlledMoveEffects(moveAmount);
  }

  private void ControlledMoveEffects(Vector2 moveAmount)
  {
    velocity.ResolveCollision(moveAmount);
    IsGrounded = !movement.CanMoveInDirection(Dir4.down);
    AnimationEffects();
    gravity.ApplyGravity();
  }

  public void PhysicsReset()
  {
    velocity.Value = Vector2.zero;
  }

  public void YeetUpdate() => UncontrolledMoveUpdate();

  public void InactiveUpdate() => UncontrolledMoveUpdate();

  public void WalSlideUpdate()
  {
    BeforeMove();
    Vector2 moveAmount = movement.TryToMove(velocity.DeltaPosition);
    WallSlideMoveEffects(moveAmount);
  }

  private void WallSlideMoveEffects(Vector2 moveAmount)
  {
    velocity.ResolveCollision(moveAmount);
    IsGrounded = !movement.CanMoveInDirection(Dir4.down);
    animator.PlaySlide();
    gravity.ApplyGravity();
  }

  public void DeadUpdate() {}

  public void ParalyzedUpdate()
  {
    BeforeMove();
    Vector2 moveAmount = movement.TryToMove(velocity.DeltaPosition);
    ParalyzedMoveEffects(moveAmount);
  }

  private void ParalyzedMoveEffects(Vector2 moveAmount)
  {
    velocity.ResolveCollision(moveAmount);
    IsGrounded = !movement.CanMoveInDirection(Dir4.down);

    if (IsGrounded)
      animator.PlayIdle();
    else if (moveAmount.y > 0)
      animator.PlayJump();
    else
      animator.PlayFall();

    gravity.ApplyGravity();
  }

  private Vector2 UncontrolledMoveUpdate()
  {
    BeforeMove();
    Vector2 moveAmount = movement.TryToMove(velocity.DeltaPosition);
    UncontrolledMoveEffects(moveAmount);
    return moveAmount;
  }

  private Vector2 GetDeltaPosition()
  {
    Vector2 deltaPosition = velocity.DeltaPosition;
    return deltaPosition;
  }

  private void UncontrolledMoveEffects(Vector2 moveAmount)
  {
    velocity.ResolveCollision(moveAmount);
    IsGrounded = !movement.CanMoveInDirection(Dir4.down);
    AnimationEffects();
    gravity.ApplyGravity();
  }

  private void ResolveVelocityOnBothAxes(Vector2 moveAmount)
  {
    velocity.ResolveCollision(moveAmount);
  }

  // TODO: Move to respective states
  private void AnimationEffects()
  {
    if (action.IsAttacking)
      return;

    if (IsGrounded)
    {
      bool isSelected = controller.di.selectable.IsSelected;
      if (isSelected && input.movement.Value != 0)
        if (pushEffectable.effectable.isPushing)
          animator.PlayPushBlock();
        else
          animator.PlayWalk();
      else
        animator.PlayIdle();
    }
    else
    {
      if (velocity.Y > 0)
        animator.PlayJump();
      else
        animator.PlayFall();
    }
  }

  private Vector2 CorrectCorner(Vector2 deltaPosition)
  {
    Vector2 cornerCorrectionMove = cornerCorrection.GetCornerMoveCorrection(deltaPosition);
    return movement.TryToMove(cornerCorrectionMove);
  }

  private void BeforeMove()
  {
    standEffectable.EffectableReset();
    if (pushEffectable.effectable.isPushing)
    {
      velocity.X *= pushEffectable.pushVelocityMultiplier;
      pushEffectable.effectable.EffectableReset();
    }
    pushEffectable.effectable.canPush = IsGrounded && walkState.HasSameSign(velocity.X);
    force.PhysicsUpdate();
  }

  public void Inject(PlayerUnitDI di)
  {
    controller = di.controller;
    input = di.mainDi.controller.input;
    animator = di.animator;
    action = di.abilities.action;

    collidable.Inject(di);
    pushEffectable.Init(di.stats);
  }
}