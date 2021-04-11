using Kite;
using System;
using System.Collections;
using UnityEngine;

public class PlayerUnitHitState : MonoBehaviour, IPlayerUnitState
{
  public float paralyzeDuration;
  public float invulnerableTime;
  public AnimationCurve hitForceCurve;

  private PlayerUnitStateMachine stateMachine;
  private PlayerPhysics physics;
  private PlayerVulnerability vulnerability;

  private Vector2 pushForce;

  public void Inject(PlayerUnitDI di)
  {
    stateMachine = di.stateMachine;
    physics = di.physics;
    vulnerability = di.vulnerability;
  }

  public void StartState()
  {
    physics.force.SetForce(value: pushForce, duration: paralyzeDuration, curve: hitForceCurve.Evaluate);
    AudioSingleton.PlaySound(AudioSingleton.Instance.clips.playerHit);
    if (vulnerability.IsVulnerable())
      vulnerability.SetInvulnerable(invulnerableTime);
  }

  public void UpdateState()
  {
    physics.ParalyzedUpdate();

    if (!physics.force.HasForce())
      stateMachine.SetDefaultState();
  }

  public void ExitState()
  {
  }

  public void SetPushForce(Vector2 pushForce)
  {
    this.pushForce = pushForce;
  }
}
