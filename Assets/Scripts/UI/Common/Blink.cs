using UnityEngine;
using System.Collections;
using TMPro;

namespace UI.Common {
  public class Blink : MonoBehaviour {

    [SerializeField]
    private bool startWithBlink;
    
    [SerializeField]
    private float blinkInterval = 0.5f;

    [SerializeField]
    private float alpha = 0.5f;

    [SerializeField]
    private TextMeshProUGUI textMesh;

    private float intervalTimeElapsed;
    private bool transparent;

    public bool IsFlashing { get; private set; }

    private void Awake() {
      if(startWithBlink) {
        StartBlinking();
      }
    }

    public void StartBlinking() {
      intervalTimeElapsed = 0;
      transparent = false;
      IsFlashing = true;
    }

    private void Update() {
      if (IsFlashing) {
        FlashUpdate();
      }
    }

    void FlashUpdate() {
      intervalTimeElapsed += Time.deltaTime;

      if (intervalTimeElapsed >= blinkInterval) {
        intervalTimeElapsed -= blinkInterval;
        FlashToggle();
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
      textMesh.alpha = alpha;
    }

    private void SetSolid() {
      transparent = false;
      textMesh.alpha = 1f;
    }
  }
}