using NUnit.Framework;
using UnityEngine;
using Kite;

namespace KiteEditTests {
  [TestFixture]
  public class CollisionMoveControllerTest {

    [Test]
    public void AllowsNoDistanceIfDistanceToCollisionIs0() {
      var gameObject = new GameObject("testObject");
      var physicsMovement = gameObject.AddComponent<PhysicsMovement>();
      var collisionMoveController = CollisionMoveController.Create(gameObject.transform, physicsMovement);

      var collisionGameObject = new GameObject("collidedObject");
      var transform = collisionGameObject.transform;
      var collider = collisionGameObject.AddComponent<BoxCollider2D>();
      var collisionPoint = new Vector2(1, 1);
      var collisionNormal = Vector2.left;
      var rayDir = Dir4.right;
      var distanceToCollision = 0;
      var hit = RaycastHitHelpers.MockHit(collider, collisionPoint, collisionNormal, distanceToCollision);

      var distanceToCheck = 2f;
      var allowedDistace = collisionMoveController.GetAllowedMoveInto(hit, distanceToCheck, rayDir);
      Assert.That(allowedDistace, Is.EqualTo(0));
    }

    [Test]
    public void AllowsHalfDistanceIfThatsDistanceToCollision() {
      var gameObject = new GameObject("testObject");
      var physicsMovement = gameObject.AddComponent<PhysicsMovement>();
      var collisionMoveController = CollisionMoveController.Create(gameObject.transform, physicsMovement);

      var collisionGameObject = new GameObject("collidedObject");
      var transform = collisionGameObject.transform;
      var collider = collisionGameObject.AddComponent<BoxCollider2D>();
      var collisionPoint = new Vector2(1, 1);
      var collisionNormal = Vector2.left;
      var rayDir = Dir4.right;
      var distanceToCollision = 1;
      var hit = RaycastHitHelpers.MockHit(collider, collisionPoint, collisionNormal, distanceToCollision);
      //var collisionMoveHit = new CollisionMoveHit(transform, collisionPoint, collisionNormal, rayDir, distanceToCollision);

      var distanceToCheck = 2f;
      var allowedDistace = collisionMoveController.GetAllowedMoveInto(hit, distanceToCheck, rayDir);
      Assert.That(allowedDistace, Is.EqualTo(distanceToCheck/2));
    }

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(1.9f)]
    [TestCase(2)]
    public void AllowsFullDistanceIfCollidableAllows(float distanceToCollision) {
      var gameObject = new GameObject("testObject");
      var physicsMovement = gameObject.AddComponent<PhysicsMovement>();
      var collisionMoveController = CollisionMoveController.Create(gameObject.transform, physicsMovement);

      var collisionGameObject = new GameObject("collidedObject");
      var transform = collisionGameObject.transform;
      var collider = collisionGameObject.AddComponent<BoxCollider2D>();
      var physicsMoveCollidable = collisionGameObject.AddComponent<PhysicsMoveCollidableMock>();
      physicsMoveCollidable.allowedMove = new Vector2(2, 0);
      var collisionPoint = new Vector2(1, 1);
      var collisionNormal = Vector2.left;
      var rayDir = Dir4.right;
      var rayDistanceToCollision = distanceToCollision;
      var hit = RaycastHitHelpers.MockHit(collider, collisionPoint, collisionNormal, rayDistanceToCollision);
      //var collisionMoveHit = new CollisionMoveHit(transform, collisionPoint, collisionNormal, rayDirection, rayDistanceToCollision);

      var distanceToCheck = 2f;
      var allowedDistace = collisionMoveController.GetAllowedMoveInto(hit, distanceToCheck, rayDir);
      Assert.That(allowedDistace, Is.EqualTo(distanceToCheck));
    }
  }
}
