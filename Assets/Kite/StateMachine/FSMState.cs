using UnityEditor;
using UnityEngine;

namespace Kite
{
  public abstract class FSMState : MonoBehaviour
  {
    public virtual void StateStart() { }
    public virtual void StateUpdate() { }
    public virtual void StateFixedUpdate() { }
    public virtual void StateExit() { }
  }
}