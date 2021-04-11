using Kite;
using System;
using System.Collections;
using UnityEngine;

public class PigmanPhysics : MonoBehaviour, IPigmanComponent {

  public float runTileVelocity;
  public float walkTileVelocity;
  public EnemyPhysics physics;
  public HorizontalFlipComponent flip;

  public void Inject(PigmanController controller) {
    runTileVelocity = controller.data.runTileVelocity;
    walkTileVelocity = controller.data.walkTileVelocity;
  }

  public void RunInDirection(Direction2H direction) {
    flip.Direction = direction;
    physics.MoveUpdate(direction.ToVector2() * TileHelpers.TileToWorld(runTileVelocity));
  }

  internal void WalkInDirection(Direction2H direction) {
    flip.Direction = direction;
    physics.MoveUpdate(direction.ToVector2() * TileHelpers.TileToWorld(walkTileVelocity));
  }
}
