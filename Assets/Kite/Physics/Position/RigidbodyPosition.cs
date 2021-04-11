using UnityEngine;
using System.Collections;

namespace Kite {
  public class RigidbodyPosition : PositionComponent {

    [SerializeField] private new Rigidbody2D rigidbody;

    public override Vector2 Value {
      get => rigidbody.position;
      set => rigidbody.position = value;
    }

    public void Init(Rigidbody2D rigidbody) {
      this.rigidbody = rigidbody;
    }

    public override Vector2 GetDelta(Vector2 destination) {
      return rigidbody.position - destination;
    }

    public override void Move(Vector2 deltaPosition) {
      rigidbody.position += deltaPosition;
    }
  }
}