using UnityEngine;
using System.Collections;

namespace Kite {

  public class FlashSprite : MonoBehaviour {

    [SerializeField] private float flashInterval = 0.05f;
    [SerializeField] private float defaultFlashDuration = 0.5f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Color initialColor;
    private Color semiTransparentColor;
    private bool transparent;

    private float intervalTimeElapsed;
    private float durationTimeLeft;

    public bool IsFlashing => durationTimeLeft > 0;

    private void Awake() {
      initialColor = spriteRenderer.color;
      semiTransparentColor = initialColor;
      semiTransparentColor.a = 0.5f;
    }

    public void StartFlash() {
      InitStartFlashParams(defaultFlashDuration);
    }

    public void StartFlash(float duration) {
      InitStartFlashParams(duration);
    }

    private void InitStartFlashParams(float duration) {
      durationTimeLeft = duration;
      enabled = true;
    }

    void FlashUpdate() {
      durationTimeLeft -= Time.deltaTime;
      intervalTimeElapsed += Time.deltaTime;

      if (intervalTimeElapsed >= flashInterval) {
        intervalTimeElapsed -= flashInterval;
        FlashToggle();
      }
      if (durationTimeLeft <= 0) {
        SetSolid();
      }
    }

    private void FlashToggle() {
      if (transparent) {
        SetSolid();
      } else {
        SetTransparent();
      }
    }

    private void SetTransparent() {
      transparent = true;
      spriteRenderer.color = semiTransparentColor;
    }

    private void SetSolid() {
      transparent = false;
      spriteRenderer.color = initialColor;
    }

    private void Update() {
      if (durationTimeLeft > 0) {
        FlashUpdate();
      } else {
        enabled = false;
      }
    }
  }
}
