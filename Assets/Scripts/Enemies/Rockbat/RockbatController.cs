using System.Collections;
using UnityEngine;

public class RockbatController : MonoBehaviour {

  public RockbatDi di;
  public float destroyAfterFlyTime;

  //private void Awake() {
  //  di.Inject(this);
  //}

  //private void FixedUpdate() {
  //  di.range.TrackUpdate();
  //  if (di.range.hasTarget)
  //  {
  //    di.physics.FlyToTarget();
  //  }
  //}

  public void DestroyWithPoof() {
    di.deathPoof.DestroyWithPoof(gameObject);
  }

  public void DestroyImmediate()
  {
    Destroy(gameObject);
  }

  public void SetDestroyTimer() {
    Destroy(gameObject, destroyAfterFlyTime);
  }
}
