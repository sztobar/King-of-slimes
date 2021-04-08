namespace Kite {

  public struct CollisionState {

    bool top, bottom, left, right;

    public bool this[Direction4 direction] {
      get {
        switch (direction) {
          case Direction4.Down:
            return bottom;
          case Direction4.Left:
            return left;
          case Direction4.Right:
            return right;
          case Direction4.Up:
          default:
            return top;
        }
      }

      set {
        switch (direction) {
          case Direction4.Down:
            bottom = value;
            break;
          case Direction4.Left:
            left = value;
            break;
          case Direction4.Right:
            right = value;
            break;
          case Direction4.Up:
          default:
            top = value;
            break;
        }
      }
    }

    public bool this[Direction2H direction] {
      get {
        switch (direction) {
          case Direction2H.Left:
            return left;
          case Direction2H.Right:
          default:
            return right;
        }
      }

      set {
        switch (direction) {
          case Direction2H.Left:
            left = value;
            break;
          case Direction2H.Right:
          default:
            right = value;
            break;
        }
      }
    }
  }
}