using UnityEngine;

namespace Kite {
  public class FromToCurve : IEvaluator<Vector2> {

    private readonly Vector2 from;
    private readonly Vector2 to;
    private readonly AnimationCurve curve;

    public FromToCurve(Vector2 from, Vector2 to, AnimationCurve curve) {
      this.from = from;
      this.to = to;
      this.curve = curve;
    }

    public Vector2 Evaluate(float percentage) {
      float t = curve.Evaluate(percentage);
      return Vector2.Lerp(from, to, t);
    }
  }
}