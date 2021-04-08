using System.Collections;
using UnityEngine;

namespace Kite
{
  public readonly struct PhysicsMove
  {
    public readonly RaycastHit2D hit;
    public readonly Dir4 dir;
    public readonly float collideDistance;
    public readonly PhysicsMovement moving;

    public PhysicsMove(RaycastHit2D hit, float collideDistance, Dir4 dir, PhysicsMovement moving) =>
      (this.hit, this.dir, this.collideDistance, this.moving) = (hit, dir, collideDistance, moving);
  }
}