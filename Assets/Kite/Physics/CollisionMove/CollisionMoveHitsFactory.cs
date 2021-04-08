using UnityEngine;
using System.Collections.Generic;
using System;

namespace Kite
{
  public class CollisionMoveHitsFactory : ScriptableObject, ISerializationCallbackReceiver
  {
    public Collider2D skipCollider;
    public bool isPlatform;

    public Dictionary<Transform, RaycastHit2D> hitsDict;

    public bool IsPlatform
    {
      get => isPlatform;
      set => isPlatform = value;
    }

    public static CollisionMoveHitsFactory Create(Collider2D skipCollider, bool isPlatform = false)
    {
      CollisionMoveHitsFactory instance = CreateInstance<CollisionMoveHitsFactory>();
      instance.skipCollider = skipCollider;
      instance.isPlatform = isPlatform;
      instance.InitDictionary();
      return instance;
    }

    public List<RaycastHit2D> GetUnique(RaycastHit2D[] hits, int count)
    {
      ClearPreviousData();
      List<RaycastHit2D> uniqueHits = new List<RaycastHit2D>();
      for (int i = 0; i < count; i++)
      {
        RaycastHit2D hit = hits[i];
        if (!hit || !IsCorrectHit(hit))
        {
          continue;
        }
        if (TilemapHelpers.IsColliderTilemap(hit.collider))
          uniqueHits.Add(hit);
        else
          AddOrUpdateHitsDictionary(hit);
      }
      uniqueHits.AddRange(hitsDict.Values);
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

    public void ClearPreviousData()
    {
      hitsDict.Clear();
    }

    public void InitDictionary()
    {
      hitsDict = new Dictionary<Transform, RaycastHit2D>();
    }

    private void AddOrUpdateHitsDictionary(RaycastHit2D hit)
    {
      Transform transformKey = hit.transform;
      if (!hitsDict.ContainsKey(transformKey))
      {
        hitsDict.Add(transformKey, hit);
      }
      else if (hitsDict[transformKey].distance > hit.distance)
      {
        hitsDict[transformKey] = hit;
      }
    }

    private bool IsCorrectHit(RaycastHit2D hit)
    {
      if (isPlatform)
      {
        float hitPointY = hit.point.y;
        float colliderMinY = hit.collider.bounds.min.y;
        //bool isObjectOnPlatform = Mathf.Approximately(hitPointY, colliderMinY);
        bool isObjectOnPlatform = Mathf.Abs(hitPointY - colliderMinY) <= 2 * RaycastHelpers.skinWidth;
        bool isPlatformCollisionNormal = hit.normal == Vector2.down;
        return isObjectOnPlatform && isPlatformCollisionNormal;
      }
      return hit.collider != skipCollider;
    }

    public void OnBeforeSerialize() {}

    public void OnAfterDeserialize()
    {
      InitDictionary();
    }
  }
}