using System.Collections;
using UnityEngine;

namespace Kite
{
  public abstract class EffectableBehaviour : MonoBehaviour
  {
    public abstract void Push(float distance, Dir4 direction);
  }
}