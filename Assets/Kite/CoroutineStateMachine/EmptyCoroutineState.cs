using UnityEngine;
using System.Collections;

namespace Kite {

  public class EmptyCoroutineState : ICoroutineState {

    public void ExitState() {}

    public IEnumerator StartState() {
      return null;
    }

    public void UpdateState() {}
  }
}
