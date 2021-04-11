using UnityEngine;
using System.Collections.Generic;

namespace Kite
{
  public class CollisionMoveHitsFactoryBehaviour : MonoBehaviour, ICollisionMoveHitsFactory
  {
    public Collider2D skipCollider;
    public bool isPlatform;

    private readonly Dictionary<Transform, RaycastHit2D> previousHitsDictionary = new Dictionary<Transform, RaycastHit2D>();
    private readonly List<RaycastHit2D> uniqueHits = new List<RaycastHit2D>();

    public bool IsPlatform
    {
      get => isPlatform;
      set => isPlatform = value;
    }

    public List<RaycastHit2D> GetUnique(RaycastHit2D[] hits, int count)
    {
      ClearPreviousHits();
      uniqueHits.Clear();
      for (int i = 0; i < count; i++)
      {
        RaycastHit2D hit = hits[i];
        if (!hit || !IsCorrectHit(hit))
        {
          continue;
        }
        if (hit.transform.CompareTag("Tilemap"))
          uniqueHits.Add(hit);
        else
          ShouldAddOrUpdatePreviousHitsDictionary(hit);
      }
      uniqueHits.AddRange(previousHitsDictionary.Values);
      return uniqueHits;
    }

    public IEnumerable<CollisionMoveHit> GetUniqueRaycasterHits(RaycastHit2D[] hits, int count, Direction4 direction)
    {
      List<RaycastHit2D> uniqeHits = new List<RaycastHit2D>(GetUnique(hits, count));
      foreach (RaycastHit2D hit in uniqeHits)
      {
        yield return new CollisionMoveHit(hit, direction);
      }
    }

    public void ClearPreviousHits()
    {
      previousHitsDictionary.Clear();
    }

    private void ShouldAddOrUpdatePreviousHitsDictionary(RaycastHit2D hit)
    {
      Transform transformKey = hit.transform;
      if (!previousHitsDictionary.ContainsKey(transformKey))
      {
        previousHitsDictionary.Add(transformKey, hit);
      }
      else if (previousHitsDictionary[transformKey].distance > hit.distance)
      {
        previousHitsDictionary[transformKey] = hit;
      }
    }

    private bool IsCorrectHit(RaycastHit2D hit)
    {
      if (isPlatform)
      {
        float hitPointY = hit.point.y;
        float colliderMinY = hit.collider.bounds.min.y;
        //bool isObjectOnPlatform = Mathf.Approximately(hitPointY, colliderMinY);
        bool isObjectOnPlatform = Mathf.Abs(hitPointY - colliderMinY) <= 2 * Constants.SKIN_WIDTH;
        bool isPlatformCollisionNormal = hit.normal == Vector2.down;
        return isObjectOnPlatform && isPlatformCollisionNormal;
      }
      return hit.collider != skipCollider;
    }
  }
}