using UnityEngine;

namespace Kite {
  public abstract class PositionComponent : MonoBehaviour, IPosition {
    public abstract Vector2 Value { get; set; }
    public abstract Vector2 GetDelta(Vector2 destination);
    public abstract void Move(Vector2 deltaPosition);
  }
}