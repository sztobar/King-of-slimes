using System.Collections.Generic;
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
  }
}