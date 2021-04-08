using UnityEngine;

namespace Kite {
  public class SineY : IEvaluator<Vector2> {

    private static readonly float TWO_PI = 2 * Mathf.PI;
    private readonly float periods;
    private readonly float amplidue;

    public SineY(float periods, float amplidue) {
      this.periods = periods;
      this.amplidue = amplidue;
    }

    public Vector2 Evaluate(float percentage) {
      float theta = percentage * periods * TWO_PI;
      float sineY = amplidue * Mathf.Sin(theta);
      return Vector2.up * sineY;
    }
  }
}
