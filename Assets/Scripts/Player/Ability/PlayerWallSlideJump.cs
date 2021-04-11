using Kite;
using UnityEngine;

public class PlayerWallSlideJump : MonoBehaviour, PlayerWallSlideAbility.IInjectable
{
  [Tooltip("Jump normal when player is clicking arrow in wall direction")]
  public Vector2 wallClimbNormalVelocity = new Vector2(0.25f, 1);

  [Tooltip("Jump normal when player is not clicking any arrow keys")]
  public Vector2 jumpOffNormalVelocity = new Vector2(0.5f, 1);

  [Tooltip("Jump normal when player is clicking arrow opposite to wall direction")]
  public Vector2 wallLeapNormalVelocity = new Vector2(1, 1);

  [Tooltip("After jump horizontal movement wont work for time")]
  public float blockMovementTime = 0.5f;

  public bool debug;

  private WallSlideRaycaster wallSlideRaycaster;

  private PlayerJumpAbility jump;
  private PlayerMovementAbility movement;
  private PlayerPhysics physics;

  private Vector2 JumpWorldVelocity(Vector2 normal) =>
    new Vector2(normal.x * movement.WorldVelocity, normal.y * jump.MaxJumpVelocity);

  public void PerformJump(Direction2H wallSlideDirection, float moveInput)
  {
    if (debug)
      Debug.Log("[PlayerWallSlideJump] PerformJump");

    Vector2 jumpNormalVelocity = GetJumpNormalVelocity(wallSlideDirection, moveInput);
    Vector2 jumpVelocity = JumpWorldVelocity(jumpNormalVelocity);

    AudioSingleton.PlaySound(AudioSingleton.Instance.clips.jump);
    physics.velocity.Value = jumpVelocity;
  }

  public Vector2 GetJumpNormalVelocity(Direction2H wallSlideDirection, float moveInput)
  {
    Vector2 jumpNormalVelocity;
    bool isBetweenWalls = wallSlideRaycaster.IsBetweenWalls();
    Direction2H moveInputDirection = Direction2HHelpers.FromFloat(moveInput);
    if (moveInput == 0)
    {
      jumpNormalVelocity = jumpOffNormalVelocity;
    }
    else if (wallSlideRaycaster.IsTouchingWall(moveInputDirection) || isBetweenWalls)
    {
      jumpNormalVelocity = wallClimbNormalVelocity;
    }
    else
    {
      jumpNormalVelocity = wallLeapNormalVelocity;
    }
    float wallSlideXSign = 0;
    if (!isBetweenWalls)
    {
      wallSlideXSign = wallSlideDirection.ToFloat() * -1;
    }
    Vector2 oppositeToWallVector = new Vector2(wallSlideXSign, 1);
    return jumpNormalVelocity * oppositeToWallVector;
  }
  public void InactiveAbilityUpdate() { }

  public void Inject(PlayerUnitDI di, WallSlideRaycaster raycaster)
  {
    wallSlideRaycaster = raycaster;
    physics = di.physics;
    movement = di.abilities.movement;
    jump = di.abilities.jump;
  }
}
