using Kite;
using System.Collections;
using UnityEngine;

public enum PlayerWalkState 
{
  None,
  Left,
  Right
}

public static class PlayerWalkStateHelpers
{
  public static PlayerWalkState FromFloat(float value)
  {
    if (value > 0)
      return PlayerWalkState.Right;
    if (value < 0)
      return PlayerWalkState.Left;

    return PlayerWalkState.None;
  }

  public static bool HasSameSign(this PlayerWalkState state, float value) => FromFloat(value) == state;
}