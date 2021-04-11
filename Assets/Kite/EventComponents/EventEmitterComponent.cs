using System.Collections.Generic;
using UnityEngine;

namespace Kite
{
  public class EventEmitterComponent<TListener, TValue> : MonoBehaviour
    where TListener : EventListenerComponent<TValue>
  {
    public List<TListener> listeners = new List<TListener>();

    public virtual void Emit(TValue value)
    {
      foreach (var listener in listeners)
      {
        listener.OnEmit(value);
      }
    }

#if UNITY_EDITOR
    protected void OnDrawGizmosSelected()
    {
      if (listeners == null)
        return;

      foreach (var listener in listeners)
      {
        if (!listener)
          return;

        GizmosHelpers.Arrow(transform.position, listener.transform.position - transform.position, 4);
        Collider2D collider = listener.GetComponentInChildren<Collider2D>();
        if (collider)
          GizmosHelpers.Collider(collider);
      }
    }
#endif
  }
}