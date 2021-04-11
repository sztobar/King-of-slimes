using UnityEngine;
using Kite;
using System;

public class PlayerMovementAbility : MonoBehaviour, IPlayerAbility {

  [SerializeField] private float tileVelocity = 8;

  [SerializeField] private MovementAcceleration groundedMovement;
  [SerializeField] private MovementAcceleration aerialMovement;

  [SerializeField] private bool debug;

  public AudioSource audioSource;

  private HorizontalFlipComponent flip;
  private PlayerInput input;
  private PlayerPhysics physics;

  private float smoothDampAccelerateVelocity;
  private float smoothDampDecelerateVelocity;
  private bool wasGrounded;
  private float velocity;

  public float WorldVelocity => TileHelpers.TileToWorld(tileVelocity);

  public void ControlUpdate() {
    float movementInput = input.movement.Value;

    if (physics.IsGrounded) {
      if (!wasGrounded) {
        smoothDampAccelerateVelocity = 0;
        smoothDampDecelerateVelocity = 0;
        wasGrounded = true;
      }
      velocity = GetVelocity(movementInput, groundedMovement);
    } else {
      if (wasGrounded) {
        smoothDampAccelerateVelocity = 0;
        smoothDampDecelerateVelocity = 0;
        wasGrounded = false;
      }
      velocity = GetVelocity(movementInput, aerialMovement);
    }
    physics.velocity.X = velocity;
    physics.walkState = PlayerWalkStateHelpers.FromFloat(movementInput);
    if (movementInput != 0) {
      flip.Direction = Direction2HHelpers.FromFloat(movementInput);

      if (!audioSource.isPlaying && physics.IsGrounded)
      {
        audioSource.Play();
      }
    }
    else if (audioSource.isPlaying)
    {
      audioSource.Stop();
    }
  }

  internal void ResetVelocity()
  {
    velocity = 0;
  }

  public void InactiveUpdate() {
    if (velocity != 0) {
      if (physics.IsGrounded) {
        velocity = GetStoppedVelocityX(groundedMovement);
      } else {
        velocity = GetStoppedVelocityX(aerialMovement);
      }
    }
    physics.velocity.X = velocity;
  }

  public void DeadUpdate() {
    if (velocity != 0 && physics.IsGrounded) {
      velocity = GetStoppedVelocityX(groundedMovement);
      physics.velocity.X = velocity;
    }
  }

  public void WallSlideUpdate() { }

  public void Inject(PlayerUnitDI di) {
    physics = di.physics;
    input = di.mainDi.controller.input;
    flip = di.flip;
    ReadStats(di.stats.Data);
  }

  private void ReadStats(ScriptableUnit data) {
    tileVelocity = data.TileVelocity;
    groundedMovement = data.GroundedMovement;
    aerialMovement = data.AerialMovement;
  }

  private float GetVelocity(float moveInput, MovementAcceleration movement) {
    if (moveInput != 0) {
      return GetMovingVelocityX(moveInput, movement);
    } else {
      return GetStoppedVelocityX(movement);
    }
  }

  private float GetMovingVelocityX(float moveInput, MovementAcceleration movement) {
    smoothDampDecelerateVelocity = 0f;
    float velocityX;
    if (movement.instantDirectionChange) {
      velocityX = Mathf.SmoothDamp(Mathf.Abs(velocity), Mathf.Abs(moveInput) * WorldVelocity, ref smoothDampAccelerateVelocity, movement.timeToFullSpeed);
      velocityX *= Mathf.Sign(moveInput);
    } else {
      float targetVelocity = moveInput * WorldVelocity;
      velocityX = Mathf.SmoothDamp(velocity, targetVelocity, ref smoothDampAccelerateVelocity, movement.timeToFullSpeed);
    }
    return velocityX;
  }

  private float GetStoppedVelocityX(MovementAcceleration movement) {
    smoothDampAccelerateVelocity = 0f;
    float currentVelocityX = velocity;
    float velocityX = Mathf.Sign(currentVelocityX) * Mathf.SmoothDamp(Mathf.Abs(currentVelocityX), 0f, ref smoothDampDecelerateVelocity, movement.timeToStop);
    return velocityX;
  }
}
