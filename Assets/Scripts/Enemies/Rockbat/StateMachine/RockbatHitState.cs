using Kite;
using System.Collections;
using UnityEngine;

public class RockbatHitState : MonoBehaviour, IRockbatState
{
  public AnimationCurve hitForceCurve;
  public WorldTileFloat hitDistance;
  public float hitTime;

  private float elapsedTime;
  private Rockbat controller;
  private Vector2 startPosition;
  private Vector2 endPosition;

  public Vector2 Direction { get; set; }

  public void Inject(Rockbat rockbat)
  {
    controller = rockbat;
  }

  public void StartState()
  {
    controller.cameraCollider.DisableCollider();
    controller.animator.PlayHit();
    startPosition = controller.physics.rigidbody.position;
    endPosition = startPosition + (hitDistance * Direction);
  }

  public void UpdateState()
  {
    float t = elapsedTime / hitTime;
    float curveT = hitForceCurve.Evaluate(t);
    controller.physics.rigidbody.position = Vector2.Lerp(startPosition, endPosition, curveT);

    if (elapsedTime >= hitTime)
      controller.destroyable.DestroyEnemyWithPoof();
    else
      elapsedTime += Time.deltaTime;
  }

  public void ExitState()
  {
  }
}