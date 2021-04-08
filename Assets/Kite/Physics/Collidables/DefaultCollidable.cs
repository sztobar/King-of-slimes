using UnityEngine;

namespace Kite {
  public class DefaultCollidable : ICollidable {

    private static readonly DefaultCollidable _instance = new DefaultCollidable();

    public static DefaultCollidable Get() =>
      _instance;

    public void OnPhysicsMoveInto(Transform moving, float collideDistance, Direction4 direction, Vector2 hitPoint) { }

    public float GetAllowedMoveInto(Transform wantsToMove, float collideDistance, Direction4 direction, Vector2 hitPoint) =>
      0;
  }
}