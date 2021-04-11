namespace Kite {
  public abstract class StateWithActions<TController> : ActionsHolder, IState {

    protected TController controller;

    public virtual void Init(TController controller) {
      this.controller = controller;
    }

    public virtual void UpdateState() {
      Act();
    }

    public virtual void StartState() {
    }

    public virtual void ExitState() {
    }
  }
}