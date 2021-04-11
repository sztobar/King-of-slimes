using Kite;
using System.Collections;
using UnityEngine;

public class RockbatSleepState : MonoBehaviour, IRockbatState
{
  private Rockbat controller;

  public void Inject(Rockbat rockbat)
  {
    controller = rockbat;
  }

  public void StartState()
  {
  }

  public void UpdateState()
  {
    controller.range.TrackForPlayerUpdate();

    if (controller.range.hasTarget)
      controller.fsm.PlayFly();
  }

  public void ExitState()
  {
    controller.range.DisableRange();
  }
}