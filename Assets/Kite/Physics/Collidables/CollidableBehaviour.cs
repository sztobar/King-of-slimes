using System.Collections;
using UnityEngine;

namespace Kite
{
  public abstract class CollidableBehaviour : MonoBehaviour, ICollidable
  {
    public abstract float GetAllowedMoveInto(Transform wantsToMove, float collideDistance, Direction4 direction, Vector2 hitPoint);
    public abstract void OnPhysicsMoveInto(Transform moving, float collideDistance, Direction4 direction, Vector2 hitPoint);
  }
}