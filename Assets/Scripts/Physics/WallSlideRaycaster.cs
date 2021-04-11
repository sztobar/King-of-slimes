using Kite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideRaycaster : MonoBehaviour {

  [SerializeField] private BoxCollider2D boxCollider;
  [SerializeField] private float rayLength;
  [SerializeField] private LayerMask layerMask;
  [SerializeField] private bool debug;

  private bool wallOnLeft;
  private bool wallOnRight;

  public bool IsTouchingWall(Direction2H direction) =>
    direction == Direction2H.Left ? wallOnLeft : wallOnRight;

  public void CheckWallCollision() {
    Vector2 extents = boxCollider.bounds.extents;
    float x = boxCollider.bounds.center.x;
    float y = transform.position.y;
    Vector2 origin = new Vector2(x, y);
    wallOnLeft = CastForWall(origin - new Vector2(extents.x, 0), Direction2H.Left);
    wallOnRight = CastForWall(origin + new Vector2(extents.x, 0), Direction2H.Right);
  }

  public bool IsBetweenWalls() {
    return wallOnLeft && wallOnRight;
  }

  public bool IsTouchingAnyWall() {
    return wallOnLeft || wallOnRight;
  }

  private bool CastForWall(Vector2 position, Direction2H direction) {
    float skinWidth = Constants.SKIN_WIDTH;
    Vector2 rayVector = direction.ToVector2();
    Vector2 rayOrigin = position - rayVector * skinWidth;
    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayVector, rayLength + skinWidth, layerMask);
    if (debug) {
      Vector2 ray = rayVector * 1;
      Debug.DrawLine(position, position + ray, Color.blue);
    }
    return hit;
  }
}
