using UnityEngine;
using System.Collections;

namespace Kite {
  public interface IPosition {
    Vector2 Value { get; set; }
    Vector2 GetDelta(Vector2 destination);
    void Move(Vector2 deltaPosition);
  }
}