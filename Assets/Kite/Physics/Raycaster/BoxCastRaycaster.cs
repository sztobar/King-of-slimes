using System.Collections;
using UnityEngine;

namespace Kite
{
  public class BoxCastRaycaster : IRaycaster
  {
    private const int boxCastAngle = 0;
    private static readonly RaycastHit2D[] results = new RaycastHit2D[16];

    private readonly BoxCollider2D collider;
    private readonly LayerMask layermask;

    public BoxCastRaycaster(BoxCollider2D collider, LayerMask layermask)
    {
      this.collider = collider;
      this.layermask = layermask;
    }

    public (RaycastHit2D[], int) GetHits(float distance, Direction4 direction, Vector2 deltaPosition = default)
    {
      int count = Physics2D.BoxCastNonAlloc((Vector2)collider.bounds.center + deltaPosition, collider.bounds.size, boxCastAngle, direction.ToVector2(), results, distance, layermask);
      return (results, count);
    }

    public void InvalidateBounds()
    {
    }
  }
}