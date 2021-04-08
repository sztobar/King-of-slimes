using UnityEngine;

namespace Kite
{
  public class PhysicsGravity : MonoBehaviour
  {
    public float gravityScale = 5;
    public float maxFallTileVelocity = 20;
    public PhysicsVelocity velocity;

    public float G => Physics2D.gravity.y * gravityScale * TileHelpers.tileSize;
    public float JumpVelocity(float jumpVelocity) => Mathf.Sqrt(2 * Mathf.Abs(G) * jumpVelocity);

    public float Scale
    {
      get => gravityScale;
      set => gravityScale = value;
    }

    public void ApplyGravity()
    {
      float velocityY = velocity.Y;
      float maxFallVelocity = -maxFallTileVelocity * TileHelpers.tileSize;
      if (velocityY > maxFallVelocity)
      {
        float dt = Time.deltaTime;
        float newVelocityY = velocityY + G * dt;
        velocity.Y = Mathf.Max(newVelocityY, maxFallVelocity);
      }
    }
  }
}