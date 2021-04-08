using UnityEngine;

namespace Kite {

  [ExecuteInEditMode]
  public class VerticalFlipComponent : MonoBehaviour {

    public Direction2V Direction {

      get => direction;

      set {
        if (value != direction) {
          Flip();
        }
      }
    }

    public float Value { get => direction.ToFloat(); }
    public Vector2 Vector2 { get => direction.ToVector2(); }
    public Vector2 Rotation { get => new Vector2(1, transform.up.y); }

    [SerializeField]
    private Direction2V initialDirection = Direction2V.Up;

    [SerializeField]
    private new Collider2D collider;

    [SerializeField]
    private bool rotateInEditMode;

    private Direction2V direction = Direction2V.Up;

    public void Flip() {
      direction = direction.Flip();
      transform.rotation = Quaternion.Euler(direction == Direction2V.Up ? 0 : 180, 0, 0);
      //transform.FlipVertically();
      if (collider) {
        transform.Translate(new Vector2(0, -2f * collider.offset.y));
      }
    }

    void Awake() {
      direction = initialDirection;
    }

    private void Update() {
      if (!Application.isPlaying && rotateInEditMode && initialDirection != direction) {
        Flip();
      }
    }
  }
}