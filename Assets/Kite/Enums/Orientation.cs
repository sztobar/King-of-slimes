namespace Kite {
  public enum Orientation {
    Horizontal,
    Vertical
  }

  public static class OrientationHelpers {
    public static int ToVector2Index(this Orientation orientation) =>
      orientation == Orientation.Horizontal ? 0 : 1;

    public static Orientation Opposite(this Orientation orientation) =>
      orientation == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal;
  }
}