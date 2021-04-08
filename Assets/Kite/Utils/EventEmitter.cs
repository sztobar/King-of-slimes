using System;
using System.Collections.Generic;

namespace Kite {

  class EventEmitter<T> {

    private readonly List<Action<T>> listeners = new List<Action<T>>();
    private T currentValue;

    public EventEmitter(T initialValue) {
      currentValue = initialValue;
    }

    public void Subscribe(Action<T> listener) {
      listeners.Add(listener);
      listener(currentValue);
    }

    public void Unsubscribe(Action<T> listener) {
      listeners.Remove(listener);
    }

    public void Emit(T newValue) {
      currentValue = newValue;
      for (int i = 0; i < listeners.Count; i++) {
        listeners[i](newValue);
      }
    }
  }
}
