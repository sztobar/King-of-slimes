namespace Kite {

  public interface IPausable {

    void HandlePauseOn();
    
    void HandlePauseOff();

    void FixedPlayUpdate(float dt);

    void FixedPauseUpdate(float dt);
  }
}
