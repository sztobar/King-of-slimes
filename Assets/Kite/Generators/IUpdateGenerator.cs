namespace Kite {
  public interface IUpdateGenerator {
    bool IsDone { get; }
    void Tick(float dt);
  }
}
