using UnityEngine;
using Kite;

public class DoorCrusher : MonoBehaviour
{
  public BoxCollider2D boxCollider;
  public LayerMask layerMask;
  public float rayLength;
  public bool debug;

  public void CrushUpdate()
  {
    Bounds bounds = boxCollider.bounds;
    float skinWidth = Constants.SKIN_WIDTH;
    Vector2 leftRayOrigin = new Vector2(bounds.min.x + skinWidth, bounds.min.y + skinWidth);
    CastRay(leftRayOrigin);
    Vector2 rightRayOrigin = new Vector2(bounds.max.x - skinWidth, bounds.min.y + skinWidth);
    CastRay(rightRayOrigin);
    Vector2 centerRayOrigin = new Vector2(bounds.center.x, bounds.min.y + skinWidth);
    CastRay(centerRayOrigin);
  }

  public void CastRay(Vector2 origin) {
    Vector2 rayVector = Vector2.down;
    float skinWidth = Constants.SKIN_WIDTH;
    float rayLength = this.rayLength + skinWidth;
    if (debug)
    {
      Debug.DrawLine(origin, origin + (rayVector * rayLength), Color.red);
    }
    RaycastHit2D hit = Physics2D.Raycast(origin, rayVector, rayLength, layerMask);
    if (hit)
    {
      PlayerUnitController player = InteractiveHelpers.GetPlayer(hit.collider);
      if (player)
      {
        float collideDistance = 1f;
        float allowedMove = player.di.physics.movement.GetAllowedMovement(collideDistance, Dir4.down);
        if (allowedMove < collideDistance)
        {
          PlayerDamageModule damage = player.di.damage;
          damage.TakeFullDamage();
        }
      }
    }
  }

}