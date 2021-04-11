using Kite;
using System.Collections;
using UnityEngine;

public class PushBlockPhysicsCollidable : PhysicsCollidable
{
  public PushBlockPhysics physics;
  public bool debug;

  public override float GetAllowedMoveInto(PhysicsMove move)
  {
    PushEffectable pushEffectable = move.moving.GetComponent<PushEffectable>();
    if (pushEffectable && CanBePushed(pushEffectable, move.dir))
    {
      float allowedMove = physics.movement.GetAllowedMovement(move.collideDistance, move.dir);
      if (debug)
        Debug.Log($"[PushBlock] collideDist: {move.collideDistance}; allowed: {allowedMove}");
      return allowedMove;
    }
    //PlayerUnitController player = move.hit.rigidbody.GetComponent<PlayerUnitController>();
    //if (PushConditionsPass(player, move.dir) && player.di.physics.pushHandler.CanPush(physics.requiredStrength))
    //{
    //  float allowedMove = physics.movement.GetAllowedMovement(move.collideDistance, move.dir);
    //  return allowedMove;
    //}
    return 0;
  }

  public override void OnMoveInto(PhysicsMove move)
  {
    if (move.dir == Dir4.down)
    {
      StandEffectable standEffectable = move.moving.GetComponent<StandEffectable>();
      if (standEffectable)
        physics.carryEffector.AddEffectable(standEffectable);
      return;
    }
    PushEffectable pushEffectable = move.moving.GetComponent<PushEffectable>();
    //PlayerUnitController player = move.hit.rigidbody.GetComponent<PlayerUnitController>();
    if (pushEffectable && CanBePushed(pushEffectable, move.dir))
    {
      physics.pushEffector.AddEffectable(pushEffectable);
      if (RaycastHelpers.IsValidDistance(move.collideDistance))
        physics.Move(move.collideDistance, move.dir);

      //if (player.di.physics.pushHandler.CanPush(physics.requiredStrength))
      //{
      //  player.di.physics.pushHandler.SetIsPushing();
      //  physics.Move(move.collideDistance, move.dir);
      //}
    }
  }

  private bool CanBePushed(PushEffectable effectable, Dir4 dir) =>
    effectable.canPush &&
    effectable.strength >= physics.pushEffector.requiredStrength &&
    physics.IsGrounded &&
    dir.Axis == 0;
}