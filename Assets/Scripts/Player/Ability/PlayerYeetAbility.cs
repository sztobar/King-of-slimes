using System.Collections;
using UnityEngine;

public class PlayerYeetAbility : MonoBehaviour, IPlayerAbility
{
  private PlayerInput input;
  private PlayerUnitHandler unitHandler;
  private PlayerUnitStateMachine stateMachine;
  private PlayerPhysics physics;
  private PlayerBaseStats stats;
  private PlayerHPModule hp;

  public void ControlUpdate()
  {
    if (!stats.IsAssembly)
      return;

    if (input.yeet.IsPressed())
    {
      SlimeType selectedType = unitHandler.SelectedType;
      if (selectedType.IsKing())
      {
        if (hp.IsFull())
          PerformYeet(selectedType);
      }
      else
      {
        PlayerBaseStats unitStats = unitHandler.GetSelectable(selectedType).Stats;
        int hearts = unitStats.Hearts;
        if (hp.CurrentHP > hearts)
          PerformYeet(selectedType);
      }
    }
  }

  private void PerformYeet(SlimeType selectedType)
  {
    input.yeet.Use();
    unitHandler.OnSlimeYeet(selectedType);
    stateMachine.SetInactiveState();
    physics.velocity.Value = Vector2.zero;
  }

  public void InactiveUpdate() { }

  public void WallSlideUpdate() { }

  public void Inject(PlayerUnitDI di)
  {
    physics = di.physics;
    stateMachine = di.stateMachine;
    unitHandler = di.mainDi.unitHandler;
    input = di.mainDi.controller.input;
    stats = di.stats;
    hp = di.hp;
  }
}