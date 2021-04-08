using UnityEngine;

namespace Kite {

  [ExecuteInEditMode]
  public class HorizontalFlipComponent : MonoBehaviour {

    public Direction2H Direction {

      get => direction;

      set {
        if (direction != value) {
          Flip();
        }
      }
    }

    public float Value { get => direction.ToFloat(); }
    public Vector2 Vector2 { get => direction.ToVector2(); }
    public Vector2 Rotation { get => new Vector2(transform.right.x, 1); }


    [SerializeField]
    private Direction2H initialDirection = Direction2H.Right;

    [SerializeField]
    private new Collider2D collider;

    [SerializeField]
    private bool rotateInEditMode;

    private Direction2H direction = Direction2H.Right;

    public void Flip() {
      direction = direction.Flip();
      transform.rotation = Quaternion.Euler(0, direction == Direction2H.Right ? 0 : 180, 0);
      //transform.FlipHorizontally();
      if (collider) {
        transform.Translate(new Vector2(-2f * collider.offset.x, 0));
      }
    }

    public void LookTowards(Vector2 destination) {
      float directionSign = Mathf.Sign(destination.x - transform.position.x);
      Direction = directionSign >= 0 ? Direction2H.Right : Direction2H.Left;
    }

    private void Awake() {
      direction = initialDirection;
    }

    private void Update() {
      if (!Application.isPlaying && rotateInEditMode && initialDirection != direction) {
        Flip();
      }
    }
  }
}