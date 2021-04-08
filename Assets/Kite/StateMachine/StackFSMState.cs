using System.Collections;
using UnityEngine;

namespace Kite
{
  public class StackFSMState : FSMState
  {
    /// <summary>
    /// When another state gets on top of the stack
    /// </summary>
    public virtual void StatePause() { }

    /// <summary>
    /// When a state is again on top of the stack
    /// </summary>
    public virtual void StateResume() { }
  }
}