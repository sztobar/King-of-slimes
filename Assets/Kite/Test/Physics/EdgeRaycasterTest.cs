using UnityEngine;
using NUnit.Framework;
using Kite;

namespace KiteEditTests {
  [TestFixture]
  public class EdgeRaycasterTest {

    [OneTimeSetUp]
    public void OneTimeSetup() {
      TestHelpers.OpenEmptyScene();
      Physics2D.queriesStartInColliders = true;
      Physics2D.IgnoreLayerCollision(CollisionHelpers.layer, CollisionHelpers.layer, false);
    }

    [SetUp]
    public void Setup() {
    }

    [TearDown]
    public void Teardown() {
      GameObjectHelpers.Clear();
    }

    //[Test]
    //public void ItDoesntReturnHitWithOwnCollider() {
    //  var gameObject = GameObjectHelpers.Create();
    //  var boxCollider = gameObject.AddComponent<BoxCollider2D>();
    //  var boundsComponent = gameObject.AddComponent<BoundsComponent>();
    //  boundsComponent.Init(boxCollider);
    //  boxCollider.size = Vector2.one * 2;

    //  RaycastHit2D hit = Physics2D.Raycast(new Vector2(1 - 0.01f, 0), Vector2.right, 10, CollisionHelpers.GetLayerMask());
    //  Assert.That(hit.collider, Is.EqualTo(boxCollider));

    //  var raycaster = new EdgeRaycaster(boundsComponent, Orientation.Vertical);
    //  (_, var count) = raycaster.GetHits(Vector2.zero, 1, Direction4.Right);
    //  Assert.That(count, Is.EqualTo(0));
    //  Object.DestroyImmediate(gameObject);
    //}

    //[Test]
    //public void ItReturnsHitToOneCollider() {
    //  var gameObject = GameObjectHelpers.Create();
    //  var boxCollider = gameObject.AddComponent<BoxCollider2D>();
    //  boxCollider.size = Vector2.one * 2;

    //  var otherGameObject = GameObjectHelpers.Create();
    //  otherGameObject.transform.position = new Vector2(3, 0);
    //  var otherBoxCollider = otherGameObject.AddComponent<BoxCollider2D>();
    //  var boundsComponent = gameObject.AddComponent<BoundsComponent>();
    //  boundsComponent.Init(boxCollider);
    //  otherBoxCollider.size = Vector2.one * 2;

    //  RaycastHit2D hit = Physics2D.Raycast(new Vector2(1 + 0.01f, 0), Vector2.right, 10, CollisionHelpers.GetLayerMask());
    //  Assert.That(hit.collider, Is.EqualTo(otherBoxCollider));

    //  var raycaster = new EdgeRaycaster(boundsComponent, Orientation.Vertical);
    //  (var hits, var count) = raycaster.GetHits(Vector2.zero, 2, Direction4.Right);
    //  Assert.That(count, Is.EqualTo(2));
    //  Assert.That(hits[0].collider, Is.EqualTo(otherBoxCollider));
    //  Assert.That(hits[1].collider, Is.EqualTo(otherBoxCollider));
    //}
  }
}