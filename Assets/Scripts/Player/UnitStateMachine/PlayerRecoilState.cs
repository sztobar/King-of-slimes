using Kite;
using UnityEngine;

public class PlayerRecoilState : MonoBehaviour, IPlayerUnitState
{
  public bool freezeGravity;
  public float recoilTime;
  public AnimationCurve recoilEase;

  private PlayerUnitStateMachine stateMachine;
  private PlayerPhysics physics;

  private float timeLeft;
  private Vector2 recoilForce;

  public void Inject(PlayerUnitDI di)
  {
    stateMachine = di.stateMachine;
    physics = di.physics;
  }

  public void StartState()
  {
    timeLeft = recoilTime;
    if (freezeGravity)
      physics.gravity.enabled = false;
    else
      if (recoilForce.y > 0)
        physics.velocity.Y = physics.gravity.JumpVelocity(recoilForce.y);
      else
        physics.velocity.Y = recoilForce.y;
  }

  public void UpdateState()
  {
    float t = recoilTime - timeLeft;
    if (freezeGravity)
    {
      physics.velocity.Value = recoilForce * DerivativeHelpers.ForwardDerivative(t, recoilTime, recoilEase.Evaluate);
    }
    else
    {
      physics.velocity.X = recoilForce.x * DerivativeHelpers.ForwardDerivative(t, recoilTime, recoilEase.Evaluate);
    }
    physics.ParalyzedUpdate();

    if (timeLeft <= 0)
      stateMachine.SetDefaultState();
    else
      timeLeft -= Time.deltaTime;

  }

  public void ExitState()
  {
    if (freezeGravity)
      physics.gravity.enabled = true;
  }

  internal void SetForce(Vector2 recoilForce)
  {
    this.recoilForce = recoilForce;
  }
}
