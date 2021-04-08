using UnityEngine;

namespace Kite
{
  public readonly struct CollisionMoveHit
  {
    public readonly bool isTilemap;
    public readonly float distanceToHit;
    public readonly Vector2 point;
    public readonly Direction4 rayDirection;
    public readonly Vector2 normal;
    public readonly Transform transform;

    public CollisionMoveHit(Transform transform, Vector2 point, Vector2 normal, Direction4 rayDirection, float distanceToHit)
    {
      this.transform = transform;
      this.point = point;
      this.distanceToHit = distanceToHit;
      this.rayDirection = rayDirection;
      this.normal = normal;
      isTilemap = transform.CompareTag("Tilemap");
    }

    public CollisionMoveHit(RaycastHit2D hit, Direction4 rayDirection) :
      this(
        transform: hit.transform,
        point: hit.point,
        normal: hit.normal,
        rayDirection: rayDirection,
        distanceToHit: hit.distance
        //distanceToHit: Mathf.Max(hit.distance - RaycastHelpers.skinWidth, 0)
      )
    { }

    public float GetCollideDistance(float wantedToMove)
    {
      return Mathf.Max(wantedToMove - distanceToHit, 0);
    }
  }
}