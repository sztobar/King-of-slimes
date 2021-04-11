using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SlimeMap<T> : IEnumerable<T>
{
  public T king;
  public T heart;
  public T sword;
  public T shield;

  public T this[SlimeType type]
  {
    get => Get(type);
    set => Set(type, value);
  }

  public T Get(SlimeType type)
  {
    switch (type)
    {
      case SlimeType.King:
        return king;
      case SlimeType.Heart:
        return heart;
      case SlimeType.Sword:
        return sword;
      case SlimeType.Shield:
      default:
        return shield;
    }
  }

  public void Set(SlimeType type, T value)
  {
    switch (type)
    {
      case SlimeType.King:
        king = value;
        break;
      case SlimeType.Heart:
        heart = value;
        break;
      case SlimeType.Sword:
        sword = value;
        break;
      case SlimeType.Shield:
        shield = value;
        break;
    }
  }

  public IEnumerable<(SlimeType, T)> GetPairEnumerable()
  {
    foreach (SlimeType type in SlimeTypeHelpers.GetEnumerable())
    {
      yield return (type, Get(type));
    };
  }

  public IEnumerator<T> GetEnumerator()
  {
    foreach (SlimeType type in SlimeTypeHelpers.GetEnumerable())
    {
      yield return Get(type);
    };
  }

  // thanks c#
  // https://docs.microsoft.com/en-gb/dotnet/api/system.collections.generic.ienumerable-1.getenumerator?view=net-5.0
  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}
