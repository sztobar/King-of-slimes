using System.Collections;

namespace Kite {

  /// <summary>
  /// Base class for Coroutine State with collection of StateActions.
  /// UpdateState calls all attached actions.
  /// </summary>
  /// <typeparam name="TController"></typeparam>
  public abstract class BaseCoroutineState<TController> : ActionsHolder, ICoroutineState {

    protected TController controller;

    public virtual void Init(TController controller) {
      this.controller = controller;
    }

    /// <summary>
    /// Call all attached <see cref="IStateAction"/>s
    /// </summary>
    public virtual void UpdateState() {
      Act();
    }

    public abstract IEnumerator StartState();

    public virtual void ExitState() { }
  }
}