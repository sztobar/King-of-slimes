using UnityEngine;
using System.Collections;
using Kite;
using NUnit.Framework;

namespace KiteEditTests {
  [TestFixture]
  public class BoundsTest {

    private Bounds bounds = new Bounds(new Vector3(1, -0.5f), new Vector3(4, 3));

    [Test]
    public void ReturnsCorrectLefTopEnd() {
      Vector2 end = bounds.GetEnd(Direction2H.Left, Direction2V.Up);
      Vector2 expectedEnd = new Vector2(-0.99f, 0.99f);
      Assert.That(end, Is.EqualTo(expectedEnd));
    }

    [Test]
    public void ReturnsCorrectLeftBottomEnd() {
      Vector2 end = bounds.GetEnd(Direction2H.Left, Direction2V.Down);
      Vector2 expectedEnd = new Vector2(-0.99f, -1.99f);
      Assert.That(end, Is.EqualTo(expectedEnd));
    }

    [Test]
    public void ReturnsCorrectRightTopEnd() {
      Vector2 end = bounds.GetEnd(Direction2H.Right, Direction2V.Up);
      Vector2 expectedEnd = new Vector2(2.99f, 0.99f);
      Assert.That(end, Is.EqualTo(expectedEnd));
    }

    [Test]
    public void ReturnsCorrectRightBottomEnd() {
      Vector2 end = bounds.GetEnd(Direction2H.Right, Direction2V.Down);
      Vector2 expectedEnd = new Vector2(2.99f, -1.99f);
      Assert.That(end, Is.EqualTo(expectedEnd));
    }
  }
}