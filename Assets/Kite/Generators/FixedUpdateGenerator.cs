using UnityEngine;
using System.Collections;

namespace Kite {
  public class FixedUpdateGenerator : IEnumerable {

    private IUpdateGenerator enumerator;

    public FixedUpdateGenerator(IUpdateGenerator enumerator) {
      this.enumerator = enumerator;
    }

    public IEnumerator GetEnumerator() {
      var fixedUpdate = new WaitForFixedUpdate();

      while (!enumerator.IsDone) {
        enumerator.Tick(Time.fixedDeltaTime);
        do {
          yield return fixedUpdate;
        } while (PauseManager.IsPaused);
      }
    }
  }
}