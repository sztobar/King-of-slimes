using UnityEngine;
using System.Collections;
using NUnit.Framework;
using Kite;

namespace KiteEditTests {
  [TestFixture]
  public class CollisionMoveHitsFactoryTest {

    [TearDown]
    public void Teardown() {
      GameObjectHelpers.Clear();
    }

    [Test]
    public void ItFiltersHitsWithSameCollider() {
      var gameObject = GameObjectHelpers.Create("factory");
      var omitCollider = gameObject.GetComponent<Collider2D>();
      var factory = CollisionMoveHitsFactory.Create(omitCollider);

      var hitGameObject = GameObjectHelpers.CreateCollider("collider", Vector2.zero, Vector2.one);
      var collider = hitGameObject.GetComponent<Collider2D>();
      var point = Vector2.one;
      var normal = Vector2.up;
      var distance = 1;

      var hits = new RaycastHit2D[2] {
        RaycastHitHelpers.MockHit(collider, point, normal, distance),
        RaycastHitHelpers.MockHit(collider, point, normal, distance)
      };
      var count = 2;
      var result = factory.GetUnique(hits, count);
      Assert.That(result.Count, Is.EqualTo(1));
    }

    [Test]
    public void ItReturnsSameHitOnNextGetUniqueCall() {
      var gameObject = GameObjectHelpers.Create("factory");
      var omitCollider = gameObject.GetComponent<Collider2D>();
      var factory = CollisionMoveHitsFactory.Create(omitCollider);

      var hitGameObject = GameObjectHelpers.CreateCollider("collider", Vector2.zero, Vector2.one);
      var collider = hitGameObject.GetComponent<Collider2D>();
      var point = Vector2.one;
      var normal = Vector2.up;
      var distance = 1;

      var hits = new RaycastHit2D[1] {
        RaycastHitHelpers.MockHit(collider, point, normal, distance)
      };
      var count = 1;
      var firstCallResult = factory.GetUnique(hits, count);
      var secondCallResult = factory.GetUnique(hits, count);
      Assert.That(firstCallResult[0].collider, Is.EqualTo(secondCallResult[0].collider));
    }

    //[Test]
    //public void ItReturnsNoHitsOnNextCallIfAutoClearIsOff() {
    //  var gameObject = GameObjectHelpers.Create("factory");
    //  var factory = gameObject.AddComponent<CollisionMoveHitsFactory>();
    //  factory.AutoClear = false;

    //  var hitGameObject = GameObjectHelpers.CreateCollider("collider", Vector2.zero, Vector2.one);
    //  var collider = hitGameObject.GetComponent<Collider2D>();
    //  var point = Vector2.one;
    //  var normal = Vector2.up;
    //  var distance = 1;

    //  var hits = new RaycastHit2D[1] {
    //    RaycastHitHelpers.MockHit(collider, point, normal, distance)
    //  };
    //  var count = 1;
    //  var firstCallResult = factory.GetUnique(hits, count);
    //  Assert.That(firstCallResult.Count, Is.EqualTo(1));
    //  var secondCallResult = factory.GetUnique(hits, count);
    //  Assert.That(secondCallResult.Count, Is.EqualTo(0));
    //}
  }
}