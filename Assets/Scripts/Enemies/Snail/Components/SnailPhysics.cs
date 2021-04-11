using Kite;
using NaughtyAttributes;
using UnityEngine;

public class SnailPhysics : MonoBehaviour, ISnailComponent
{
  private static readonly Quaternion noRotation = Quaternion.identity;
  private static readonly Quaternion clockwise90Rotation = Quaternion.Euler(0, 0, -90);
  private static readonly Quaternion counterClockwise90Rotation = Quaternion.Euler(0, 0, 90);

  public float moveTileSpeed;
  public float groundCheckDistance;

  [Header("Go Around")]
  public bool goAround;
  [ShowIf("goAround")] public float edgeMoveTileSpeed;
  [ShowIf("goAround")] public float forwardMoveAfterRotation;
  [ShowIf("goAround")] public int rotationFrames = 5;

  [Header("Puff")]
  public bool canPuff;
  [ShowIf("canPuff")] public float puffDelay;

  [Header("Components")]
  public PhysicsMovement movement;
  public PhysicsVelocity velocity;
  public PhysicsGravity gravity;
  public SpriteRenderer spriteRenderer;

  private SnailRotation rotation;
  private SnailRaycaster raycaster;
  private SnailAnimator animator;
  private int rotationFramesLeft;
  private bool checkIsGrounded = true;
  private bool hasRbRotated;

  public void PhysicsUpdate()
  {
    if (checkIsGrounded)
    {
      FallingUpdate();
      return;
    }
    
    if (rotationFramesLeft > 0)
      RotationUpdate();

    if (canPuff && animator.IsState(SnailAnimatorState.Puff))
      return;

    MoveUpdate();
  }

  private void MoveUpdate()
  {
    if (goAround)
      CheckForSpriteRotation();
    MoveForwardUpdate();
  }

  private void MoveForwardUpdate()
  {
    float forwardTileSpeed = rotationFramesLeft == 0 ? moveTileSpeed : edgeMoveTileSpeed;
    Vector2 forwardDeltaMove = TileHelpers.TileToWorld(forwardTileSpeed) * rotation.GetFrontVector() * Time.deltaTime;
    Vector2 moveAmount = movement.TryToMove(forwardDeltaMove);

    if (raycaster.HasWallInFront())
      ReverseSnail();
    else if (raycaster.HasNoColliderBelow())
      if (goAround)
        RotateSnail90();
      else
        ReverseSnail();
  }

  public void ReverseSnail()
  {
    rotation.Reverse();
    if (rotationFramesLeft > 0)
    {
      rotationFramesLeft = 0;
      if (hasRbRotated)
        spriteRenderer.transform.localRotation = noRotation;
      else
        spriteRenderer.transform.localRotation = clockwise90Rotation;

      hasRbRotated = false;
    }
  }

  private void CheckForSpriteRotation()
  {
    if (rotationFrames > 0 && rotationFramesLeft == 0)
    {
      Vector2 deltaMoveCheckForRotation = TileHelpers.TileToWorld(edgeMoveTileSpeed) * rotation.GetFrontVector() * Time.deltaTime * 0.5f * rotationFrames;
      if (raycaster.HasNoColliderBelow(deltaMoveCheckForRotation))
      {
        rotationFramesLeft = rotationFrames;
        float t = 1 - (float)rotationFramesLeft / rotationFrames;
        spriteRenderer.transform.localRotation = Quaternion.Lerp(noRotation, counterClockwise90Rotation, t);
      }
    }
  }

  private void FallingUpdate()
  {
    gravity.ApplyGravity();
    Vector2 gravityDeltaPosition = rotation.GetRotatedVector(Vector2.down * groundCheckDistance);
    Vector2 gravityMoveAmount = movement.TryToMove(gravityDeltaPosition);
    if (gravityMoveAmount.magnitude < RaycastHelpers.skinWidth * 2)
      checkIsGrounded = false;
  }

  private void RotationUpdate()
  {
    if (canPuff && animator.IsState(SnailAnimatorState.Puff))
      rotationFramesLeft = 0;
    else
      rotationFramesLeft--;

    float t = 1 - (float)rotationFramesLeft / rotationFrames;

    if (hasRbRotated)
      spriteRenderer.transform.localRotation = Quaternion.Lerp(counterClockwise90Rotation, noRotation, t);
    else
      spriteRenderer.transform.localRotation = Quaternion.Lerp(noRotation, clockwise90Rotation, t);

    if (rotationFramesLeft == 0)
      hasRbRotated = false;
  }

  private void RotateSnail90()
  {
    Quaternion rotationQuaternion = Quaternion.AngleAxis(-90, transform.forward);
    Vector2 rotatedDownVector = rotationQuaternion * rotation.GetDownVector();
    Vector2 rotatedFrontVector = rotationQuaternion * rotation.GetFrontVector();
    rotation.Rotate90();
    movement.raycaster.InvalidateBounds();

    Vector2 rotatedDownMoveAmount = movement.TryToMove(rotatedDownVector * groundCheckDistance);
    Vector2 rotateForwardMoveAmount = movement.TryToMove(rotatedFrontVector * forwardMoveAfterRotation);

    if (rotationFrames > 0)
    {
      hasRbRotated = true;
      float t = 1 - (float)rotationFramesLeft / rotationFrames;
      spriteRenderer.transform.localRotation = Quaternion.Lerp(counterClockwise90Rotation, noRotation, t);
    }
  }

  public void Inject(SnailController controller)
  {
    rotation = controller.di.rotation;
    raycaster = controller.di.raycaster;
    animator = controller.di.animator;
  }
}