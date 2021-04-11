using UnityEngine;
using Kite;
using System;

public class PlayerJumpAbility : MonoBehaviour, IPlayerAbility
{
  public float jumpTileMaxHeight = 3f;
  public float jumpTileMinHeight = 1f;
  public float coyoteJumpTime = 0.1f;
  public bool canDoubleJump = false;
  public float doubleJumpTileMaxHeight = 3f;

  private PlayerPhysics physics;
  private PlayerInput input;

  private float coyoteJumpTimeLeft;
  private bool jumpInProgress;
  private bool doubleJumped;

  public float MaxJumpVelocity => physics.gravity.JumpVelocity(jumpTileMaxHeight * TileHelpers.tileSize);
  private float DoubleJumpVelocity => physics.gravity.JumpVelocity(doubleJumpTileMaxHeight * TileHelpers.tileSize);
  private float TerminateJumpVelocity => Mathf.Sqrt(Mathf.Pow(MaxJumpVelocity, 2) - (2 * -physics.gravity.G * TileHelpers.TileToWorld(jumpTileMaxHeight - jumpTileMinHeight)));

  public void Inject(PlayerUnitDI di)
  {
    physics = di.physics;
    input = di.mainDi.controller.input;
    ReadStats(di.stats.Data);
  }

  private void ReadStats(ScriptableUnit data)
  {
    jumpTileMaxHeight = data.JumpTileMaxHeight;
    jumpTileMinHeight = data.JumpTileMinHeight;
    canDoubleJump = data.CanDoubleJump;
    doubleJumpTileMaxHeight = data.DoubleJumpTileMaxHeight;
  }

  public void ControlUpdate()
  {
    bool isGrounded = physics.IsGrounded;
    BeforeUpdate(isGrounded);
    bool jumpDownPerformed = false;

    if (input.jumpDown.IsPressed())
    {
      if (physics.standEffectable.isOnPlatform)
      {
        input.jumpDown.Use();
        input.jump.Use();
        physics.standEffectable.SkipPlatform();
        coyoteJumpTime = 0;
        jumpDownPerformed = true;
        physics.IsGrounded = false;
      }
    }
    if (!jumpDownPerformed && input.jump.IsPressed())
    {
      bool canGroundJump = isGrounded || coyoteJumpTimeLeft > 0;
      if (canGroundJump)
      {
        PerformJump();
      }
      else if (CanDoubleJump())
      {
        PerformDoubleJump();
      }
    }

    if (!isGrounded)
    {
      float termVelocity = TerminateJumpVelocity;
      if (!input.jump.IsHeld() && physics.velocity.Y > termVelocity)
      {
        physics.velocity.Y = termVelocity;
      }
    }
  }

  public void InactiveUpdate()
  {
    bool isGrounded = physics.IsGrounded;
    if (isGrounded)
    {
      jumpInProgress = false;
      doubleJumped = false;
      enabled = false;
    }
    else if (jumpInProgress)
    {
      float termVelocity = TerminateJumpVelocity;
      if (physics.velocity.Y > termVelocity)
      {
        physics.velocity.Y = termVelocity;
      }
    }
  }

  public void WallSlideUpdate() { }

  private void BeforeUpdate(bool isGrounded)
  {
    if (isGrounded)
    {
      jumpInProgress = false;
      doubleJumped = false;
      coyoteJumpTimeLeft = coyoteJumpTime;
    }
    else if (!isGrounded && coyoteJumpTimeLeft > 0)
    {
      coyoteJumpTimeLeft -= Time.deltaTime;
    }
  }

  private void PerformJump()
  {
    // sound.PlayJump();
    // jumpParticles.Play();
    AudioSingleton.PlaySound(AudioSingleton.Instance.clips.jump);
    physics.velocity.Y = MaxJumpVelocity;
    coyoteJumpTimeLeft = 0;
    jumpInProgress = true;
    physics.IsGrounded = false;
    input.jump.Use();
  }

  private bool CanDoubleJump()
  {
    return canDoubleJump && !doubleJumped;
  }

  private void PerformDoubleJump()
  {
    //sound.PlayJump();
    //jumpParticles.Play();
    AudioSingleton.PlaySound(AudioSingleton.Instance.clips.jump);
    physics.velocity.Y = DoubleJumpVelocity;
    jumpInProgress = true;
    doubleJumped = true;
  }

  private void OnEnable()
  {
    coyoteJumpTime = 0;
  }
}
