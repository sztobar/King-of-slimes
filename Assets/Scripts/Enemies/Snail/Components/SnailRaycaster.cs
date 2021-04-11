using System;
using System.Collections;
using Kite;
using UnityEngine;

public class SnailRaycaster : MonoBehaviour, ISnailComponent
{
  public Transform rotateRayOrigin;
  public Transform goBackRayOrigin;
  public LayerMask frontRayLayerMask;
  public LayerMask bottomRayLayerMask;
  public float frontRayLength;
  public float bottomRayLength;
  public bool debug;

  private BoxCollider2D boxCollider;
  private SnailRotation rotation;
  private SnailPhysics physics;

  public void Inject(SnailController controller)
  {
    rotation = controller.di.rotation;
    physics = controller.di.physics;
    boxCollider = controller.di.boxCollider;
  }

  internal bool HasWallInFront()
  {
    Vector2 extents = boxCollider.bounds.extents;
    Vector2 origin = boxCollider.bounds.center;

    Vector2 rayOrigin = origin;
    float skingWidth = Constants.SKIN_WIDTH;
    int axis = rotation.IsHorizontal() ? 0 : 1;
    float sign = transform.right[axis];
    rayOrigin += Vector2Helpers.AxisVector(axis, sign * (extents[axis] - skingWidth));
    Vector2 rayVector = rotation.GetFrontVector();
    RaycastHit2D hit = CastRay(rayOrigin, rayVector, frontRayLength + skingWidth, frontRayLayerMask);
    return hit == true;
  }

  internal RaycastHit2D GetBelowHit(Vector2 deltaMove = default) => GetBelowHit(bottomRayLength, deltaMove);
  internal RaycastHit2D GetBelowHit(float rayLength, Vector2 deltaMove = default)
  {
    Transform rayOriginTransform = physics.goAround ? rotateRayOrigin : goBackRayOrigin;
    Vector2 rayOrigin = (Vector2)rayOriginTransform.transform.position + deltaMove;
    Vector2 rayVector = rotation.GetDownVector();
    RaycastHit2D hit = CastRay(rayOrigin, rayVector, rayLength, bottomRayLayerMask);
    return hit;
  }

  internal bool HasNoColliderBelow(Vector2 deltaMove = default) => GetBelowHit(deltaMove) == false;

  RaycastHit2D CastRay(Vector2 rayOrigin, Vector2 rayVector, float rayLength, int layerMask)
  {
    if (debug)
    {
      Debug.DrawLine(rayOrigin, rayOrigin + rayVector * rayLength, Color.blue);
    }
    return Physics2D.Raycast(rayOrigin, rayVector, rayLength, layerMask);
  }
}