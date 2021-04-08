using System;
using System.Collections;

namespace Kite {

  public interface IAnimatorEventEmitter<TEvent> where TEvent : Enum {

    Action<TEvent> OnAnimationEvent { get; set; }

    void EmitAnimationEvent(TEvent evt);

    IEnumerator WaitForAnimationEvent(TEvent evt);
  }
}