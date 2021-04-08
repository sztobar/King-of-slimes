using UnityEngine;

namespace Kite {
  public class PixelPerfectPosition : MonoBehaviour {

    private Transform parent;

    private Vector3 SourcePosition => parent ? parent.position : transform.position;
    private float SourceRightX => parent ? parent.right.x : transform.right.x;

    private void Awake() {
      parent = transform.parent;
    }

    private void Update() {
      transform.localPosition = FixPixelPosition(SourcePosition);
    }

    private Vector3 FixPixelPosition(Vector3 realPosition) {
      return new Vector3(
        SourceRightX * FixPixelPosition(realPosition.x),
        FixPixelPosition(realPosition.y),
        transform.localPosition.z
      );
    }

    float FixPixelPosition(float realPos) {
      float rest = realPos % 1;
      return Mathf.Abs(rest) >= 0.5f ? Mathf.Sign(rest) - rest : -rest;
    }
  }
}