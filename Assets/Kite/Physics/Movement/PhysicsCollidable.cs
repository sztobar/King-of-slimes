using System.Collections;
using UnityEngine;

namespace Kite
{
  public abstract class PhysicsCollidable : MonoBehaviour
  {
    public abstract float GetAllowedMoveInto(PhysicsMove move);
    public abstract void OnMoveInto(PhysicsMove move);
  }
}