using System.Collections;
using UnityEngine;

public class PlayerUnitFallIntoAbyssState : MonoBehaviour, IPlayerUnitState
{
  public float fallAmount = 32f;

  private PlayerPhysics physics;

  public void ExitState()
  {
  }

  public void Inject(PlayerUnitDI di)
  {
    physics = di.physics;
  }

  public void StartState()
  {
  }

  public void UpdateState()
  {
  }
}