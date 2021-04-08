using UnityEngine;
using NUnit.Framework;
using Kite;

namespace KiteEditTests {
  [TestFixture]
  public class MovingColliderMovementTest {

    //[OneTimeSetUp]
    //public void OneTimeSetup() {
    //  TestHelpers.OpenEmptyScene();
    //}

    //[TearDown]
    //public void Teardown() {
    //  GameObjectHelpers.Clear();
    //}

    //[Test]
    //public void ObjectStandingOnRightEdgeIsPushedOnlyOnceOnColliderRightDownMove() {
    //  var standingObjectPosition = Vector2.one * 2;
    //  var standingObject = GameObjectHelpers.CreatePhysicsMovement("pushable", standingObjectPosition, Vector2.one * 2);
    //  var pushable = standingObject.AddComponent<PushableComponent>();
    //  pushable.Init(standingObject.GetComponent<IPhysicsMovement>(), null);

    //  var movingCollider = GameObjectHelpers.CreatePhysicsMovement("movingCollider", Vector2.zero, new Vector2(4, 2));
    //  var movingColliderMovement = movingCollider.AddComponent<MovingColliderMovement>();
    //  var collidableComponent = movingCollider.AddComponent<CollidableComponent>();
    //  var carryComponent = movingCollider.AddComponent<PhysicsCarry>();
    //  movingColliderMovement.Init(
    //    movingCollider.GetComponent<IPhysicsMovement>(),
    //    collidableComponent,
    //    carryComponent
    //  );
    //  var collisionMoveHitsFactory = movingCollider.GetComponent<ICollisionMoveHitsFactory>();
    //  collisionMoveHitsFactory.AutoClear = false;

    //  var moveAmount = new Vector2(1, -1);
    //  movingColliderMovement.MoveWithObjectsAbove(moveAmount);
    //  Assert.That(standingObject.GetComponent<IPosition>().Value, Is.EqualTo(standingObjectPosition + moveAmount));
    //}
  }
}