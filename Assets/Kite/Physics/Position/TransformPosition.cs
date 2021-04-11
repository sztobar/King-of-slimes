using UnityEngine;
using System.Collections;

namespace Kite {
  public class TransformPosition : PositionComponent {

    public new Transform transform;

    public override Vector2 Value {
      get => transform.position;
      set => transform.position = value;
    }

    public override Vector2 GetDelta(Vector2 destination) {
      return Value - destination;
    }

    public override void Move(Vector2 deltaPosition) {
      Value += deltaPosition;
    }
  }
}