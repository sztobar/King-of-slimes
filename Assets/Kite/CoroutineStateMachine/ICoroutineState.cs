using System.Collections;

namespace Kite {

  public interface ICoroutineState {

    IEnumerator StartState();

    void UpdateState();

    void ExitState();
  }
}