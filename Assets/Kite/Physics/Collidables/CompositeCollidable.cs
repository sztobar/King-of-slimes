using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kite {
  public class CompositeCollidable : MonoBehaviour, ICollidable {

    private List<IConditionalCollidable> leafs;

    public void AddLeaf(ICollidable collidable, int priority) {

    }

    public void OnPhysicsMoveInto(Transform moving, float collideDistance, Direction4 direction, Vector2 hitPoint) {
    }

    public float GetAllowedMoveInto(Transform wantsToMove, float collideDistance, Direction4 direction, Vector2 hitPoint) {
      return 0;
    }
  }
}