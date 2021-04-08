using System.Collections;
using UnityEngine;

namespace Kite
{
  public class PhysicsMoveCollidableMock : PhysicsCollidable
  {
    public bool isPlatform;
    public Vector2 allowedMove;

    public override void OnMoveInto(PhysicsMove move) { }

    public override float GetAllowedMoveInto(PhysicsMove move)
    {
      if (allowedMove != Vector2.zero && allowedMove[move.dir.Axis] != 0)
      {
        float allowedDistance = Mathf.Abs(allowedMove[move.dir.Axis]);
        return Mathf.Min(move.collideDistance, allowedDistance);
      }
      if (isPlatform)
      {
        return PlatformCollidableHelpers.GetEdgeColliderAllowedMovementInto(move);
      }
      return 0;
    }
  }
}