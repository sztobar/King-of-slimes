using UnityEngine;
using System.Collections;

namespace Kite {
  public interface IConditionalCollidable : ICollidable {
    bool ShouldCallGetAllowedMoveInto(Transform wantsToMove, float collideDistance, Direction4 direction, Vector2 hitPoint);
    bool ShouldCallForceMoveInto(Transform wantsToMove, float collideDistance, Direction4 direction);
  }
}