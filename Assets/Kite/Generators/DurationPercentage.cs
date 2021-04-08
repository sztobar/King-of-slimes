using System;

namespace Kite {

  public class DurationPercentage : IUpdateGenerator {

    private readonly float duration;
    private readonly Action<float> callback;
    private float timeElapsed;

    public DurationPercentage(float duration, Action<float> callback) {
      this.duration = duration;
      this.callback = callback;
    }

    public bool IsDone => timeElapsed == duration;

    public void Tick(float dt) {
      timeElapsed += dt;
      if (timeElapsed > duration) {
        timeElapsed = duration;
      }
      float percentage = timeElapsed / duration;
      callback(percentage);
    }
  }
}
