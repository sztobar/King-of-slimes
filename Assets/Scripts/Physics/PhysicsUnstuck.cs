using Kite;
using System;
using System.Collections;
using UnityEngine;

public class PhysicsUnstuck : MonoBehaviour
{
  private static readonly RaycastHit2D[] hits = new RaycastHit2D[16];

  [SerializeField] private new BoxCollider2D collider;
  [SerializeField] private new Rigidbody2D rigidbody;
  [SerializeField] private LayerMask layerMask;

  private bool stuckHorizontally;
  private bool stuckVertically;

  public bool IsStuck() =>
    stuckHorizontally || stuckVertically;

  public void FixPositionIfStuck()
  {
    Physics2D.SyncTransforms();
    Bounds bounds = collider.bounds;
    if (bounds.extents == Vector3.zero)
      Debug.LogWarning("Calling PhysicsUnstuck with empty collider. Gameobject is probably inactive");

    UnstuckResolver resolver = new UnstuckResolver(transform, bounds, layerMask);
    resolver.Resolve();
    stuckHorizontally = resolver.stuckHorizontally;
    stuckVertically = resolver.stuckVertically;
    rigidbody.position += resolver.PositionFix;
  }

  class UnstuckResolver
  {

    readonly Transform transform;
    readonly Bounds bounds;
    readonly Vector2 quadCastSize;
    readonly int layerMask;

    Vector2 verticalPositionFix;
    Vector2 horizontalPositionFix;
    public bool stuckUp, stuckDown, stuckLeft, stuckRight;
    public bool stuckVertically, stuckHorizontally;

    public Vector2 PositionFix => verticalPositionFix + horizontalPositionFix;

    public UnstuckResolver(Transform transform, Bounds bounds, int layerMask)
    {
      this.transform = transform;
      this.bounds = bounds;
      this.layerMask = layerMask;
      quadCastSize = bounds.size * Vector2.one * 0.5f;
      quadCastSize -= 2 * Vector2.one * Physics2D.defaultContactOffset;
    }

    public void Resolve()
    {
      float allowedDown = ResolveDown();
      ResolveUp(allowedDown);
      float allowedLeft = ResolveLeft();
      ResolveRight(allowedLeft);
    }

    private float ResolveDown()
    {
      Vector2 castDirection = Vector2.down;
      float castDistance = bounds.extents.y;
      Vector2 leftPosition = (Vector2)bounds.center + new Vector2(-bounds.extents.x / 2, bounds.extents.y / 2);
      Vector2 rightPosition = (Vector2)bounds.center + new Vector2(bounds.extents.x / 2, bounds.extents.y / 2);

      (bool stuck, float hitDistance) = CastQuadsPair(leftPosition, rightPosition, castDirection, castDistance);
      if (!stuck)
        return hitDistance;

      stuckDown = true;
      verticalPositionFix = new Vector2(0, castDistance - hitDistance);
      return 0;
    }

    private void ResolveUp(float allowedDown)
    {
      Vector2 castDirection = Vector2.up;
      float castDistance = bounds.extents.y;
      Vector2 leftPosition = (Vector2)bounds.center + new Vector2(-bounds.extents.x / 2, -bounds.extents.y / 2);
      Vector2 rightPosition = (Vector2)bounds.center + new Vector2(bounds.extents.x / 2, -bounds.extents.y / 2);

      (bool stuck, float hitDistance) = CastQuadsPair(leftPosition, rightPosition, castDirection, castDistance);
      if (!stuck)
        return;

      stuckUp = true;

      if (stuckDown)
        stuckVertically = true;
      else
      {
        float fixUpMove = castDistance - hitDistance;
        float allowedFixUpMove = Mathf.Min(fixUpMove, allowedDown);
        verticalPositionFix = new Vector2(0, -allowedFixUpMove);
      }
    }

    private float ResolveLeft()
    {
      Vector2 castDirection = Vector2.left;
      float castDistance = bounds.extents.x;
      Vector2 upPosition = (Vector2)bounds.center + new Vector2(bounds.extents.x / 2, bounds.extents.y / 2) + verticalPositionFix;
      Vector2 downPosition = (Vector2)bounds.center + new Vector2(bounds.extents.x / 2, -bounds.extents.y / 2) + verticalPositionFix;

      (bool stuck, float hitDistance) = CastQuadsPair(upPosition, downPosition, castDirection, castDistance);
      if (!stuck)
        return hitDistance;

      stuckLeft = true;
      horizontalPositionFix = new Vector2(castDistance - hitDistance, 0);
      return 0;
    }

    private void ResolveRight(float allowedLeft)
    {
      Vector2 castDirection = Vector2.right;
      float castDistance = bounds.extents.x;
      Vector2 upPosition = (Vector2)bounds.center + new Vector2(-bounds.extents.x / 2, bounds.extents.y / 2) + verticalPositionFix;
      Vector2 downPosition = (Vector2)bounds.center + new Vector2(-bounds.extents.x / 2, -bounds.extents.y / 2) + verticalPositionFix;

      (bool stuck, float hitDistance) = CastQuadsPair(upPosition, downPosition, castDirection, castDistance);
      if (!stuck)
        return;

      stuckLeft = true;

      if (stuckRight)
      {
        stuckHorizontally = true;
        horizontalPositionFix = Vector2.zero;
      }
      else
      {
        float fixRightMove = castDistance - hitDistance;
        float allowedFixRightMove = Mathf.Min(fixRightMove, allowedLeft);
        horizontalPositionFix = new Vector2(-allowedFixRightMove, 0);
      }
    }

    private (bool, float) CastQuadsPair(Vector2 position1, Vector2 postion2, Vector2 castDirection, float castDistance)
    {
      (bool stuck1, float distance1) = CastQuad(position1, castDirection, castDistance);
      (bool stuck2, float distance2) = CastQuad(postion2, castDirection, castDistance);

      if (!stuck1 && !stuck2)
        return (false, 0);

      float hitDistance = castDistance;
      if (stuck1 && stuck2)
        hitDistance = Mathf.Min(distance1, distance2);
      else if (stuck1)
        hitDistance = distance1;
      else if (stuck2)
        hitDistance = distance2;
      return (true, hitDistance);
    }

    private (bool, float) CastQuad(Vector2 position, Vector2 direction, float distance)
    {
      float castAngle = 0;
      bool stuck = false;
      int count = Physics2D.BoxCastNonAlloc(position, quadCastSize, castAngle, direction, hits, distance, layerMask);
      float minDistance = distance;
      for (int i = 0; i < count; i++)
      {
        RaycastHit2D hit = hits[i];
        if (!hit || hit.transform == transform)
          continue;

        float hitDistance = hit.distance;
        if (hitDistance > 0)
        {
          stuck = true;
          if (minDistance > hitDistance)
            minDistance = hitDistance;
        }
      }
      return (stuck, minDistance);
    }
  }
}
