using UnityEngine;
using System.Collections;
using System;

namespace Kite {
  public static class CollidableHelpers {

    public static ICollidable GetCollidable(Transform transform) {
      ICollidable collidable = transform.GetComponent<ICollidable>();
      return collidable ?? DefaultCollidable.Get();
    }
  }
}