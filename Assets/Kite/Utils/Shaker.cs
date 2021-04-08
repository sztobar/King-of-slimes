using UnityEngine;
using System.Collections;

namespace Kite {
  public class Shaker {
    private readonly Transform transform;
    private readonly float duration;
    private readonly float amount;
    private UpdatePhase phase;

    public Shaker(Transform transform, float duration, float amount = Constants.PIXEL_SIZE, UpdatePhase phase = UpdatePhase.Update) {
      this.transform = transform;
      this.duration = duration;
      this.amount = amount;
      this.phase = phase;
    }

    public IEnumerator GetEnumerator() {
      float elapsedTime = 0f;
      Vector3 previousShakeValue = Vector3.zero;
      var next = phase == UpdatePhase.LateUpdate ? new WaitForEndOfFrame() : null;
      while(elapsedTime < duration) {
        Vector3 newShakeValue = Random.insideUnitCircle * amount;
        transform.position = transform.position - previousShakeValue + newShakeValue;
        previousShakeValue = newShakeValue;
        elapsedTime += Time.deltaTime;
        do {
          yield return next;
        } while (PauseManager.IsPaused);
      }
      transform.position -= previousShakeValue;
    }

    public enum UpdatePhase {
      Update,
      LateUpdate
    }
  }
}