using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ScriptableUnit")]
public class ScriptableUnit : ScriptableObject {

  [Header("Movement")]
  [SerializeField] private float tileVelocity = 8f;
  [SerializeField]
  private MovementAcceleration groundedMovement = new MovementAcceleration {
    timeToFullSpeed = 0.2f,
    timeToStop = 0.1f,
    instantDirectionChange = true
  };
  [SerializeField]
  private MovementAcceleration aerialMovement = new MovementAcceleration {
    timeToFullSpeed = 0.5f,
    timeToStop = 0.5f,
    instantDirectionChange = false
  };

  [Header("Jump")]
  [SerializeField] private float jumpTileMaxHeight = 3f;
  [SerializeField] private float jumpTileMinHeight = 1f;

  [SerializeField] private bool canDoubleJump = false;
  [SerializeField] private float doubleJumpTileMaxHeight = 3f;

  [Header("Wall Slide")]
  [SerializeField] private bool canWallSlide = false;

  public float TileVelocity => tileVelocity;
  public MovementAcceleration GroundedMovement => groundedMovement;
  public MovementAcceleration AerialMovement => aerialMovement;
  public float JumpTileMaxHeight => jumpTileMaxHeight;
  public float JumpTileMinHeight => jumpTileMinHeight;
  public bool CanDoubleJump => canDoubleJump;
  public float DoubleJumpTileMaxHeight => doubleJumpTileMaxHeight;
  public bool CanWallSlide => canWallSlide;
}
