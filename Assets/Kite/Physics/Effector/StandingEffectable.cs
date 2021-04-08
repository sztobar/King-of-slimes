using UnityEngine;
using System.Collections.Generic;
using System;

namespace Kite
{
  [Obsolete]
  public class StandingEffectable : EffectableBehaviour
  {
    private PhysicsMovement movement;
    private HashSet<PhysicsEffector<PhysicsEffectable>> currentEffectors = new HashSet<PhysicsEffector<PhysicsEffectable>>();
    private bool isGrounded;

    public bool IsGrounded => isGrounded;

    public override void Push(float distance, Dir4 dir)
    {
      movement.TryToMove(distance, dir);
    }

    public void Clear()
    {
      foreach (PhysicsEffector<PhysicsEffectable> effector in currentEffectors)
      {
        if (!currentEffectors.Contains(effector))
        {
          effector.UnregisterEffectable(this);
        }
      }
      currentEffectors = currentEffectors;
    }

    public void CheckEffectorsBelow()
    {
      //IEnumerable<CollisionMoveHit> collisionMoveHits = GetCollisionMoveHitsBelow();

      //bool isGrounded = false;
      //HashSet<PhysicsEffector<PhysicsEffectable>> currentEffectors = new HashSet<PhysicsEffector<PhysicsEffectable>>();
      //foreach (CollisionMoveHit hit in collisionMoveHits)
      //{
      //  isGrounded = true;
      //  // TODO: integrate it into PhysicsMovement and utilize
      //  // OnPhysicsMoveInto with dir4.down as effector match
      //  PhysicsEffector<PhysicsEffectable> effector = hit.transform.GetComponent<PhysicsEffector<PhysicsEffectable>>();
      //  if (effector && effector.Match(hit))
      //  {
      //    effector.RegisterEffectable(this);
      //    currentEffectors.Add(effector);
      //  }
      //}
      //ClearPreviousEffectors(currentEffectors);
      //this.isGrounded = isGrounded;
    }

    private IEnumerable<CollisionMoveHit> GetCollisionMoveHitsBelow()
    {
      float distance = RaycastHelpers.skinWidth;
      Direction4 direction = Direction4.Down;
      (RaycastHit2D[] hits, int count) = movement.raycaster.GetHits(distance, direction);
      IEnumerable<CollisionMoveHit> collisionMoveHits = movement.collisionMoveHitsFactory.GetUniqueRaycasterHits(hits, count, direction);
      return collisionMoveHits;
    }

    private void ClearPreviousEffectors(HashSet<PhysicsEffector<PhysicsEffectable>> currentEffectors)
    {
      foreach (PhysicsEffector<PhysicsEffectable> effector in this.currentEffectors)
      {
        if (!currentEffectors.Contains(effector))
        {
          effector.UnregisterEffectable(this);
        }
      }
      this.currentEffectors = currentEffectors;
    }

    private void OnDisable()
    {
      foreach (PhysicsEffector<PhysicsEffectable> effector in currentEffectors)
      {
        effector.UnregisterEffectable(this);
      }
    }
  }
}