using Kite;
using System;
using System.Collections;
using UnityEngine;

public class RockbatFlyState : MonoBehaviour, IRockbatState
{
  public float trackPlayerTime;
  public float destroyAfterFlyTime;

  private Rockbat controller;

  private Vector2 flyDirection;
  private float elapsedTime;
  private bool isTrackingPlayer;

  public void Inject(Rockbat rockbat)
  {
    controller = rockbat;
  }

  public void StartState()
  {
    controller.animator.PlayFly();
    elapsedTime = 0;
    isTrackingPlayer = true;
    flyDirection = GetDirectionToTarget();
    controller.flip.Direction = Direction2HHelpers.FromFloat(flyDirection.x);
  }

  public void UpdateState()
  {
    if (isTrackingPlayer)
    {
      if (IsTargetDead())
      {
        isTrackingPlayer = false;
      }
      else if (elapsedTime < trackPlayerTime)
      {
        flyDirection = GetDirectionToTarget();
        controller.flip.Direction = Direction2HHelpers.FromFloat(flyDirection.x);
      }
    }
    else if (elapsedTime >= destroyAfterFlyTime)
    {
      controller.destroyable.DestroyEnemy();
    }
    controller.physics.FlyToTarget(flyDirection);
    elapsedTime += Time.deltaTime;
  }

  public void ExitState() { }

  private Vector2 GetDirectionToTarget()
  {
    PlayerUnitController target = controller.range.Target;
    Vector2 distance = target.transform.position - controller.transform.position;
    Vector2 directionToTarget = distance.normalized;
    return directionToTarget;
  }

  private bool IsTargetDead() => controller.range.Target.di.hp.IsDead;
}