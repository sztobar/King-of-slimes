using Kite;
using System;
using System.Collections;
using UnityEngine;

public static class RecoilHelpers
{
  public static Vector2 GetRecoilNormalFromTo(Transform recoilTarget, Transform recoilEmitter)
  {
    Vector2 distance = recoilTarget.position - recoilEmitter.position;
    return distance.normalized;
  }

  internal static Vector2 GetRecoilFromTo(Transform recoilTarget, Transform recoilEmitter, float forceAmount)
  {
    Vector2 normal = GetRecoilNormalFromTo(recoilTarget, recoilEmitter);
    Vector2 recoilForce = normal * forceAmount;
    return recoilForce;
  }

}
