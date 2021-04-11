using System.Collections;
using UnityEngine;

public readonly struct RandomRange {

  private readonly float min, max;

  public RandomRange(float min, float max) {
    this.min = min;
    this.max = max;
  }

  public float GetValue() =>
    Random.Range(min, max);

  public static implicit operator RandomRange(Vector2 v) =>
    new RandomRange(v.x, v.y);

  public static float FromVector(Vector2 v) =>
    Random.Range(v.x, v.y);
}
