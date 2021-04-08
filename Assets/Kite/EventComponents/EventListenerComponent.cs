using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kite
{
  public abstract class EventListenerComponent<T> : MonoBehaviour
  {
    public delegate void Listener(T value);
    protected List<Listener> listeners = new List<Listener>();
    
    public void Subscribe(Listener listener)
    {
      if (!listeners.Contains(listener)) {
        listeners.Add(listener);
      }
    }

    public void OnEmit(T value)
    {
      foreach(var listener in listeners)
      {
        listener(value);
      }
    }


#if UNITY_EDITOR
    //protected void DrawGizmos(IEnumerable<EventEmitterComponent<T>> emitters)
    //{
    //  if (emitters == null)
    //    return;

    //  foreach (var emitter in emitters)
    //  {
    //    if (!emitter)
    //      continue;

    //    GizmosHelpers.Arrow(transform.position, emitter.transform.position - transform.position, 4);
    //    Collider2D collider = emitter.GetComponent<Collider2D>();
    //    if (collider)
    //      GizmosHelpers.Collider(collider);
    //  }
    //}
#endif
  }
}