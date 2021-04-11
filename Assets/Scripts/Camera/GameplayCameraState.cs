using Kite;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct GameplayCameraState
{
  public CameraSegment segment;
  public Transform target;

  public override bool Equals(object obj) =>
    obj is GameplayCameraState state
    && segment == state.segment
    && target == state.target;

  public static bool operator ==(GameplayCameraState a, GameplayCameraState b)
  {
    if (((object)a) == null || ((object)b) == null)
      return Equals(a, b);

    return a.Equals(b);
  }

  public static bool operator !=(GameplayCameraState a, GameplayCameraState b)
  {
    if (((object)a) == null || ((object)b) == null)
      return !Equals(a, b);

    return !a.Equals(b);
  }

  public override int GetHashCode()
  {
    int hashCode = -2042979042;
    hashCode = hashCode * -1521134295 + segment.GetHashCode();
    hashCode = hashCode * -1521134295 + target.GetHashCode();
    return hashCode;
  }
}