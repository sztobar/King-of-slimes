using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

namespace Kite
{
  public class CornerCorrectionMovement : MonoBehaviour
  {
    public PhysicsMovement movement;
    public float jumpCornerCorrection = 5;

    public float moveCornerCorrectionUp = 8;
    public float moveCornerCorrectionDown = 8;

    public bool debugLog;

    public Vector2 GetCornerMoveCorrection(Vector2 wantsToMoveAmount)
    {
      if (wantsToMoveAmount.y > 0)
      {
        if (jumpCornerCorrection > 0)
        {
          float cornerCorrectionMoveResult = GetJumpCornerCorrection(wantsToMoveAmount.y);
          if (Mathf.Abs(cornerCorrectionMoveResult) > RaycastHelpers.skinWidth)
          {
            if (debugLog)
              Debug.Log($"[CornerCorrectionMovement]: Jump Correction: {cornerCorrectionMoveResult}");

            return new Vector2(cornerCorrectionMoveResult, 0);
          }
        }
      }
      else if (wantsToMoveAmount.x != 0)
      {
        if (moveCornerCorrectionDown > 0)
        {
          float correctionMoveResult = GetMoveCornerCorrection(wantsToMoveAmount.x, moveCornerCorrectionDown, DirY.down);
          if (Mathf.Abs(correctionMoveResult) > RaycastHelpers.skinWidth)
          {
            if (debugLog)
              Debug.Log($"[CornerCorrectionMovement]: Move Correction down: {correctionMoveResult}");

            return new Vector2(0, correctionMoveResult);
          }
        }
        if (moveCornerCorrectionUp > 0)
        {
          float correctionMoveResult = GetMoveCornerCorrection(wantsToMoveAmount.x, moveCornerCorrectionUp, DirY.up);
          if (Mathf.Abs(correctionMoveResult) > RaycastHelpers.skinWidth)
          {
            if (debugLog)
              Debug.Log($"[CornerCorrectionMovement]: Move Correction up: {correctionMoveResult}");

            return new Vector2(0, correctionMoveResult);
          }
        }
      }
      return Vector2.zero;
    }

    private float GetJumpCornerCorrection(float topMovementAmount)
    {
      (bool leftCorrected, float leftCorrectionValue) = GetJumpCornerCorrectionFor(topMovementAmount, DirX.left);
      if (leftCorrected)
        return leftCorrectionValue;

      (bool rightCorrected, float rightCorrectionValue) = GetJumpCornerCorrectionFor(topMovementAmount, DirX.right);
      if (rightCorrected)
        return rightCorrectionValue;

      return 0;
    }

    private (bool, float) GetJumpCornerCorrectionFor(float topMovementAmount, DirX rayDirX)
    {
      Dir4 rayDir = Dir4.FromXFloat(rayDirX);
      Bounds bounds = movement.boxCollider.bounds;
      float rayLength = jumpCornerCorrection;
      Vector2 upperCorner = BoundsHelpers.GetCorner(bounds, rayDirX, DirY.up);
      Vector2 rayDelta = new Vector2(-rayDirX * rayLength, topMovementAmount);
      Vector2 rayOrigin = upperCorner + rayDelta;
      RaycastHit2D cornerHit = RaycastHelpers.SingleHit(rayOrigin, rayLength, rayDir, movement.layerMask);
      if (CanBeCornerCorrected(rayLength, rayDir, cornerHit))
      {
        float correctionValue = -rayDirX * (rayLength - cornerHit.distance);
        return (true, correctionValue);
      }
      return (false, 0);
    }

    private bool CanBeCornerCorrected(float rayLength, Dir4 rayDir, RaycastHit2D cornerHit) =>
      IsCorrectDistance(cornerHit.distance, jumpCornerCorrection) &&
      cornerHit.collider &&
      !IsInsideTile(cornerHit, rayDir) &&
      !CanMoveInto(cornerHit, rayLength, rayDir);

    // TODO: Fix based on GetJumpCornerCorrection
    private float GetMoveCornerCorrection(float xMoveAmount, float correctionAmount, DirY dir)
    {
      Bounds testBounds = movement.boxCollider.bounds;
      float xSign = Mathf.Sign(xMoveAmount);
      float ySign = dir.value;
      Vector2 rayVector = dir;
      float rayLength = correctionAmount + RaycastHelpers.skinWidth;
      {
        Vector2 rayOrigin = new Vector2(
          testBounds.center.x + xSign * testBounds.extents.x + xMoveAmount,
          testBounds.center.y + ySign * (testBounds.extents.y - rayLength)
        );
        RaycastHit2D rayHit = Physics2D.Raycast(rayOrigin, rayVector, rayLength, movement.layerMask);
        Debug.DrawRay(rayOrigin + Vector2.right * 5 * xSign, rayVector * rayLength);
        if (IsCorrectDistance(rayHit.distance, correctionAmount) && !CanMoveInto(rayHit, rayLength, Dir4.FromYFloat(dir)))
          return -ySign * (rayLength - rayHit.distance);
      }
      return 0;
    }

    private bool IsCorrectDistance(float distance, float max) =>
      distance > 0 && distance < max;

    private bool IsInsideTile(RaycastHit2D hit, Dir4 rayDir)
    {
      Tilemap tilemap = hit.collider.GetComponent<Tilemap>();
      if (tilemap)
      {
        Vector2 pointBeforeHit = hit.point + ((Vector2)(-rayDir) * RaycastHelpers.skinWidth);
        Vector2Int tileSize = Vector2Int.RoundToInt(tilemap.cellSize);
        int tileX = Mathf.FloorToInt(pointBeforeHit.x / tileSize.x);
        int tileY = Mathf.FloorToInt(pointBeforeHit.y / tileSize.y);
        Vector3Int tilePosition = new Vector3Int(tileX, tileY, 0);
        TileBase tile = tilemap.GetTile(tilePosition);
        return tile;
      }
      return false;
    }

    private bool CanMoveInto(RaycastHit2D hit, float distance, Dir4 dir)
    {
      PhysicsCollidable collidable = hit.collider.GetComponent<PhysicsCollidable>();
      if (collidable)
      {
        float collideDistance = distance - hit.distance;
        float allowedMove = collidable.GetAllowedMoveInto(new PhysicsMove(hit, collideDistance, dir, movement));
        return allowedMove == collideDistance;
      }
      else
      {
        float collideDistance = distance - hit.distance;
        float allowedMove = 0;
        return allowedMove == collideDistance;
      }
    }

  }
}