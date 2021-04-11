using System;
using System.Collections.Generic;

public enum SlimeType {
  King,
  Heart,
  Sword,
  Shield
}

public static class SlimeTypeHelpers {
  public static bool IsKing(this SlimeType type) =>
    type == SlimeType.King;

  public static IEnumerable<SlimeType> GetEnumerable() {
    yield return SlimeType.King;
    yield return SlimeType.Heart;
    yield return SlimeType.Sword;
    yield return SlimeType.Shield;
  }

  public static IEnumerable<SlimeType> GetWithoutKingEnumerable() {
    yield return SlimeType.Heart;
    yield return SlimeType.Sword;
    yield return SlimeType.Shield;
  }
}