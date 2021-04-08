namespace Kite {

  public interface IState {

    void StartState();

    void UpdateState();

    void ExitState();
  }
}